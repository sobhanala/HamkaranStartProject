using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
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
    public class EnhancedWarehouseReceiptsTableAdapter : EnhancedTableAdapterBase<AnbarDataSet.WarehouseReceiptsDataTable>,IWarehouseReceiptRepository
    {
        private readonly WarehouseReceiptsTableAdapter _baseAdapter;

        protected override DbDataAdapter DataAdapter => _baseAdapter.GetAdapter();

        public EnhancedWarehouseReceiptsTableAdapter(
            ILogger<EnhancedWarehouseReceiptsTableAdapter> logger,
            ISessionService sessionService,ITransactionManager _manager)
            : base(logger, sessionService,_manager)
        {
            _baseAdapter = new WarehouseReceiptsTableAdapter();
            InitCommands();
        }

        public async Task<AnbarDataSet.WarehouseReceiptsDataTable> GetDataByIdAsync(int receiptId)
        {
            try
            {
                var result = await base.GetByIdAsync(receiptId);
                return result as AnbarDataSet.WarehouseReceiptsDataTable;
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex, "Error getting WarehouseReceipt by ID: {ReceiptId}", receiptId);
                throw;
            }
        }

        public async Task<string> GetNextReceiptNumberAsync()
        {
            var query = @"
            SELECT ISNULL(MAX(CAST(SUBSTRING(ReceiptNumber, 5, LEN(ReceiptNumber)) AS INT)), 0)
            FROM WarehouseReceipts
            WHERE ReceiptNumber LIKE 'RCP-%'
            AND ISNUMERIC(SUBSTRING(ReceiptNumber, 5, LEN(ReceiptNumber))) = 1";
            try
            {
                using (var command = new SqlCommand(query, Connection))
                {

                    if (Connection.State != ConnectionState.Open)
                        await Connection.OpenAsync();

                    var maxNumber = (int)await command.ExecuteScalarAsync();
                    return $"RCP-{(maxNumber + 1):D4}";
                }

            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex, "Error generating next receipt number");
                throw;
            }
        }

        public async Task<AnbarDataSet.WarehouseReceiptsDataTable> FetchAsync()
        {
           var datatable= await base.FetchTypedAsync();
           return datatable;
        }
        public async Task<int> UpdateTransaction2(AnbarDataSet.WarehouseReceiptsDataTable data)
        {
            try
            {
                var insertCommand = CreateInsertCommand();
                Debug.WriteLine($"THE INSERT COMMAND IS {insertCommand.CommandText}");
                insertCommand.Transaction = Transaction;
                foreach (var parameter in insertCommand.Parameters)
                {
                    Debug.WriteLine(parameter);
                }

                return await insertCommand.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public async Task<int> UpdateTransaction(AnbarDataSet.WarehouseReceiptsDataTable data)
        {
            var insertCommand = CreateInsertCommand();
            _baseAdapter.GetAdapter().InsertCommand = insertCommand;
           var updated =  await base.UpdateAsync(data);

           return updated;
        }


        protected override void ApplyTransactionToCommands(SqlTransaction transaction)
        {
            if (_baseAdapter.GetAdapter().InsertCommand != null)
                _baseAdapter.GetAdapter().InsertCommand.Transaction = transaction;
            if (_baseAdapter.GetAdapter().UpdateCommand != null)
                _baseAdapter.GetAdapter().UpdateCommand.Transaction = transaction;
            if (_baseAdapter.GetAdapter().DeleteCommand != null)
                _baseAdapter.GetAdapter().DeleteCommand.Transaction = transaction;
            if (_baseAdapter.GetAdapter().SelectCommand != null)
                _baseAdapter.GetAdapter().SelectCommand.Transaction = transaction;

        }


    }
}