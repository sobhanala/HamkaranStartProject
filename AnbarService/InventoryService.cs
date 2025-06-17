using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AnbarDomain.Orders;
using AnbarService.Interfaces;
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

        public async Task UpdateInventoryAsync(
            AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail,
            AnbarDataSet.view_WarehouseReceiptsRow headerRow)
        {
            var inventoryTable = await GetTheDataset();

            await ValidateInventoryItem(detail, headerRow);

            foreach (var item in detail)
            {
                var changeQuantity = CalculateInventoryChange(item);
                var existingStock = await _inventoryRepository.GetAvailableStock(item.ProductId, headerRow.WarehouseId);
                var existingRow = existingStock.FirstOrDefault();

                int deltaQty = headerRow.ReceiptStatus == (int)ReciteType.Purchase ? changeQuantity : -changeQuantity;

                if (existingRow != null)
                {
                    await _inventoryRepository.UpdateATrack(
                        item.ProductId,
                        headerRow.WarehouseId,
                        deltaQty + existingRow.Quantity
                    );
                }
                else
                {
                    AddNewRowToInventory(headerRow, inventoryTable, item);
                }
            }

            await _inventoryRepository.UpdateAsync(inventoryTable);
        }

        private async Task ValidateInventoryItem(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail,
            AnbarDataSet.view_WarehouseReceiptsRow headerRow)
        {
            var validationErrors = new List<string>();
            foreach (var item in detail)
            {
                var changeQuantity = CalculateInventoryChange(item);
                var existingStock = await _inventoryRepository.GetAvailableStock(item.ProductId, headerRow.WarehouseId);
                var existingRow = existingStock.FirstOrDefault();

                if (headerRow.ReceiptStatus == (int)ReciteType.Sale)
                {
                    if (existingRow == null)
                    {
                        validationErrors.Add($"No stock found for product {item.ProductId}");
                    }
                    else if (existingRow.Quantity < item.Quantity)
                    {
                        validationErrors.Add($"Insufficient stock for product {item.ProductId} (available: {existingRow.Quantity}, required: {item.Quantity})");
                    }
                }
            }

            if (validationErrors.Any())
            {
                throw new InventoryException(
                    technicalMessage: "Inventory validation failed.",
                    userFriendlyMessage: string.Join(Environment.NewLine, validationErrors),
                    errorCode: ErrorCode.InssufientProduct // Add this code if not defined
                );
            }
        }

        private static int CalculateInventoryChange(AnbarDataSet.WarehouseReceiptItemsWithProductViewRow item)
        {
            var headerquantity = 0;
            switch (item.RowState)
            {
                case DataRowState.Added:
                    headerquantity = item.Quantity;
                    break;

                case DataRowState.Modified:
                    var oldQty = (int)item["Quantity", DataRowVersion.Original];
                    var newQty = item.Quantity;
                    headerquantity = newQty - oldQty;
                    break;

                case DataRowState.Deleted:
                    var deletedQty = (int)item["Quantity", DataRowVersion.Original];

                    headerquantity =deletedQty;
                    break;
            }

            return headerquantity;
        }

        public async Task DeleteInventoryAsync(
            AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail,
            AnbarDataSet.view_WarehouseReceiptsRow headerRow)
        {
            foreach (var item in detail)
            {
                var existingStock = await _inventoryRepository.GetAvailableStock(item.ProductId, headerRow.WarehouseId);
                var existingRow = existingStock.FirstOrDefault();

                if (existingRow == null)
                {
                    throw new InventoryException(
                        technicalMessage: " ",
                        userFriendlyMessage: $"No inventory found for product {item.ProductId} in warehouse {headerRow.WarehouseId}",
                        ErrorCode.NoInventoryFound);
                }

                if (headerRow.ReceiptStatus == (int)ReciteType.Purchase && existingRow.Quantity < item.Quantity)
                {
                    throw new InventoryException(
                        technicalMessage: " ",
                        userFriendlyMessage: $"Cannot delete. Not enough stock to reverse purchase for product {item.ProductId}.",
                        ErrorCode.NoStockFound);
                }

                int deltaQty = headerRow.ReceiptStatus == (int)ReciteType.Purchase
                    ? -item.Quantity  
                    : item.Quantity; 

                await _inventoryRepository.UpdateATrack(
                    item.ProductId,
                    headerRow.WarehouseId,
                    existingRow.Quantity + deltaQty);
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
