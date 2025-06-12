using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using Domain.Attribute;
using Domain.Repositorys;
using Domain.SharedSevices;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace AnbarPersitence.Newway
{
    [Repository]
    public class EnhancedWarehouseReceiptItemsViewAdapter : EnhancedTableAdapterBase<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable>, IWarehouseReceiptItemRepository
    {
        private readonly WarehouseReceiptItemsWithProductViewTableAdapter _adapter;

        protected override DbDataAdapter DataAdapter => _adapter.GetAdapter();



        public EnhancedWarehouseReceiptItemsViewAdapter(
            ILogger<EnhancedWarehouseReceiptItemsViewAdapter> logger,
            ISessionService sessionService, ITransactionManager _manager)
            : base(logger, sessionService, _manager)
        {
            _adapter = new WarehouseReceiptItemsWithProductViewTableAdapter();
            InitCommands();
        }


        public async Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FetchByForeignKeyAsync(int receiptId)
        {
            try
            {
                return await base.FetchByForeignKeyAsync(receiptId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error loading items for ReceiptId: {ReceiptId}", receiptId);
                throw new DatabaseException(ex.Message, "cannot FetchByReceiptIdWithProductInfo Track", ErrorCode.DataBaseError, ex); 
            }
        }
        //public async Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable Table, int receiptId)
        //{
        //    try
        //    {
        //       return await  base.FillByForeignKeyAsync(Table, receiptId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError(ex, "Error loading items for ReceiptId: {ReceiptId}", receiptId);
        //        throw new DatabaseException(ex.Message, "cannot FetchByReceiptIdWithProductInfo Track", ErrorCode.DataBaseError, ex);
        //    }
        //}

        //public async Task<int> DeleteByReciteInfo(int receiptId)
        //{
        //    try
        //    {
        //       return  await DeleteByForeignKeyAsync(receiptId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError(ex, "Error loading items for ReceiptId: {ReceiptId}", receiptId);
        //         throw new DatabaseException(ex.Message, "cannot DeleteByReciteInfo Track", ErrorCode.DataBaseError, ex); ;
        //    }
        //}




        protected override void ApplyTransactionToCommands(SqlTransaction transaction)
        {
            foreach (var command in new[] {
            DataAdapter.InsertCommand,
            DataAdapter.UpdateCommand,
            DataAdapter.DeleteCommand })
            {
                if (command != null) command.Transaction = transaction;
            }
        }


    }

}
