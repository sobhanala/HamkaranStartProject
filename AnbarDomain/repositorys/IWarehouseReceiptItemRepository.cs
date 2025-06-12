using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptItemRepository : IEnhancedTableAdapter
    {
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FetchByForeignKeyAsync(int receiptId);
       
        

    }
}