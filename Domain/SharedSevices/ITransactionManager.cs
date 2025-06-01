using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Domain.SharedSevices
{
    public interface ITransactionManager : IDisposable
    {
        void BeginTransactionAsync();
        void CommitTransactionAsync();
        void RollbackTransactionAsync();
        SqlConnection GetConnection();
        SqlTransaction GetTransaction();
        bool HasActiveTransaction { get; }
    }
}