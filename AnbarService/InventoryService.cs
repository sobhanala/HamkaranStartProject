using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AnbarService
{
    [Service]
    class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        //private readonly IWarehouseReceipt _warehouseReceipt; //TODO think and thoght aabout it  i use this and put it in to the domain layer or 

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

        public async Task UpdateInventoryAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail, AnbarDataSet.WarehouseReceiptsRow headerRow)
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
                        throw new InvalidOperationException($"Insufficient stock for product {item.ProductId}");
                    }

                    var row = inventoryTable.NewInventoryRow();
                    row.Id = existingRow.Id;
                    row.ProductId = item.ProductId;
                    row.WarehouseId = headerRow.WarehouseId;
                    row.Quantity = existingRow.Quantity + deltaQty;

                    inventoryTable.ImportRow(row);
                }
                else
                {
                    if (headerRow.ReceiptStatus == 1)
                    {
                        throw new InvalidOperationException($"No stock found for product {item.ProductId}");
                    }

                    var row = inventoryTable.NewInventoryRow();
                    row.ProductId = item.ProductId;
                    row.WarehouseId = headerRow.WarehouseId;
                    row.Quantity = item.Quantity;

                    inventoryTable.AddInventoryRow(row);
                }
            }

            await _inventoryRepository.UpdateAsync(inventoryTable);
        }


        public async Task DeleteInventoryAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail,
            AnbarDataSet.WarehouseReceiptsRow headerRow)
        {
            var inventoryTable = await GetTheDataset();


            foreach (var item in detail)
            {
                var existingStock = await _inventoryRepository.GetAvailableStock(item.ProductId, headerRow.WarehouseId);
                var existingRow = existingStock.FirstOrDefault();

                if (existingRow == null)
                {
                    throw new InvalidOperationException($"No inventory found for product {item.ProductId} in warehouse {headerRow.WarehouseId}");
                }

                if (existingRow.Quantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Cannot delete. Not enough stock for product {item.ProductId}.");
                }
                int deltaQty = headerRow.ReceiptStatus == 1 ? item.Quantity : -item.Quantity;


                var row = inventoryTable.NewInventoryRow();
                row.Id = existingRow.Id;
                row.ProductId = item.ProductId;
                row.WarehouseId = headerRow.WarehouseId;
                row.Quantity = existingRow.Quantity - deltaQty;

                inventoryTable.ImportRow(row);
            }

            await _inventoryRepository.UpdateAsync(inventoryTable);
        }

    }
}
