using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AnbarDomain.Orders;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.Repositorys;
using Domain.SharedSevices;
using Microsoft.Extensions.Logging;

namespace AnbarPersitence
{
    [Repository]
    public class WarehouseReceiptItemRepository : TypedDataSetRepository<ReceiptItems, int, AnbarDataSet>,
        IWarehouseReceiptItemRepository
    {
        private readonly ILogger<WarehouseReceiptItemRepository> _logger;
        private readonly ISessionService _sessionService;

        public WarehouseReceiptItemRepository(DbConnectionFactory connectionFactory,
            ILogger<WarehouseReceiptItemRepository> logger, ISessionService sessionService)
            : base(connectionFactory, "WarehouseReceiptItems", "Id",
                GetColumnNames(new AnbarDataSet.WarehouseReceiptItemsDataTable()), logger, sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }


        public async Task<AnbarDataSet> FillByReceiptIdWithProductInfo(int receiptId)
        {

            const string query = @"
        SELECT 
            wri.Id, 
            wri.ReceiptId, 
            wri.ProductId, 
            p.Name AS ProductName, 
            p.Code AS ProductCode, 
            wri.Quantity, 
            wri.UnitPrice
        FROM 
            WarehouseReceiptItems wri
        INNER JOIN 
            Products p ON wri.ProductId = p.Id
        WHERE 
            wri.ReceiptId = @ReceiptId";

            var parameter = new SqlParameter("@ReceiptId", SqlDbType.Int) { Value = receiptId };

            var dataSet = await ExecuteTypedDataSetAsync<AnbarDataSet>(
                commandText: query,
                type: CommandType.Text,
                tableName: "WarehouseReceiptItemsWithProductView",
                parameters: parameter
            );

             dataSet.WarehouseReceiptItemsWithProductView.MarkViewColumnsAsReadOnly();

            return dataSet;

        }



        protected override IEnumerable<ReceiptItems> MapResultsToEntities(AnbarDataSet dataSet)
        {
            return dataSet.WarehouseReceipts.Select(MapToDomainEntity).ToList();
        }

        protected override ReceiptItems MapSingleResultToEntity(AnbarDataSet dataSet)
        {
            return dataSet.Products.Count > 0 ? MapToDomainEntity(dataSet.WarehouseReceipts[0]) : null;
        }

        protected override IEnumerable<SqlParameter> CreateParametersFromEntity(ReceiptItems entity)
        {
            return null;
        }

        private ReceiptItems MapToDomainEntity(AnbarDataSet.WarehouseReceiptsRow row)
        {
            return new ReceiptItems
            {
                Id = row.Id,

            };
        }


    }
}

