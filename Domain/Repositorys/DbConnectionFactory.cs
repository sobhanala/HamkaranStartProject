using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Domain.Repositorys
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public SqlConnection CreateConnection()
        {
        
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            
         
        }

        public SqlCommand CreateCommand(string commandText, CommandType commandType, IDbConnection connection)
        {
    
                var command = new SqlCommand(commandText, (SqlConnection)connection);
                command.CommandType = commandType;
                return command;
            
           
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