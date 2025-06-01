using AnbarDomain.Tabels;
using System.Threading.Tasks;

namespace AnbarService
{
    public interface IInventoryService
    {
        Task UpdateInventoryAsync(
            AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail,
            AnbarDataSet.WarehouseReceiptsRow headerRow);

        Task<AnbarDataSet.InventoryDataTable> GetTheDataset();

        Task DeleteInventoryAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail,
            AnbarDataSet.WarehouseReceiptsRow headerRow);
    }
}