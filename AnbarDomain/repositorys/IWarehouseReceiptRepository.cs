using AnbarDomain.Orders;
using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptRepository : IEnhancedTableAdapter
    {
        Task<string> GetNextReceiptNumberAsync();
    }
}