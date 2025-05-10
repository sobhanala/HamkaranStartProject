using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Domain.Repositorys
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;
        private readonly ILogger<DbConnectionFactory> _logger;

        public DbConnectionFactory(string connectionString, ILogger<DbConnectionFactory> logger)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _logger = logger;
        }

        public SqlConnection CreateConnection()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                _logger.LogCritical("Cant Create the Connection {1}", e.Message);
                throw;
            }
        }

        public SqlCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            try
            {
                var command = new SqlCommand(commandText, (SqlConnection)connection);
                command.CommandType = commandType;
                return command;
            }
            catch (Exception e)
            {
                _logger.LogCritical("Cant CreateCommand {1}", e.Message);
                throw;
            }
        }

        public SqlParameter CreateParameter(string name, object value, DbType dbType)
        {
            return new SqlParameter
            {
                ParameterName = name,
                Value = value ?? DBNull.Value,
                DbType = dbType,
            };
        }
        public SqlDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }
    }
}