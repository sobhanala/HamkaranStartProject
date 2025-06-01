using Domain.Attribute;
using Domain.SharedSevices;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Repositorys
{

    [Service]
    public class TransactionManager : ITransactionManager
    {
        private readonly ILogger<TransactionManager> _logger;
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private bool _disposed = false;
        private DbConnectionFactory _factory;
        public TransactionManager(DbConnectionFactory factory, ILogger<TransactionManager> logger)
        {
            this._factory = factory;
            _logger = logger;
        }

        public bool HasActiveTransaction => _transaction != null;

        public SqlConnection GetConnection()
        {
            if (_connection != null)
            {
                return _connection;

            }

            _connection = _factory.CreateConnection();
            return _connection;
        }

        public SqlTransaction GetTransaction()
        {
            return _transaction;
        }

        public void BeginTransactionAsync()
        {
            try
            {
                if (_connection == null)
                {
                    _connection = _factory.CreateConnection();
                }

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.OpenAsync();
                }

                if (_transaction == null)
                {
                    _transaction = _connection.BeginTransaction();
                    _logger.LogDebug("Transaction started");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error beginning transaction");
                throw;
            }
        }

        public void CommitTransactionAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    _transaction.Commit();
                    _transaction.Dispose();
                    _transaction = null;
                    _logger.LogDebug("Transaction committed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error committing transaction");
                RollbackTransactionAsync();
                throw;
            }
        }

        public void RollbackTransactionAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction.Dispose();
                    _transaction = null;
                    _logger.LogDebug("Transaction rolled back");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rolling back transaction");
                throw;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                try
                {
                    _transaction?.Rollback();
                    _transaction?.Dispose();
                    _connection?.Close();
                    _connection?.Dispose();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error disposing transaction manager");
                }
                finally
                {
                    _disposed = true;
                }
            }
        }
    }
}
