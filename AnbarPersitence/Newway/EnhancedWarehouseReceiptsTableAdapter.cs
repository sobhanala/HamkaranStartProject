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
    public class EnhancedWarehouseReceiptsTableAdapter : EnhancedTableAdapterBase<AnbarDataSet.WarehouseReceiptsDataTable>,IWarehouseReceiptRepository
    {
        private readonly WarehouseReceiptsTableAdapter _baseAdapter;

        protected override DbDataAdapter DataAdapter => _baseAdapter.GetAdapter();
        protected override SqlConnection Connection => _baseAdapter.GetConnection();

        public EnhancedWarehouseReceiptsTableAdapter(
            ILogger<EnhancedWarehouseReceiptsTableAdapter> logger,
            ISessionService sessionService)
            : base(logger, sessionService)
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


        protected override void ApplyTransactionToCommands(SqlTransaction transaction)
        {
            if (_baseAdapter.GetAdapter().InsertCommand != null)
                _baseAdapter.GetAdapter().InsertCommand.Transaction = transaction;
            if (_baseAdapter.GetAdapter().UpdateCommand != null)
                _baseAdapter.GetAdapter().UpdateCommand.Transaction = transaction;
            if (_baseAdapter.GetAdapter().DeleteCommand != null)
                _baseAdapter.GetAdapter().DeleteCommand.Transaction = transaction;
        }


    }
}