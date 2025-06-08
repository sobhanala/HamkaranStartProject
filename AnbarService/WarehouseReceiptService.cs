using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.SharedSevices;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;


namespace AnbarService
{
    [Service]
    class WarehouseReceiptService : IWarehouseReceipt
    {
        private readonly IWarehouseReceiptRepository _receiptRepository;
        private readonly IWarehouseReceiptItemRepository _receiptItemRepository;
        private readonly ITransactionManager _transactionManager;
        private readonly IInventoryService _inventoryService;



        public WarehouseReceiptService(IWarehouseReceiptRepository receiptRepository, IWarehouseReceiptItemRepository receiptItemRepository, ITransactionManager transactionManager, IInventoryService inventoryService)
        {
            _receiptRepository = receiptRepository;
            _receiptItemRepository = receiptItemRepository;
            _transactionManager = transactionManager;
            _inventoryService = inventoryService;
        }

        public async Task<string> GenerateNewReceiptNumber()
        {
            var recitenum = await _receiptRepository.GetNextReceiptNumberAsync();
            return recitenum;
        }


        public async Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(int receiptId)
        {
            return await _receiptItemRepository.FetchByReceiptIdWithProductInfo(receiptId);
        }


        public async Task<AnbarDataSet> GetFullDatasetAsync()
        {
            var dataSet = new AnbarDataSet();
            await _receiptRepository.FillAsync(dataSet.view_WarehouseReceipts);
            await _receiptItemRepository.FillAsync(dataSet.WarehouseReceiptItemsWithProductView);

            return dataSet;
        }

        public async Task FillReceiptById(AnbarDataSet dataSet,int reciteId)
        {
            await _receiptRepository.GetByIdAsync(dataSet.view_WarehouseReceipts,reciteId);
            await _receiptItemRepository.FillByReceiptIdWithProductInfo(dataSet.WarehouseReceiptItemsWithProductView, reciteId);

        }


        public async Task SaveReceiptWithItemsAsync(AnbarDataSet dataset)
        {

            _transactionManager.BeginTransactionAsync();
            try
            {

                await _receiptRepository.UpdateTransaction(dataset.view_WarehouseReceipts);



                var header = await _receiptRepository.FetchAsync();

                var lastRow = (AnbarDataSet.view_WarehouseReceiptsRow)header.Rows[header.Rows.Count - 1];

                foreach (var item in dataset.WarehouseReceiptItemsWithProductView)
                {
                    item.ReceiptId = lastRow.Id;

                }
                await _receiptItemRepository.UpdateAsync(dataset.WarehouseReceiptItemsWithProductView);

                var detail = await _receiptItemRepository.FetchByReceiptIdWithProductInfo(lastRow.Id);

                await UpdateHeaderTotalAmount(detail, lastRow, header);

              
                await _inventoryService.UpdateInventoryAsync(detail, lastRow);

                
                _transactionManager.CommitTransactionAsync();


            }
            catch (Exception ex)
            {
                _transactionManager.RollbackTransactionAsync();
                throw;
            }
        }


        public async Task DeleteReceiptWithInventoryAsync(AnbarDataSet.view_WarehouseReceiptsRow receiptRow)
        {
            _transactionManager.BeginTransactionAsync();

            try
            {
                var detail = await _receiptItemRepository.FetchByReceiptIdWithProductInfo(receiptRow.Id);

                await _inventoryService.DeleteInventoryAsync(detail, receiptRow);
                await _receiptItemRepository.DeleteByReciteInfo(receiptRow.Id);

                await _receiptRepository.DeleteByIdAsync(receiptRow.Id);

                _transactionManager.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _transactionManager.RollbackTransactionAsync();
                throw;
            }
        }




        #region Private

        private async Task UpdateHeaderTotalAmount(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable detail, AnbarDataSet.view_WarehouseReceiptsRow lastRow,
            AnbarDataSet.view_WarehouseReceiptsDataTable header)
        {

            decimal totalAmount = 0;

            foreach (var item in detail)
            {
                if (!item.IsTotalAmountNull())
                    totalAmount += item.TotalAmount;
            }

            decimal discount = lastRow.IsDiscountNull() ? 0 : lastRow.Discount;
            decimal transportCost = lastRow.IsTransportCostNull() ? 0 : lastRow.TransportCost;

            decimal discountedAmount = totalAmount * (1 - discount / 100m);
            decimal finalAmount = discountedAmount + transportCost;

            lastRow.TotalAmount = finalAmount;



            await _receiptRepository.UpdateTransaction(header);
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




        #endregion

    }



}

