﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.Repositorys;

namespace AnbarService
{
    [Service]
    class WarehouseReceiptService:IWarehouseReceipt
    {
        private readonly IWarehouseReceiptRepository _receiptRepository;

        public WarehouseReceiptService(IWarehouseReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        private async  Task<string> GenerateNewReceiptNumber()
        {
            var recitenum=await _receiptRepository.GetNextReceiptNumberAsync();
            return recitenum;
        }

        public async Task<AnbarDataSet> GetFullDatasetAsync()
        {
            var dataSet = await _receiptRepository.GetDataSetAsync();

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

        public async Task SaveChangesAsync(AnbarDataSet  dataSet)
        {
            await _receiptRepository.SaveChangesFromDataSet(dataSet);
        }

        public async Task<DataRow> CreateWarehouseReceiptAsync(AnbarDataSet dataset, int warehouseId, int partyId, byte type,DateTime date)
        {

            var newRow = dataset.WarehouseReceipts.NewWarehouseReceiptsRow();
            newRow.WarehouseId = warehouseId;
            newRow.PartyId = partyId;
            newRow.ReceiptNumber = await GenerateNewReceiptNumber();
            newRow.ReceiptStatus = type;
            newRow.ReceiptDate = date;

            dataset.WarehouseReceipts.AddWarehouseReceiptsRow(newRow);

            await SaveChangesAsync(dataset);

            return newRow;
        }

        public async Task SaveReceiptItemsAndUpdateEwiAsync(AnbarDataSet dataset, int receiptId)
        {
            var receipt = dataset.WarehouseReceipts.FindById(receiptId);
            if (receipt == null)
                throw new Exception("Receipt not found.");

            foreach (var item in receipt.GetWarehouseReceiptItemsRows())
            {
                if (item.Quantity <= 0)
                    throw new Exception("Quantity must be greater than zero.");
            }

            await SaveChangesAsync(dataset);
        }

    }
}
