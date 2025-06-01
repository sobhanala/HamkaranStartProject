using AnbarDomain.Tabels;
using System.Threading.Tasks;

namespace AnbarService
{
    public interface IWarehouseReceipt
    {
        Task<AnbarDataSet> GetFullDatasetAsync();
        Task SaveReceiptItemsAndUpdateEwiAsync(AnbarDataSet dataset, int receiptId);
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(int receiptId);
        Task SaveChangesTableAsync(AnbarDataSet.WarehouseReceiptsDataTable dataTable);
        Task SaveChanges2TableAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable dataTable);
        Task SaveReceiptWithItemsAsync(AnbarDataSet dataset);
        Task<string> GenerateNewReceiptNumber();

        Task DeleteReceiptWithInventoryAsync(AnbarDataSet.WarehouseReceiptsRow receiptRow);
    }
}