using AnbarDomain.Orders;
using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptRepository : IGenericRepository<Receipt, int, AnbarDataSet>
    {
        Task<string> GetNextReceiptNumberAsync();
    }
}