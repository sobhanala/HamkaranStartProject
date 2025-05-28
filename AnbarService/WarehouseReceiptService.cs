using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.Repositorys;


/// <summary>
///  برای دفعه بعد ما میخوایم اینجوری کنیم ک ه چی صفحه اول بیاد ایتم های سند اولیه یعنی مستر رو نشون بده به چه فرمی همشونو نشون بده با دکمه ی ادد و ادیت وارد این صفحه که جدید زدی بشی 
///
/// اینمه که خود سند انبار کلی باید از ویو پر شه عین ایتم هاش و همچنین یه کار دیکه که بزنس لاجیک اصافه شدن هر کاله اونو به یه اینونتوری ببره یعنی چی یعنی ما هینونتوری دارینم یا همون انبار اوکیه و اینکه اضافه شدن هر کالا اونو تو انبار داشته باشیم یعنی موقع ااضفه شدن انبار مت لیست اینوتوری داریم
///  یعنی هر انبار یه رابطه با پروداکت داره
///
/// </summary>
namespace AnbarService
{
    [Service]
    class WarehouseReceiptService: IWarehouseReceipt
    {
        private readonly IWarehouseReceiptRepository _receiptRepository;
        private readonly IWarehouseReceiptItemRepository _receiptItemRepository;
        

        public WarehouseReceiptService(IWarehouseReceiptRepository receiptRepository, IWarehouseReceiptItemRepository receiptItemRepository)
        {
            _receiptRepository = receiptRepository;
            _receiptItemRepository = receiptItemRepository;
        }

        private async  Task<string> GenerateNewReceiptNumber()
        {
            var recitenum=await _receiptRepository.GetNextReceiptNumberAsync();
            return recitenum;
        }

        public  async Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(
            AnbarDataSet dataset, int receiptId)
        {
            return await _receiptItemRepository.FillByReceiptIdWithProductInfo(dataset.WarehouseReceiptItemsWithProductView,receiptId);
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

    }
}
