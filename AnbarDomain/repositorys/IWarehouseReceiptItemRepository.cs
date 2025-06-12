using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;
using Domain.Repositorys.Interfaces;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptItemRepository : IEnhancedTableAdapter
    {
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FetchByForeignKeyAsync(int receiptId);
       
        

    }
}