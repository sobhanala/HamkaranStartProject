﻿using System;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using AnbarDomain.Tabels.AnbarDataSetTableAdapters;
using Domain.Attribute;
using Domain.Repositorys;
using Domain.SharedSevices;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace AnbarPersitence.Newway
{
    [Repository]
    public class EnhancedWarehouseReceiptsviewTableAdapter : EnhancedTableAdapterBase<AnbarDataSet.view_WarehouseReceiptsDataTable>,IWarehouseReceiptRepository
    {
        private readonly view_WarehouseReceiptsTableAdapter _baseAdapter;

        protected override DbDataAdapter DataAdapter => _baseAdapter.GetAdapter();

        public EnhancedWarehouseReceiptsviewTableAdapter(
            ILogger<EnhancedWarehouseReceiptsviewTableAdapter> logger,
            ISessionService sessionService, ITransactionManager _manager)
            : base(logger, sessionService, _manager)
        {
            _baseAdapter = new view_WarehouseReceiptsTableAdapter();
            InitCommands();
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
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error generating next receipt number");
                throw new DatabaseException(ex.Message, "Error generating next receipt number", ErrorCode.DataBaseError, ex);
            }
        }

        public async Task<int> GetLastInsertedReceiptIdAsync()
        {
            var query = "SELECT ISNULL(MAX(Id), 0) + 1 FROM WarehouseReceipts";

            try
            {
                using (var command = new SqlCommand(query, Connection))
                {
                    if (Connection.State != ConnectionState.Open)
                        await Connection.OpenAsync();
                    command.Transaction = Transaction;

                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error retrieving last inserted receipt ID");
                throw new DatabaseException(ex.Message, "Error retrieving last inserted receipt ID", ErrorCode.DataBaseError, ex);
            }
        }

        public async Task<AnbarDataSet.view_WarehouseReceiptsDataTable> FetchAsync()
        {
            try
            {
                var datatable = await base.FetchTypedAsync();
                return datatable;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error FetchAsync ");
                throw new DatabaseException(e.Message, "cannot FetchAsync Track", ErrorCode.DataBaseError, e); ;
            }
        }

        //public async Task<int> UpdateTransaction(AnbarDataSet.view_WarehouseReceiptsDataTable data)
        //{
        //    try
        //    {
        //        var insertCommand = CreateInsertCommand();
        //        _baseAdapter.GetAdapter().InsertCommand = insertCommand;
        //        var updated = await base.UpdateAsync(data);

        //        return updated;
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.LogError(e, "Error UpdateTransaction");
        //        throw new DatabaseException(e.Message, "cannot UpdateTransaction Track", ErrorCode.DataBaseError, e); ;
        //    }

        //}


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