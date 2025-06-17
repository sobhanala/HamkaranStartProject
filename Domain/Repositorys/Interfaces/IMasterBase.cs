using System.Threading.Tasks;

namespace Domain.Repositorys.Interfaces
{
    public interface IMasterBase : IEnhancedTableAdapter
    {
        Task<int> GetLastInsertedReceiptIdAsync();

    }
}