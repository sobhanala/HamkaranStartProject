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

namespace AnbarPersitence.Newway
{
    [Repository]
    public class EnhancedInventoryAdapter : EnhancedTableAdapterBase<AnbarDataSet.InventoryDataTable>, IInventoryRepository
    {
        private readonly InventoryTableAdapter _adapter;

        protected override DbDataAdapter DataAdapter => _adapter.GetAdapter();



        public EnhancedInventoryAdapter(
            ILogger<EnhancedInventoryAdapter> logger,
            ISessionService sessionService, ITransactionManager manager)
            : base(logger, sessionService, manager)
        {
            _adapter = new InventoryTableAdapter();
            InitCommands();
        }





        protected override void ApplyTransactionToCommands(SqlTransaction transaction)
        {
            foreach (var command in new[] {
            DataAdapter.InsertCommand,
            DataAdapter.UpdateCommand,
            DataAdapter.DeleteCommand,
            DataAdapter.SelectCommand })
            {
                if (command != null) command.Transaction = transaction;
            }
        }

        public async Task<AnbarDataSet.InventoryDataTable> GetAvailableStock(int productId, int warehouseId)
        {
            try
            {
                var command = new SqlCommand("SELECT * FROM Inventory WHERE ProductId = @ProductId AND WarehouseId=@WarehouseId ", Connection);

                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@WarehouseId", warehouseId);
                if (Transaction != null)
                {
                    command.Transaction = Transaction;
                }
                var a = await FetchAsyncByCommand(command);
                return a;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error loading items for ReceiptId: {productID}-{WarehouseId}", productId, warehouseId);
                throw;
            }
        }
    }

}
