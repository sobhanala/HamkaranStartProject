using System.Data;
using System.Threading.Tasks;

namespace Domain.Repositorys.Interfaces
{
    public interface IDetailBase : IEnhancedTableAdapter
    {
        Task<int> GetLastInsertedReceiptIdAsync();
        Task FillByForeignKeyAsync(DataTable table, object foreignKeyValue);
        Task<int> DeleteByForeignKeyAsync(object foreignKeyValue);

    }
}