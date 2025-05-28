using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Domain.SharedSevices
{
    public interface ITransactionManager : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        SqlConnection GetConnection();
        SqlTransaction GetTransaction();
        bool HasActiveTransaction { get; }
    }
}