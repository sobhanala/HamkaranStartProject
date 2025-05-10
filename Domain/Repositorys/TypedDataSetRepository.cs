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


        public virtual async Task<TDataSet> GetDataSetAsync()
        {
            var query = GenerateSelectQuery(TableName, TableColumns);
            return await ExecuteTypedDataSetAsync<TDataSet>(query, CommandType.Text, TableName);
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


    }
}
