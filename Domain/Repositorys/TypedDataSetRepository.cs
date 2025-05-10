using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositorys
{
    public abstract class TypedDataSetRepository<TEntity, TKey, TDataSet> : BaseRepository, IGenericRepository<TEntity, TKey>
        where TEntity : class
        where TDataSet : DataSet, new()
    {
        protected readonly string TableName;
        protected readonly string KeyColumn;
        protected readonly string[] TableColumns;

        protected TypedDataSetRepository(
            DbConnectionFactory connectionFactory,
            string tableName,
            string keyColumn,
            string[] tableColumns) : base(connectionFactory)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            KeyColumn = keyColumn ?? throw new ArgumentNullException(nameof(keyColumn));
            TableColumns = tableColumns ?? throw new ArgumentNullException(nameof(tableColumns));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var query = GenerateSelectQuery(TableName, TableColumns);
            var dataSet = await ExecuteTypedDataSetAsync<TDataSet>(query, CommandType.Text, TableName);
            return MapResultsToEntities(dataSet);
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
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

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            var insertColumns = TableColumns.Where(c => c != KeyColumn).ToList();

            var query = GenerateInsertQuery(TableName, insertColumns);

            var parameters = CreateParametersFromEntity(entity);

            return await ExecuteWriterCommandAsync(query, CommandType.Text, parameters.ToArray());
        }

        public virtual async Task<int> UpdateAsync(TEntity entity, string keyColumn="")
        {
            if (string.IsNullOrEmpty(keyColumn))
            {
                keyColumn = KeyColumn;
            }
            var query = GenerateUpdateQuery(TableName, TableColumns, keyColumn);
            var parameters = CreateParametersFromEntity(entity);

            return await ExecuteWriterCommandAsync(query, CommandType.Text, parameters.ToArray());
        }

        public virtual async Task<int> DeleteAsync(TKey id, string keyColumn="")
        {
            if (string.IsNullOrEmpty(keyColumn))
            {
                keyColumn = KeyColumn;
            }
            var query = GenerateDeleteQuery(TableName, keyColumn);
            var parameter = CreateParameter($"@{keyColumn}", id, GetDbType(id));

            return await ExecuteWriterCommandAsync(query, CommandType.Text, parameter);
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

        //public virtual async Task SyncDataTableAsync(DataTable dataTable)
        //{
        //    if (dataTable == null || dataTable.TableName != TableName)
        //        throw new ArgumentException("Invalid table passed to sync.");

        //    using var connection = ConnectionFactory.CreateConnection();
        //    await connection.OpenAsync();

        //    using var adapter = new SqlDataAdapter();

        //    var insertCols = TableColumns.Where(c => c != KeyColumn).ToArray();
        //    var insertQuery = $"INSERT INTO {TableName} ({string.Join(", ", insertCols)}) VALUES ({string.Join(", ", insertCols.Select(c => "@" + c))})";
        //    var insertCmd = new SqlCommand(insertQuery, (SqlConnection)connection);
        //    foreach (var col in insertCols)
        //        insertCmd.Parameters.Add("@" + col, SqlDbTypeFromName(dataTable.Columns[col].DataType)).SourceColumn = col;
        //    adapter.InsertCommand = insertCmd;

        //    // UPDATE
        //    var updateSet = string.Join(", ", TableColumns.Where(c => c != KeyColumn).Select(c => $"{c} = @{c}"));
        //    var updateQuery = $"UPDATE {TableName} SET {updateSet} WHERE {KeyColumn} = @{KeyColumn}";
        //    var updateCmd = new SqlCommand(updateQuery, (SqlConnection)connection);
        //    foreach (var col in TableColumns)
        //        updateCmd.Parameters.Add("@" + col, SqlDbTypeFromName(dataTable.Columns[col].DataType)).SourceColumn = col;
        //    adapter.UpdateCommand = updateCmd;

        //    // DELETE
        //    var deleteCmd = new SqlCommand($"DELETE FROM {TableName} WHERE {KeyColumn} = @{KeyColumn}", (SqlConnection)connection);
        //    deleteCmd.Parameters.Add("@" + KeyColumn, SqlDbTypeFromName(dataTable.Columns[KeyColumn].DataType)).SourceColumn = KeyColumn;
        //    adapter.DeleteCommand = deleteCmd;

        //    adapter.Update(dataTable);
        //}
    }
}
