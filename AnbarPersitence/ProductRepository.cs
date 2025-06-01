using AnbarDomain.Products;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.Repositorys;
using Domain.SharedSevices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;



namespace AnbarPersitence
{
    [Repository]
    public class ProductRepository : TypedDataSetRepository<Product, int, AnbarDataSet>, IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;

        private readonly ISessionService _sessionService;


        public ProductRepository(DbConnectionFactory connectionFactory, ILogger<ProductRepository> logger, ISessionService sessionService)
            : base(connectionFactory, "Products", "Id", GetColumnNames(new AnbarDataSet.ProductsDataTable()), logger, sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }

        public async Task<int> GetMaxProductCode()
        {
            string query = "SELECT MAX(CAST(ProductCode AS INT)) FROM Products WHERE ISNUMERIC(ProductCode) = 1";

            var result = await ExecuteScalarAsync<int>(query, CommandType.Text);
            return result;
        }


        protected override IEnumerable<Product> MapResultsToEntities(AnbarDataSet dataSet)
        {
            return dataSet.Products.Select(MapToDomainEntity).ToList();
        }

        protected override Product MapSingleResultToEntity(AnbarDataSet dataSet)
        {
            return dataSet.Products.Count > 0 ? MapToDomainEntity(dataSet.Products[0]) : null;
        }

        protected override IEnumerable<SqlParameter> CreateParametersFromEntity(Product entity)
        {
            return new[]
            {
                CreateParameter("@Name", entity.Name, DbType.String),
                CreateParameter("@Description", entity.Description, DbType.String),
                CreateParameter("@CostPrice", entity.CostPrice, DbType.Decimal),
                CreateParameter("@SellingPrice", entity.SellingPrice, DbType.Decimal),
                CreateParameter("@ProductCode", entity.ProductCode, DbType.String),
                CreateParameter("@UnitOfMeasure", (int)entity.UnitOfMeasure, DbType.Int32),
                CreateParameter("@Weight", entity.Weight, DbType.Decimal),
                CreateParameter("@CreatedAt", entity.CreatedAt == default ? DateTime.Now : entity.CreatedAt, DbType.DateTime)
            };
        }



        private Product MapToDomainEntity(AnbarDataSet.ProductsRow row)
        {
            return new Product
            {
                Id = row.Id,
                Name = row.Name,
                Description = row.Description,
                UnitOfMeasure = (Unit)row.UnitOfMeasure,
                SellingPrice = row.SellingPrice,
                CostPrice = row.CostPrice,
                ProductCode = row.ProductCode,
                CreatedAt = row.IsCreatedAtNull() ? DateTime.Now : row.CreatedAt
            };
        }



    }
}