using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Domain.Attribute;
using Domain.Exceptions;
using Domain.SharedSevices;
using Microsoft.Extensions.Logging;

namespace Domain.Repositorys
{
    public abstract class TypedDataSetRepository<TEntity, TKey, TDataSet> : BaseRepository, IGenericRepository<TEntity, TKey,TDataSet>
        where TEntity : class
        where TDataSet : DataSet, new()
    {
        private readonly ILogger<TypedDataSetRepository<TEntity, TKey, TDataSet>> _logger;

        protected readonly string TableName;
        protected readonly string KeyColumn;
        protected readonly string[] TableColumns;
        private readonly  ISessionService _sessionService;
        protected TypedDataSetRepository(
            DbConnectionFactory connectionFactory,
            string tableName,
            string keyColumn,
            string[] tableColumns, ILogger<TypedDataSetRepository<TEntity, TKey, TDataSet>> logger, ISessionService sessionService) : base(connectionFactory,sessionService)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            KeyColumn = keyColumn ?? throw new ArgumentNullException(nameof(keyColumn));
            TableColumns = tableColumns ?? throw new ArgumentNullException(nameof(tableColumns));
            _logger = logger;
            _sessionService = sessionService;
        }


        public virtual async Task<int> SaveChangesFromDataSet(TDataSet dataSet)
        {
            try
            {
                int totalChanges = 0;

                foreach (DataTable table in dataSet.Tables)
                {
                    if (table.GetChanges() == null) continue;

                    totalChanges += await SaveChangesFromDataTable(table);
                }

                if (totalChanges > 0)
                {
                    dataSet.AcceptChanges();
                }

                return totalChanges;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveChangesFromDataSet failed for dataset {DataSetType}", typeof(TDataSet).Name);
                throw new DatabaseException("SaveChangesFromDataSet failed",
                    $"Error while saving dataset: {typeof(TDataSet).Name}", ErrorCode.DataBaseError, ex);
            }
        }


        public virtual async Task<int> SaveChangesFromDataTable(DataTable table)
        {
            try
            {
                var commands = new Dictionary<string, SqlCommand>();
                AuditiseTable(table);


                var insertCommand = new SqlCommand(
                    GenerateInsertQuery(TableName, TableColumns.Where(c => c != KeyColumn).ToList()));
                AddParameters(insertCommand, table, exclude: new[] { KeyColumn });

                var updateCommand = new SqlCommand(
                    GenerateUpdateQuery(TableName, TableColumns, KeyColumn));
                AddParameters(updateCommand, table);
                updateCommand.Parameters.Add($"@{KeyColumn}", GetSqlDbType(table.Columns[KeyColumn].DataType), 0, KeyColumn);

                var deleteCommand = new SqlCommand(
                    GenerateDeleteQuery(TableName, KeyColumn));
                deleteCommand.Parameters.Add($"@{KeyColumn}", GetSqlDbType(table.Columns[KeyColumn].DataType), 0, KeyColumn);

                commands.Add("Insert", insertCommand);
                commands.Add("Update", updateCommand);
                commands.Add("Delete", deleteCommand);

                return await ExecuteDataAdapterUpdateAsyncOnDataTabel(table, commands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveChangesFromDataTable failed for table {TableName}", TableName);
                throw new DatabaseException("SaveChangesFromDataTable failed",
                    $"Error while saving data for table: {TableName}", ErrorCode.DataBaseError, ex);
            }
        }

        private void AuditiseTable(DataTable table)
        {
            bool isAuditable = typeof(IAuditable).IsAssignableFrom(table.GetType());

            if (isAuditable)
            {
                foreach (DataRow row in table.Rows)
                {
                    SetAuditFields(row); 
                }
            }
        }

        public virtual async Task<TDataSet> GetDataSetAsync()
        {
            try
            {
                var query = GenerateSelectQuery(TableName, TableColumns);
                return await ExecuteTypedDataSetAsync<TDataSet>(query, CommandType.Text, TableName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetDataSetAsync failed for table {TableName}", TableName);
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                var query = GenerateSelectQuery(TableName, TableColumns);
                var dataSet = await ExecuteTypedDataSetAsync<TDataSet>(query, CommandType.Text, TableName);
                return MapResultsToEntities(dataSet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync failed for table {TableName}", TableName);
                throw;
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            try
            {
                var query = GenerateSelectQuery(TableName, TableColumns, $"{KeyColumn} = @{KeyColumn}");
                var parameter = CreateParameter($"@{KeyColumn}", id, GetDbType(id));

                var dataSet = await ExecuteTypedDataSetAsync<TDataSet>(
                    query,
                    CommandType.Text,
                    TableName,
                    parameter);

                return MapSingleResultToEntity(dataSet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByIdAsync failed for table {TableName}, ID: {Id}", TableName, id);
                throw;
            }
        }

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            try
            {
                var insertColumns = TableColumns.Where(c => c != KeyColumn).ToList();
                var query = GenerateInsertQuery(TableName, insertColumns);
                var parameters = CreateParametersFromEntity(entity);
                return await ExecuteWriterCommandAsync(query, CommandType.Text, parameters.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "InsertAsync failed for table {TableName}", TableName);
                throw;
            }
        }

        public virtual async Task<int> UpdateAsync(TEntity entity, string keyColumn = "")
        {
            try
            {
                if (string.IsNullOrEmpty(keyColumn))
                {
                    keyColumn = KeyColumn;
                }

                var query = GenerateUpdateQuery(TableName, TableColumns, keyColumn);
                var parameters = CreateParametersFromEntity(entity);
                return await ExecuteWriterCommandAsync(query, CommandType.Text, parameters.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync failed for table {TableName}", TableName);
                throw;
            }
        }

        public virtual async Task<int> DeleteAsync(TKey id, string keyColumn = "")
        {
            try
            {
                if (string.IsNullOrEmpty(keyColumn))
                {
                    keyColumn = KeyColumn;
                }

                var query = GenerateDeleteQuery(TableName, keyColumn);
                var parameter = CreateParameter($"@{keyColumn}", id, GetDbType(id));
                return await ExecuteWriterCommandAsync(query, CommandType.Text, parameter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync failed for table {TableName}, ID: {Id}", TableName, id);
                throw;
            }
        }

        protected abstract IEnumerable<TEntity> MapResultsToEntities(TDataSet dataSet);

        protected abstract TEntity MapSingleResultToEntity(TDataSet dataSet);

        protected abstract IEnumerable<SqlParameter> CreateParametersFromEntity(TEntity entity);

        protected virtual DbType GetDbType(object value)
        {
            if (value is int) return DbType.Int32;
            if (value is string) return DbType.String;
            if (value is DateTime) return DbType.DateTime;
            if (value is bool) return DbType.Boolean;
            if (value is byte) return DbType.Byte;

            return DbType.Object;
        }

        protected SqlDbType GetSqlDbType(Type type)
        {
            if (type == typeof(string)) return SqlDbType.NVarChar;
            if (type == typeof(int)) return SqlDbType.Int;
            if (type == typeof(decimal)) return SqlDbType.Decimal;
            if (type == typeof(DateTime)) return SqlDbType.DateTime;
            if (type == typeof(bool)) return SqlDbType.Bit;
            if (type == typeof(byte[])) return SqlDbType.VarBinary;
            if (type == typeof(Guid)) return SqlDbType.UniqueIdentifier;
            if (type == typeof(byte)) return SqlDbType.TinyInt;         


            throw new NotSupportedException($"Unsupported data type: {type.FullName}");
        }

        protected void AddParameters(SqlCommand command, DataTable table, IEnumerable<string> exclude = null)
        {
            var excludeSet = exclude != null ? new HashSet<string>(exclude) : new HashSet<string>();

            foreach (DataColumn column in table.Columns)
            {
                if (excludeSet.Contains(column.ColumnName)) continue;

                var param = command.Parameters.Add($"@{column.ColumnName}", GetSqlDbType(column.DataType), 0, column.ColumnName);
                param.IsNullable = column.AllowDBNull;
            }
        }

        private void SetAuditFields(DataRow row)
        {
            if (row.RowState == DataRowState.Added)
            {
                if (row.Table.Columns.Contains("CreatedAt") && row["CreatedAt"] == DBNull.Value)
                    row["CreatedAt"] = DateTime.Now;

                if (row.Table.Columns.Contains("CreatedBy") && row["CreatedBy"] == DBNull.Value)
                    row["CreatedBy"] = _sessionService.CurrentUser.Id;
            }
            else if (row.RowState == DataRowState.Modified) 
            {
                if (row.Table.Columns.Contains("UpdatedAt"))
                    row["UpdatedAt"] = DateTime.Now;

                if (row.Table.Columns.Contains("UpdatedBy"))
                    row["UpdatedBy"] = _sessionService.CurrentUser.Id; 
            }
        }

    }
}
