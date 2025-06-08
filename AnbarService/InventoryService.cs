using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace AnbarService
{
    [Service]
    class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }


        public async Task<AnbarDataSet.InventoryDataTable> GetTheDataset()
        {
            var inventory = new AnbarDataSet.InventoryDataTable();

            await _inventoryRepository.FillAsync(inventory);

            return inventory;
        }

        public async Task UpdateInventoryAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail, AnbarDataSet.view_WarehouseReceiptsRow headerRow)
        {
            var inventoryTable = await GetTheDataset();


            foreach (var item in detail)
            {
                var existingStock = await _inventoryRepository.GetAvailableStock(item.ProductId, headerRow.WarehouseId);
                var existingRow = existingStock.FirstOrDefault();

                int deltaQty = headerRow.ReceiptStatus == 0 ? item.Quantity : -item.Quantity;

                if (existingRow != null)
                {
                    if (headerRow.ReceiptStatus == 1 && existingRow.Quantity < item.Quantity)
                    {
                        throw new InventoryException(technicalMessage:" ",userFriendlyMessage: $"Insufficient stock for product {item.ProductId}", ErrorCode.InssufientProduct);
                    }

                    await _inventoryRepository.UpdateATrack(item.ProductId, headerRow.WarehouseId,
                        deltaQty + existingRow.Quantity);
                }
                else
                {
                    if (headerRow.ReceiptStatus == 1)
                    {
                        throw new InventoryException(technicalMessage:" ",userFriendlyMessage: $"No stock found for product {item.ProductId}", ErrorCode.NoStockFound);
                    }

                    AddNewRowToInventory(headerRow, inventoryTable, item);
                }
            }

            await _inventoryRepository.UpdateAsync(inventoryTable);
        }


        public async Task DeleteInventoryAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail,
            AnbarDataSet.view_WarehouseReceiptsRow headerRow)
        {

            foreach (var item in detail)
            {
                var existingStock = await _inventoryRepository.GetAvailableStock(item.ProductId, headerRow.WarehouseId);
                var existingRow = existingStock.FirstOrDefault();

                if (existingRow == null)
                {
                    throw new InventoryException(technicalMessage:" ", userFriendlyMessage: $"No inventory found for product {item.ProductId} in warehouse {headerRow.WarehouseId}", ErrorCode.NoInventoryFound);

                }

                if (existingRow.Quantity < item.Quantity)
                {
                    throw new InventoryException(technicalMessage: " ", userFriendlyMessage: $"Cannot delete. Not enough stock for product {item.ProductId}.", ErrorCode.NoStockFound);
                }
                int deltaQty = headerRow.ReceiptStatus == 1 ? item.Quantity : -item.Quantity;


                await _inventoryRepository.UpdateATrack(item.ProductId, headerRow.WarehouseId,
                    deltaQty + existingRow.Quantity);
            }

        }







        #region Private




        private void AddNewRowToInventory(AnbarDataSet.view_WarehouseReceiptsRow headerRow, AnbarDataSet.InventoryDataTable inventoryTable,
            AnbarDataSet.WarehouseReceiptItemsWithProductViewRow item)
        {
            var row = inventoryTable.NewInventoryRow();
            row.ProductId = item.ProductId;
            row.WarehouseId = headerRow.WarehouseId;
            row.Quantity = item.Quantity;

            inventoryTable.AddInventoryRow(row);
        }


        #endregion
    }
}
