﻿using Domain.SharedSevices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Domain.Repositorys
{
    public abstract class BaseRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        protected BaseRepository(DbConnectionFactory connectionFactory, ISessionService sessionService)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }


        public void MarkReadOnlyColumnsFromSchema(SqlConnection sqlConnection, string tableName, DataTable dataTable)
        {

            using (var command = new SqlCommand($"SELECT * FROM {tableName} WHERE 1 = 0", sqlConnection))
            {
                using (var adapter = new SqlDataAdapter(command))
                {
                    var schemaTable = new DataTable();
                    adapter.FillSchema(schemaTable, SchemaType.Source);

                    foreach (DataColumn schemaColumn in schemaTable.Columns)
                    {
                        var originalColumn = dataTable.Columns[schemaColumn.ColumnName];
                        if (originalColumn != null && schemaColumn.ReadOnly)
                        {
                            originalColumn.ReadOnly = true;
                        }
                    }
                }
            }
        }

        protected async Task<T> ExecuteTypedDataSetAsync<T>(
            string commandText,
            CommandType type,
            string tableName = "",
            params SqlParameter[] parameters) where T : DataSet, new()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = _connectionFactory.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(param);
                        }
                    }

                    using (var dataAdapter = _connectionFactory.CreateDataAdapter(command))
                    {
                        var dataset = new T();

                        await Task.Run(() =>
                        {
                            if (!string.IsNullOrEmpty(tableName))
                            {
                                dataAdapter.Fill(dataset, tableName);
                            }
                            else
                            {
                                dataAdapter.Fill(dataset);
                            }
                        });

                        return dataset;
                    }
                }
            }
        }

        protected async Task<int> ExecuteDataAdapterUpdateAsyncOnDataset<T>(
            T dataSet,
            string tableName,
            Dictionary<string, SqlCommand> commands = null) where T : DataSet
        {
            var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var adapter = new SqlDataAdapter();

            try
            {
                if (commands != null)
                {
                    if (commands.TryGetValue("Insert", out var insertCommand))
                    {
                        insertCommand.Connection = connection;
                        adapter.InsertCommand = insertCommand;
                    }

                    if (commands.TryGetValue("Update", out var updateCommand))
                    {
                        updateCommand.Connection = connection;
                        adapter.UpdateCommand = updateCommand;
                    }

                    if (commands.TryGetValue("Delete", out var deleteCommand))
                    {
                        deleteCommand.Connection = connection;
                        adapter.DeleteCommand = deleteCommand;
                    }
                }

                return adapter.Update(dataSet, tableName);
            }
            finally
            {
                adapter.Dispose();
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }

        protected async Task<int> ExecuteDataAdapterUpdateAsyncOnDataTabel<T>(
            T dataTable,
            Dictionary<string, SqlCommand> commands = null) where T : DataTable
        {
            var connection = _connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var adapter = new SqlDataAdapter();

            try
            {
                if (commands != null)
                {
                    if (commands.TryGetValue("Insert", out var insertCommand))
                    {
                        insertCommand.Connection = connection;
                        adapter.InsertCommand = insertCommand;
                        foreach (SqlParameter p in insertCommand.Parameters)
                        {
                            Debug.WriteLine($"Param Insert : {p.ParameterName}");
                        }
                    }

                    if (commands.TryGetValue("Update", out var updateCommand))
                    {
                        updateCommand.Connection = connection;
                        adapter.UpdateCommand = updateCommand;
                        foreach (SqlParameter p in updateCommand.Parameters)
                        {
                            Debug.WriteLine($"Param Update : {p.ParameterName}");
                        }
                    }

                    if (commands.TryGetValue("Delete", out var deleteCommand))
                    {
                        deleteCommand.Connection = connection;
                        adapter.DeleteCommand = deleteCommand;
                        foreach (SqlParameter p in deleteCommand.Parameters)
                        {
                            Debug.WriteLine($"Param Delete: {p.ParameterName}");
                        }
                    }
                }
                MarkReadOnlyColumnsFromSchema(connection, dataTable.TableName, dataTable);



                return adapter.Update(dataTable);
            }
            catch (Exception ex)
            {

                Console.Error.WriteLine($"Error updating DataTable: {ex.Message}");
                throw;
            }
            finally
            {
                adapter.Dispose();
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }

        protected async Task<int> ExecuteWriterCommandAsync(string commandText, CommandType type,
            params SqlParameter[] parameters)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = _connectionFactory.CreateCommand(commandText, type, connection))
                {
                    if (parameters != null)
                        foreach (var param in parameters)
                            command.Parameters.Add(param);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        protected async Task<T> ExecuteScalarAsync<T>(string commandText, CommandType commandType,
            params SqlParameter[] parameters)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = _connectionFactory.CreateCommand(commandText, commandType, connection))
                {
                    if (parameters != null)
                        foreach (var parameter in parameters)
                            command.Parameters.Add(parameter);

                    var result = await command.ExecuteScalarAsync();
                    if (result == DBNull.Value)
                    {
                        return default(T);
                    }
                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
        }

        protected SqlParameter CreateParameter(string name, object value, DbType dbType)
        {
            return _connectionFactory.CreateParameter(name, value, dbType);
        }

        protected SqlCommand CreateCommand(string commandText, CommandType type)
        {
            return _connectionFactory.CreateCommand(commandText, type, _connectionFactory.CreateConnection());
        }
        public static string[] GetColumnNames(DataTable table)
        {
            return table.Columns.Cast<DataColumn>()
                .Select(col => col.ColumnName)
                .ToArray();
        }
        #region SQL Command Generation


        protected string GenerateSelectQuery(string tableName, IEnumerable<string> columns, string whereClause = null)
        {
            var enumerable = columns as string[] ?? columns.ToArray();

            var columnList = enumerable.FirstOrDefault() == null ? "*" : string.Join(", ", enumerable);

            var query = $"SELECT {columnList} FROM {tableName}";



            if (!string.IsNullOrEmpty(whereClause))
            {
                query += $" WHERE {whereClause}";
            }

            return query;
        }


        protected string GenerateInsertQuery(string tableName, IEnumerable<string> columns)
        {
            var columnList = string.Join(", ", columns);
            var parameterList = string.Join(", ", columns.Select(c => $"@{c}"));

            return $"INSERT INTO {tableName} ({columnList}) VALUES ({parameterList})";
        }


        protected string GenerateUpdateQuery(string tableName, IEnumerable<string> columns, string keyColumn)
        {
            var setClause = string.Join(", ", columns.Where(c => c != keyColumn).Select(c => $"{c} = @{c}"));

            return $"UPDATE {tableName} SET {setClause} WHERE {keyColumn} = @{keyColumn}";
        }


        protected string GenerateDeleteQuery(string tableName, string keyColumn)
        {
            return $"DELETE FROM {tableName} WHERE {keyColumn} = @{keyColumn}";
        }

        protected SqlCommand BuildUpdateCommand(string tableName, DataTable table, string keyColumn)
        {
            var editableColumns = table.Columns
                .Cast<DataColumn>()
                .Where(col =>
                    !(col.ExtendedProperties["IsViewColumn"] is bool isView && isView))
                .Select(col => col.ColumnName)
                .ToList();

            var query = GenerateUpdateQuery(tableName, editableColumns, keyColumn);

            var command = new SqlCommand(query);

            foreach (var column in editableColumns)
            {
                command.Parameters.Add(new SqlParameter($"@{column}", table.Columns[column].DataType)
                {
                    SourceColumn = column,
                    SourceVersion = DataRowVersion.Current
                });
            }

            return command;
        }
        protected SqlCommand BuildInsertCommand(string tableName, DataTable table, string KeyColumn)
        {
            var editableColumns = table.Columns
                .Cast<DataColumn>()
                .Where(col =>
                    !col.ReadOnly &&
                    !(col.ExtendedProperties["IsViewColumn"] is bool isView && isView))
                .Select(col => col.ColumnName)
                .ToList();

            var query = GenerateInsertQuery(tableName, editableColumns.Where(c => c != KeyColumn));

            var command = new SqlCommand(query);

            foreach (var column in editableColumns)
            {
                command.Parameters.Add(new SqlParameter($"@{column}", table.Columns[column].DataType)
                {
                    SourceColumn = column,
                    SourceVersion = DataRowVersion.Current
                });
            }

            return command;
        }


        #endregion
    }
}