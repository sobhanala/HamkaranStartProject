using AnbarDomain.Tabels;
using System.Threading.Tasks;

namespace AnbarService
{
    public interface IWarehouseReceipt
    {
        Task<AnbarDataSet> GetFullDatasetAsync();
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(int receiptId);
        Task SaveReceiptWithItemsAsync(AnbarDataSet dataset);
        Task<string> GenerateNewReceiptNumber();
        Task DeleteReceiptWithInventoryAsync(AnbarDataSet.view_WarehouseReceiptsRow receiptRow);
        Task FillReceiptById(AnbarDataSet dataSet, int reciteId);
    }
}