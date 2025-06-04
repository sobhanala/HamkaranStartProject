using Domain.SharedSevices;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Repositorys
{
    /// <summary>
    /// moshkel injoori bud ke id -1 bud ye zamani ava taghir dadam ke in ghaziye hal she vali khub in migi db auto inc ro khammosh konam ke fek konam esthebahee 
    /// </summary>
    /// <typeparam name="TDataTable"></typeparam>
    public abstract class EnhancedTableAdapterBase<TDataTable> : IEnhancedTableAdapter
        where TDataTable : DataTable, IEnhancedDataTableMetadata, new()

    {
        protected readonly ILogger Logger;
        protected readonly ISessionService SessionService;
        protected int? AuditUserId;
        protected TDataTable DataTable;
        protected readonly ITransactionManager TransactionManager;

        protected abstract DbDataAdapter DataAdapter { get; }
        protected SqlConnection Connection => TransactionManager.GetConnection();
        protected SqlTransaction Transaction => TransactionManager.GetTransaction();


        protected EnhancedTableAdapterBase(ILogger logger, ISessionService sessionService, ITransactionManager transactionManager)
        {
            Logger = logger;
            SessionService = sessionService;
            TransactionManager = transactionManager;
        }

        protected string TableName => DataTable.tableName;
        protected string ViewName => DataTable.viewName;


        public void InitCommands()
        {

            DataTable = new TDataTable();
            if (DataAdapter is SqlDataAdapter sqlAdapter)
            {
                sqlAdapter.InsertCommand = CreateInsertCommand();
                sqlAdapter.UpdateCommand = CreateUpdateCommand();
                sqlAdapter.DeleteCommand = CreateDeleteCommand();
                sqlAdapter.SelectCommand = CreateSelectCommand();
            }
            else
            {
                throw new InvalidOperationException("Only SqlDataAdapter is supported in this base.");
            }
        }

        public virtual async Task<DataTable> GetByIdAsync(int id)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    await Connection.OpenAsync();

                using (var command = Connection.CreateCommand())
                {
                    command.Transaction = Transaction;
                    command.CommandText = $"SELECT * FROM [{TableName}] WHERE [Id] = @Id";
                    command.CommandType = CommandType.Text;

                    var param = command.CreateParameter();
                    param.ParameterName = "@Id";
                    param.Value = id;
                    command.Parameters.Add(param);

                    var resultTable = new DataTable();
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        await Task.Run(() => adapter.Fill(resultTable));
                    }

                    return resultTable;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error retrieving row from {TableName} by ID: {Id}", TableName, id);

                throw new DatabaseException(ex.Message, "cannot GetByIdAsync Track", ErrorCode.DataBaseError, ex);
            }
        }

        public virtual async Task<int> DeleteByIdAsync(int id)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    await Connection.OpenAsync();

                using (var command = Connection.CreateCommand())
                {
                    command.Transaction = Transaction;
                    command.CommandText = $"DELETE FROM [{TableName}] WHERE [Id] = @Id";
                    command.CommandType = CommandType.Text;

                    var param = command.CreateParameter();
                    param.ParameterName = "@Id";
                    param.Value = id;
                    command.Parameters.Add(param);

                    if (Transaction != null)
                    {
                        command.Transaction = Transaction;
                    }

                    return await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error retrieving row from {TableName} by ID: {Id}", TableName, id);

                throw new DatabaseException(ex.Message, "cannot DeleteByIdAsync", ErrorCode.DataBaseError, ex);
            }
        }



        public virtual async Task<int> FillAsync(DataTable dataTable)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    await Connection.OpenAsync();

                ApplyTransactionToCommands(Transaction);

                return await Task.Run(() => DataAdapter.Fill(dataTable));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error filling DataTable for {TableName}", TableName);
                throw new DatabaseException(ex.Message, "cannot GetByIdAsync Track", ErrorCode.DataBaseError, ex);

            }
        }

        public virtual async Task<TDataTable> FillAsyncByCommand(TDataTable dataTable, SqlCommand command)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    await Connection.OpenAsync();


                using (var adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = command;

                    await Task.Run(() => adapter.Fill(dataTable));
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error filling DataTable for {TableName}", TableName);
                throw;
            }
        }

        public virtual async Task<TDataTable> FetchAsyncByCommand(SqlCommand command)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    await Connection.OpenAsync();


                var result = new TDataTable();


                using (var adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = command;

                    await Task.Run(() => adapter.Fill(result));
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error filling DataTable for {TableName}", TableName);
                throw;
            }
        }


        public virtual async Task<int> UpdateAsync(DataTable dataTable)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    await Connection.OpenAsync();

                ApplyViewSafety(dataTable);

                ApplyAuditFields(dataTable);

                ApplyTransactionToCommands(Transaction);
                return await Task.Run(() => DataAdapter.Update(dataTable));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating DataTable for {TableName}", TableName);

                throw new DatabaseException(ex.Message, "cannot UpdateAsync Track", ErrorCode.DataBaseError, ex);
            }
        }

        public virtual async Task<int> UpdateAsync(DataSet dataSet, string tableName)
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    await Connection.OpenAsync();

                if (dataSet.Tables.Contains(tableName))
                {
                    var table = dataSet.Tables[tableName];
                    ApplyAuditFields(table);
                    return await Task.Run(() => DataAdapter.Update(dataSet, tableName));
                }

                return 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating DataSet table {TableName}", tableName);
                
                throw new DatabaseException(ex.Message, "cannot UpdateAsync Track", ErrorCode.DataBaseError, ex);

            }
        }

        public virtual async Task<TDataTable> FetchTypedAsync()
        {
            try
            {
                if (Connection.State != ConnectionState.Open)
                    await Connection.OpenAsync();

                var result = new TDataTable();

                await Task.Run(() => DataAdapter.Fill(result));


                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error fetching typed data for {TableName}", TableName);


                throw new DatabaseException(ex.Message, "cannot FetchTypedAsync Track", ErrorCode.DataBaseError, ex);
            }
        }


        //TODO
        public virtual void SetAuditUser(int userId)
        {
            AuditUserId = userId;
        }

        protected virtual void ApplyAuditFields(DataTable dataTable)
        {
            var currentUserId = AuditUserId ?? SessionService?.CurrentUser?.Id ?? 0;
            var currentTime = DateTime.Now;

            foreach (DataRow row in dataTable.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (dataTable.Columns.Contains("CreatedAt") &&
                        (row["CreatedAt"] == DBNull.Value || row["CreatedAt"] == null))
                        row["CreatedAt"] = currentTime;

                    if (dataTable.Columns.Contains("CreatedBy") &&
                        (row["CreatedBy"] == DBNull.Value || row["CreatedBy"] == null))
                        row["CreatedBy"] = currentUserId;
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    if (dataTable.Columns.Contains("UpdatedAt"))
                        row["UpdatedAt"] = currentTime;

                    if (dataTable.Columns.Contains("UpdatedBy"))
                        row["UpdatedBy"] = currentUserId;
                }
            }
        }

        protected virtual void ApplyViewSafety(DataTable dataTable)
        {
            var columnsToIgnore = dataTable.Columns
                .Cast<DataColumn>()
                .Where(col => col.ExtendedProperties.ContainsKey("IsViewColumn"))
                .ToList();

            foreach (var column in columnsToIgnore)
            {
                column.ReadOnly = true;
            }
        }

        protected abstract void ApplyTransactionToCommands(SqlTransaction transaction);

        public virtual void Dispose()
        {
            try
            {
                Transaction?.Dispose();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error disposing enhanced adapter for {TableName}", TableName);
            }
        }



        #region Parameter creation 
        protected SqlCommand CreateInsertCommand()
        {
            var insertColumns = DataTable.Columns
                .Cast<DataColumn>()
                .Where(col => !(col.ExtendedProperties.ContainsKey("IsViewColumn") && (bool)col.ExtendedProperties["IsViewColumn"]))
                .Where(col => !col.AutoIncrement)
                .ToList();

            var columnNames = string.Join(", ", insertColumns.Select(c => $"[{c.ColumnName}]"));
            var paramNames = string.Join(", ", insertColumns.Select(c => $"@{c.ColumnName}"));
            string t = "";

            var cmd = new SqlCommand
            {
                CommandText = $"INSERT INTO [{TableName}] ({columnNames}) {t} VALUES ({paramNames})",
                CommandType = CommandType.Text,
                Connection = Connection
            };

            foreach (var col in insertColumns)
            {
                cmd.Parameters.Add($"@{col.ColumnName}", GetSqlDbType(col)).SourceColumn = col.ColumnName;
            }




            return cmd;
        }



        protected SqlCommand CreateDeleteCommand()
        {
            var idColumn = DataTable.Columns["Id"];
            if (idColumn == null)
                throw new InvalidOperationException("Table must have a primary key column named 'Id'.");

            var cmd = new SqlCommand
            {
                CommandText = $"DELETE FROM [{TableName}] WHERE [Id] = @Id",
                CommandType = CommandType.Text,
                Connection = Connection
            };

            cmd.Parameters.Add("@Id", GetSqlDbType(idColumn)).SourceColumn = "Id";

            return cmd;
        }

        protected SqlCommand CreateUpdateCommand()
        {
            var updatableColumns = DataTable.Columns
                .Cast<DataColumn>()
                .Where(col => !(col.ExtendedProperties.ContainsKey("IsViewColumn") && (bool)col.ExtendedProperties["IsViewColumn"]))
                .Where(col => !col.AutoIncrement && col.ColumnName != "Id")
                .ToList();

            var setClause = string.Join(", ", updatableColumns.Select(c => $"[{c.ColumnName}] = @{c.ColumnName}"));

            var cmd = new SqlCommand
            {
                CommandText = $"UPDATE [{TableName}] SET {setClause} WHERE [Id] = @Id",
                CommandType = CommandType.Text,
                Connection = Connection
            };

            foreach (var col in updatableColumns)
            {
                cmd.Parameters.Add($"@{col.ColumnName}", GetSqlDbType(col)).SourceColumn = col.ColumnName;
            }

            var idColumn = DataTable.Columns["Id"];
            if (idColumn == null)
                throw new InvalidOperationException("Table must have a primary key column named 'Id'.");

            cmd.Parameters.Add("@Id", GetSqlDbType(idColumn)).SourceColumn = "Id";

            return cmd;
        }





        protected SqlCommand CreateSelectCommand()
        {
            var cmd = new SqlCommand
            {
                CommandText = $"SELECT * FROM [{ViewName}]",
                CommandType = CommandType.Text,
                Connection = Connection
            };

            return cmd;
        }






        protected SqlDbType GetSqlDbType(DataColumn column)
        {
            var type = column.DataType;

            if (type == typeof(string))
                return SqlDbType.NVarChar;
            if (type == typeof(int))
                return SqlDbType.Int;
            if (type == typeof(long))
                return SqlDbType.BigInt;
            if (type == typeof(short))
                return SqlDbType.SmallInt;
            if (type == typeof(byte))
                return SqlDbType.TinyInt;
            if (type == typeof(bool))
                return SqlDbType.Bit;
            if (type == typeof(DateTime))
                return SqlDbType.DateTime;
            if (type == typeof(decimal))
                return SqlDbType.Decimal;
            if (type == typeof(double))
                return SqlDbType.Float;
            if (type == typeof(float))
                return SqlDbType.Real;
            if (type == typeof(Guid))
                return SqlDbType.UniqueIdentifier;
            if (type == typeof(byte[]))
                return SqlDbType.VarBinary;

            throw new NotSupportedException($"Type '{type.FullName}' is not supported for SqlDbType mapping.");
        }



        #endregion
    }
}
