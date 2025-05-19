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
    public class WarehouseReceiptRepository : TypedDataSetRepository<Receipt, int, AnbarDataSet>,
        IWarehouseReceiptRepository
    {
        private readonly ILogger<WarehouseReceiptRepository> _logger;
        private readonly ISessionService _sessionService;




        public WarehouseReceiptRepository(DbConnectionFactory connectionFactory,
            ILogger<WarehouseReceiptRepository> logger, ISessionService sessionService)
            : base(connectionFactory, "WarehouseReceipts", "Id",
                GetColumnNames(new AnbarDataSet.WarehouseReceiptsDataTable()), logger, sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }


        public async Task<string> GetNextReceiptNumberAsync()
        {
            string query = @"
        SELECT MAX(CAST(SUBSTRING(ReceiptNumber, 5, LEN(ReceiptNumber)) AS INT))
        FROM WarehouseReceipts
        WHERE ReceiptNumber LIKE 'RCP-%'
          AND ISNUMERIC(SUBSTRING(ReceiptNumber, 5, LEN(ReceiptNumber))) = 1;
    ";

            var maxNumber = await ExecuteScalarAsync<int>(query, CommandType.Text);
            int nextNumber = (maxNumber ) + 1;

            return $"RCP-{nextNumber:D4}";
        }




        protected override IEnumerable<Receipt> MapResultsToEntities(AnbarDataSet dataSet)
        {
            return dataSet.WarehouseReceipts.Select(MapToDomainEntity).ToList();
        }

        protected override Receipt MapSingleResultToEntity(AnbarDataSet dataSet)
        {
            return dataSet.Products.Count > 0 ? MapToDomainEntity(dataSet.WarehouseReceipts[0]) : null;
        }

        protected override IEnumerable<SqlParameter> CreateParametersFromEntity(Receipt entity)
        {
            return new[]
            {
                CreateParameter("@ReceiptNumber", entity.ReceiptNumber, DbType.String),
                CreateParameter("@ReceiptType", (int)entity.ReceiptType, DbType.Int32),
                CreateParameter("@PartyId", entity.PartyId, DbType.Int32),
                CreateParameter("@ReceiptTime", entity.ReceiptTime, DbType.DateTime),
                CreateParameter("@WareHouseId", entity.WareHouseId, DbType.Int32),
                CreateParameter("@TransportCost", entity.TransportCost, DbType.Decimal),
                CreateParameter("@TotalAmount", entity.TotalAmount, DbType.Decimal),
                CreateParameter("@CreatedAt", entity.CreatedAt == default ? DateTime.Now : entity.CreatedAt,
                    DbType.DateTime),
                CreateParameter("@CreatedBy", entity.CreatedBy, DbType.Int32),
                CreateParameter("@LastUpdated", entity.LastUpdated, DbType.DateTime),
                CreateParameter("@UpdatedBy", entity.UpdatedBy, DbType.Int32)
            };
        }

        private Receipt MapToDomainEntity(AnbarDataSet.WarehouseReceiptsRow row)
        {
            return new Receipt
            {
                Id = row.Id,
                ReceiptNumber = row.ReceiptNumber,
                PartyId = row.PartyId,
                WareHouseId = row.WarehouseId,
                ReceiptType = (ReciteType)row.ReceiptStatus,
                TransportCost = row.TransportCost,
                Discount = row.Discount,
                ReceiptTime = row.ReceiptDate,
                TotalAmount = row.TotalAmount,

                CreatedAt = row.IsCreatedAtNull() ? DateTime.Now : row.CreatedAt,
                CreatedBy = row.IsCreatedByNull() ? 0 : row.CreatedBy,
                LastUpdated = row.IsUpdatedAtNull() ? (DateTime?)null : row.UpdatedAt,
                UpdatedBy = row.IsUpdatedByNull() ? (int?)null : row.UpdatedBy
            };
        }


    }
}

