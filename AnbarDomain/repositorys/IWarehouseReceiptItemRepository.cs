using System.Threading.Tasks;
using AnbarDomain.Tabels;
using Domain.Repositorys;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptItemRepository:IEnhancedTableAdapter
    {
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FetchByReceiptIdWithProductInfo(int receiptId);

    }
}