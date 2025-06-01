using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.SharedSevices;


namespace AnbarService
{
    [Service]
    class WarehouseReceiptService: IWarehouseReceipt
    {
        private readonly IWarehouseReceiptRepository _receiptRepository;
        private readonly IWarehouseReceiptItemRepository _receiptItemRepository;
        private readonly ITransactionManager _transactionManager;




        public WarehouseReceiptService(IWarehouseReceiptRepository receiptRepository, IWarehouseReceiptItemRepository receiptItemRepository, ITransactionManager transactionManager)
        {
            _receiptRepository = receiptRepository;
            _receiptItemRepository = receiptItemRepository;
           _transactionManager = transactionManager;
        }

        public async  Task<string> GenerateNewReceiptNumber()
        {
            var recitenum=await _receiptRepository.GetNextReceiptNumberAsync();
            return recitenum;
        }

        public  async Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(int receiptId)
        {
            return await _receiptItemRepository.FetchByReceiptIdWithProductInfo(receiptId);
        }


        public async Task<AnbarDataSet> GetFullDatasetAsync()
        {
            var dataSet = new AnbarDataSet();

            await _receiptRepository.FillAsync(dataSet.WarehouseReceipts);
            await _receiptItemRepository.FillAsync(dataSet.WarehouseReceiptItemsWithProductView);

            return dataSet;
        }


        public async Task<AnbarDataSet> GetReceiptWithItemsAsync(int receiptId)
        {
            var dataSet = await GetFullDatasetAsync();
            var receipt = dataSet.WarehouseReceipts.FindById(receiptId);

            if (receipt == null) return null;



            var result = new AnbarDataSet();

            result.WarehouseReceipts.ImportRow(receipt);

            foreach (var item in receipt.GetWarehouseReceiptItemsRows())
            {
                result.WarehouseReceiptItems.ImportRow(item);
            }

            return result;
        }


        public async Task SaveChangesTableAsync(AnbarDataSet.WarehouseReceiptsDataTable  dataTable)
        {
            await _receiptRepository.UpdateAsync(dataTable);
        }
        public async Task SaveChanges2TableAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable  dataTable)
        {
            await _receiptItemRepository.UpdateAsync(dataTable);
        }

        public Task UpdateSingleItemAsync(int itemId, decimal newQty, decimal newPrice)
        {
            throw new NotImplementedException();
        }


        public async Task<DataRow> CreateWarehouseReceiptAsync(AnbarDataSet dataset, int warehouseId, int partyId, byte type, DateTime date)
        {

            var newRow = dataset.WarehouseReceipts.NewWarehouseReceiptsRow();
            newRow.WarehouseId = warehouseId;
            newRow.PartyId = partyId;
            newRow.ReceiptNumber = await GenerateNewReceiptNumber();
            newRow.ReceiptStatus = type;
            newRow.ReceiptDate = date;

            dataset.WarehouseReceipts.AddWarehouseReceiptsRow(newRow);

            await SaveChangesTableAsync(dataset.WarehouseReceipts);

            return newRow;
        }

        public async Task SaveReceiptItemsAndUpdateEwiAsync(AnbarDataSet dataset, int receiptId)
        {
            var receipt = dataset.WarehouseReceipts.FindById(receiptId);
            if (receipt == null)
                throw new InvalidOperationException($"Receipt with ID {receiptId} not found.");

            var items = receipt.GetWarehouseReceiptItemsRows();

            if (items == null || items.Length == 0)
                throw new InvalidOperationException("No items found for the specified receipt.");

            foreach (var item in items)
            {
                if (item.IsQuantityNull() || item.Quantity <= 0)
                    throw new InvalidOperationException("Each item must have a quantity greater than zero.");
            }

            await SaveChanges2TableAsync(dataset.WarehouseReceiptItemsWithProductView);
        }

        public async Task UpdateSingleItemAsync(AnbarDataSet.WarehouseReceiptItemsWithProductViewRow dViewRow)
        {

            
        }
        private void LogTableRows(DataTable table, string tableName, string context)
        {
            Debug.WriteLine($"\n---- {context}: Table = {tableName} ----");

            foreach (DataRow row in table.Rows)
            {
                var rowState = row.RowState;
                var rowValues = string.Join(", ", table.Columns
                    .Cast<DataColumn>()
                    .Select(c => $"{c.ColumnName}={row[c]}"));

                Debug.WriteLine($"[{rowState}] {rowValues}");
            }

            Debug.WriteLine($"---- End of {context} ----\n");
        }

        public async Task SaveReceiptWithItemsAsync(AnbarDataSet dataset)
        {

            _transactionManager.BeginTransactionAsync();
            try
            {
                LogTableRows(dataset.WarehouseReceipts, "WarehouseReceipts", "Before Update");

                await _receiptRepository.UpdateTransaction(dataset.WarehouseReceipts);

                LogTableRows(dataset.WarehouseReceipts, "WarehouseReceipts", "after Update");


                var header=await _receiptRepository.FetchAsync();

                var lastRow = (AnbarDataSet.WarehouseReceiptsRow)header.Rows[header.Rows.Count - 1];

                foreach (var item in dataset.WarehouseReceiptItemsWithProductView)
                {
                    item.ReceiptId = lastRow.Id;

                }
                await _receiptItemRepository.UpdateAsync(dataset.WarehouseReceiptItemsWithProductView);

                _transactionManager.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _transactionManager.RollbackTransactionAsync();
                throw new Exception("Failed to save receipt and items together", ex);
            }
        }

    }
}

