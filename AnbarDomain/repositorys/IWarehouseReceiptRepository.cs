using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;
using Domain.Repositorys.Interfaces;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptRepository : IEnhancedTableAdapter
    {
        Task<string> GetNextReceiptNumberAsync();
        //Task<int> UpdateTransaction(AnbarDataSet.view_WarehouseReceiptsDataTable data);
        Task<AnbarDataSet.view_WarehouseReceiptsDataTable> FetchAsync();
        Task<int> GetLastInsertedReceiptIdAsync();
    }
}