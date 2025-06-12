using System.Threading.Tasks;
using AnbarDomain.Tabels;

namespace AnbarService.Interfaces
{
    public interface IInventoryService
    {
        Task UpdateInventoryAsync(
            AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail,
            AnbarDataSet.view_WarehouseReceiptsRow headerRow);

        Task<AnbarDataSet.InventoryDataTable> GetTheDataset();

        Task DeleteInventoryAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail,
            AnbarDataSet.view_WarehouseReceiptsRow headerRow);
    }
}