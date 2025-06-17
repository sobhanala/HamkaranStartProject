using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.SharedSevices;
using System;
using System.Linq;
using System.Threading.Tasks;
using AnbarService.Interfaces;
using Domain.Exceptions;


namespace AnbarService
{
    [Service]
    class WarehouseReceiptService : BaseMasterDetailService<IWarehouseReceiptRepository,IWarehouseReceiptItemRepository,AnbarDataSet
        ,AnbarDataSet.view_WarehouseReceiptsDataTable,AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable>,IWarehouseReceipt
    {
        private readonly IWarehouseReceiptRepository _receiptRepository;
        private readonly IWarehouseReceiptItemRepository _receiptItemRepository;
        private readonly IInventoryService _inventoryService;



        public WarehouseReceiptService(IWarehouseReceiptRepository receiptRepository, IWarehouseReceiptItemRepository receiptItemRepository, ITransactionManager transactionManager, IInventoryService inventoryService) : 
            base(headerRepo:receiptRepository,detailRepo:receiptItemRepository,transactionManager)
        {
            _receiptRepository = receiptRepository;
            _receiptItemRepository = receiptItemRepository;
            _inventoryService = inventoryService;
        }

        public async Task<string> GenerateNewReceiptNumber()
        {
            var recitenum = await _receiptRepository.GetNextReceiptNumberAsync();
            return recitenum;
        }

        #region BaseClassAbstractsAndMetohd

        protected override async Task<AnbarDataSet.view_WarehouseReceiptsDataTable> FetchMasterAsync()
        {
            return await _receiptRepository.FetchAsync();
        }

        public override async Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FetchDetailsByMasterIdAsync(int masterId)
        {
            return await _receiptItemRepository.FetchByForeignKeyAsync(masterId);
        }


        protected override async Task BeforeSaveAsync(AnbarDataSet dataset)
        {
            ValidateDataSet(dataset);

            var masterRow = await SetIdToDetail(dataset);
            await _inventoryService.UpdateInventoryAsync(dataset.WarehouseReceiptItemsWithProductView, masterRow);
        }

        private static void ValidateDataSet(AnbarDataSet dataset)
        {
            if (dataset.WarehouseReceiptItemsWithProductView == null || dataset.WarehouseReceiptItemsWithProductView.Count == 0)
            {
                throw new WarehouseReceiptException("","Cannot save a warehouse receipt without at least one item.",ErrorCode.EmptyReciteTryToSave);
            }
        }

        private async Task<AnbarDataSet.view_WarehouseReceiptsRow> SetIdToDetail(AnbarDataSet dataset)
        {
            var masterRow = (AnbarDataSet.view_WarehouseReceiptsRow)dataset.view_WarehouseReceipts.Rows[dataset.view_WarehouseReceipts.Rows.Count - 1];

            if (masterRow.Id==-1)
            {
                var lastRowId = await _receiptRepository.GetLastInsertedReceiptIdAsync();

                foreach (var item in dataset.WarehouseReceiptItemsWithProductView)
                {
                    item.ReceiptId = lastRowId;

                }

                foreach (var header in dataset.view_WarehouseReceipts)
                {
                    header.Id = lastRowId;

                }
            }

            return masterRow;
        }


        protected override async Task AfterSaveAsync(AnbarDataSet dataset)
        {

            var masterRow = (AnbarDataSet.view_WarehouseReceiptsRow) dataset.view_WarehouseReceipts.Rows[dataset.view_WarehouseReceipts.Rows.Count - 1];

            await UpdateHeaderTotalAmount(dataset.WarehouseReceiptItemsWithProductView, masterRow, dataset.view_WarehouseReceipts);

        }

        protected override async  Task BeforeDeleteAsync(AnbarDataSet dataset)
        {
            var header = dataset.view_WarehouseReceipts.FirstOrDefault(); 
            await _inventoryService.DeleteInventoryAsync(dataset.WarehouseReceiptItemsWithProductView, header);
        }


        

        #endregion

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



            await _receiptRepository.UpdateAsync(header);
        }

        #endregion

    }



}

