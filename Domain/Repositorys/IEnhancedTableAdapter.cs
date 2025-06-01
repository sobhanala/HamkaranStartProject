using System;
using System.Data;
using System.Threading.Tasks;

namespace Domain.Repositorys
{
    public interface IEnhancedTableAdapter : IDisposable
    {
        Task<int> FillAsync(DataTable dataTable);
        Task<int> UpdateAsync(DataTable dataTable);
        Task<int> UpdateAsync(DataSet dataSet, string tableName);
        void SetAuditUser(int userId);
        Task<DataTable> GetByIdAsync(int id);
    }
}