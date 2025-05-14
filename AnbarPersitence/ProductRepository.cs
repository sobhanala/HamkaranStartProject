using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AnbarDomain.Partys;
using AnbarDomain.Products;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.Common;
using Domain.Exceptions;
using Domain.Repositorys;
using Microsoft.Extensions.Logging;



namespace AnbarPersitence
{
    [Repository]
    public class ProductRepository : TypedDataSetRepository<Product, int, AnbarDataSet>, IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        


        public ProductRepository(DbConnectionFactory connectionFactory, ILogger<ProductRepository> logger)
            : base(connectionFactory, "Products", "Id", GetColumnNames(new AnbarDataSet.ProductsDataTable()),logger)
        {
            _logger = logger;
        }

        //public async Task<int> SaveChangesFromDataTable(DataTable productTable)
        //{
        //    try
        //    {
        //        var commands = new Dictionary<string, SqlCommand>();

        //        //TODO 
        //        var insertCommand = new SqlCommand(
        //            GenerateInsertQuery(productTable.TableName, GetColumnNames(productTable).Where(c => c != KeyColumn)));

        //        insertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 0, "Name");
        //        insertCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 0, "Description");
        //        insertCommand.Parameters.Add("@UnitOfMeasure", SqlDbType.Int, 0, "UnitOfMeasure");
        //        insertCommand.Parameters.Add("@CostPrice", SqlDbType.Decimal, 0, "CostPrice");
        //        insertCommand.Parameters.Add("@SellingPrice", SqlDbType.Decimal, 0, "SellingPrice");
        //        insertCommand.Parameters.Add("@Weight", SqlDbType.NVarChar, 0, "Weight");
        //        insertCommand.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
        //        insertCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt").IsNullable = true;


        //        var updateCommand = new SqlCommand(
        //            GenerateUpdateQuery(productTable.TableName, GetColumnNames(productTable), KeyColumn));
        //        updateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 0, "Name");
        //        updateCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 0, "Description");
        //        updateCommand.Parameters.Add("@UnitOfMeasure", SqlDbType.Int, 0, "UnitOfMeasure");
        //        updateCommand.Parameters.Add("@CostPrice", SqlDbType.Decimal, 0, "CostPrice");
        //        updateCommand.Parameters.Add("@SellingPrice", SqlDbType.Decimal, 0, "SellingPrice");
        //        updateCommand.Parameters.Add("@Weight", SqlDbType.NVarChar, 0, "Weight");
        //        updateCommand.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
        //        updateCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt").IsNullable = true;
        //        updateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");


        //        var deleteCommand = new SqlCommand(GenerateDeleteQuery(productTable.TableName, KeyColumn));
        //        deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");


        //        commands.Add("Insert", insertCommand);
        //        commands.Add("Update", updateCommand);
        //        commands.Add("Delete", deleteCommand);


        //        return await ExecuteDataAdapterUpdateAsyncOnDataTabel(productTable, commands);
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError(ex, "Failed to SaveChangesFromDataTable Table {UserId}", productTable.TableName);
        //        throw new DatabaseException("Failed to SaveChangesFromDataTable Table",
        //            $"Error SaveChanges user",
        //            ErrorCode.DataBaseError, ex);
        //    }

        //}
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