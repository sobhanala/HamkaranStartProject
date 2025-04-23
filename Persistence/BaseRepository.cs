using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence
{
    public abstract class BaseRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        protected BaseRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        protected async Task<IEnumerable<T>> ExecuteReaderQueryAsync<T>(string commandText, CommandType type,
            Func<IDataReader, T> mapFunc, params IDbDataParameter[] parameters)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = _connectionFactory.CreateCommand(commandText, type, connection))
            {
                if (parameters != null)
                    foreach (var param in parameters)
                        command.Parameters.Add(param);


                var items = new List<T>();
                using (var reader = await Task.Run(() => command.ExecuteReader()))
                {
                    while (reader.Read()) items.Add(mapFunc(reader));
                }


                return items;
            }
        }

        protected async Task<int> ExecuteWriterCommandAsync(string commandText, CommandType type,
            params IDataParameter[] parameters)
        {
            using (var connection = _connectionFactory.CreateConnection())

            using (var command = _connectionFactory.CreateCommand(commandText, type, connection))
            {
                if (parameters != null)
                    foreach (var param in parameters)
                        command.Parameters.Add(param);

                return await Task.Run(() => command.ExecuteNonQuery());
            }
        }

        protected async Task<T> ExecuteScalarAsync<T>(string commandText, CommandType commandType,
            params IDbDataParameter[] parameters)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = _connectionFactory.CreateCommand(commandText, commandType, connection))
            {
                if (parameters != null)
                    foreach (var parameter in parameters)
                        command.Parameters.Add(parameter);

                var result = await Task.Run(() => command.ExecuteScalar());
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }

        protected IDbDataParameter CreateParameter(string name, object value, DbType dbType)
        {
            return _connectionFactory.CreateParameter(name, value, dbType);
        }
    }
}