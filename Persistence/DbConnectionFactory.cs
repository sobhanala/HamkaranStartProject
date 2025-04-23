using System;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString; 

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateConnection()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine("Cant Create the Connection {1}", e.Message);
                throw;
            }
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
            try
            {
                var command = new SqlCommand(commandText, (SqlConnection)connection);
                command.CommandType = commandType;
                return command;
            }
            catch (Exception e)
            {
                Console.WriteLine("Cant CreateCommand {1}", e.Message);
                throw;
            }
        }

        public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
        {
            return new SqlParameter
            {
                ParameterName = name,
                Value = value ?? DBNull.Value,
                DbType = dbType
            };
        }
    }
}