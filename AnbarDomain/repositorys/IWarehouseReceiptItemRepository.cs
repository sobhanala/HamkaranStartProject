using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptItemRepository : IEnhancedTableAdapter
    {
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FetchByReceiptIdWithProductInfo(int receiptId);
        Task<int> DeleteByReciteInfo(int receiptId);

        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(
            AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable table, int receiptId);

    }
}