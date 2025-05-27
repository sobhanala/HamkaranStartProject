using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using Domain.Attribute;
using Domain.Repositorys;
using Domain.SharedSevices;
using Microsoft.Extensions.Logging;

namespace AnbarPersitence.Newway
{
    [Repository]
    public class EnhancedWarehouseReceiptItemsViewAdapter : EnhancedTableAdapterBase<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable>,IWarehouseReceiptItemRepository
    {
        private readonly WarehouseReceiptItemsWithProductViewTableAdapter _adapter;

        protected override DbDataAdapter DataAdapter => _adapter.GetAdapter();
        protected override SqlConnection Connection => _adapter.GetConnection();



        public EnhancedWarehouseReceiptItemsViewAdapter(
            ILogger<EnhancedWarehouseReceiptItemsViewAdapter> logger,
            ISessionService sessionService)
            : base(logger, sessionService)
        {
            _adapter = new WarehouseReceiptItemsWithProductViewTableAdapter();
            InitCommands();
        }


        public async Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable dataTable ,int receiptId)
        {
            try
            {


                var command = new SqlCommand("SELECT * FROM WarehouseReceiptItemsWithProductView WHERE ReceiptId = @ReceiptId", Connection);

                command.Parameters.AddWithValue("@ReceiptId", receiptId);


                 var a = await FillAsyncByCommand(dataTable, command);
                return a;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error loading items for ReceiptId: {ReceiptId}", receiptId);
                throw;
            }
        }

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
