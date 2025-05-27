using System;
using System.Data;
using System.Threading.Tasks;
using AnbarDomain.Tabels;

namespace AnbarService
{
    public interface IWarehouseReceipt
    {
        Task<AnbarDataSet> GetFullDatasetAsync();
        Task<AnbarDataSet> GetReceiptWithItemsAsync(int receiptId);
        Task<DataRow> CreateWarehouseReceiptAsync(AnbarDataSet dataset, int warehouseId, int partyId, byte type,DateTime recitedDateTime);
        Task SaveReceiptItemsAndUpdateEwiAsync(AnbarDataSet dataset, int receiptId);
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(AnbarDataSet dataset, int receiptId);
        Task SaveChangesTableAsync(AnbarDataSet.WarehouseReceiptsDataTable dataTable);
        Task SaveChanges2TableAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable dataTable);
    }
}