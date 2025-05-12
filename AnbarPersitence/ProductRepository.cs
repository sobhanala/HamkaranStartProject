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
            : base(connectionFactory, "Products", "Id", GetColumnNames(new AnbarDataSet.ProductsDataTable()))
        {
            _logger = logger;
        }

        public override async Task<AnbarDataSet> GetDataSetAsync()
        {
            try
            {
                return await base.GetDataSetAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to send Product");
                throw new DatabaseException("Failed to send Product",
                    "Error retrieving all users from database",
                    ErrorCode.DataBaseError, e);
            }
        }



        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await base.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all Product");
                throw new DatabaseException("Failed to get Product",
                    "Error retrieving all users from database",
                    ErrorCode.DataBaseError, ex);
            }
        }


        public override async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                return await base.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get Product by ID {UserId}", id);
                throw new DatabaseException("Failed to get Product",
                    $"Error retrieving user with ID {id}",
                    ErrorCode.DataBaseError, ex);
            }
        }



        public override async Task<int> InsertAsync(Product party)
        {
            try
            {
                return await base.InsertAsync(party);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert user {Username}", party.Name);
                throw new DatabaseException("Failed to insert user",
                    $"Error inserting user {party.Name}",
                    ErrorCode.DataBaseError, ex);
            }
        }


        public override async Task<int> UpdateAsync(Product party, string key = "")
        {
            try
            {
                return await base.UpdateAsync(party, key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update user {UserId}", party.Id);
                throw new DatabaseException("Failed to update user",
                    $"Error updating user with ID {party.Id}",
                    ErrorCode.DataBaseError, ex);
            }
        }

        public async Task<int> SaveChangesFromDataTable(DataTable productTable)
        {
            try
            {
                var commands = new Dictionary<string, SqlCommand>();

                var insertCommand = new SqlCommand(
                    GenerateInsertQuery(productTable.TableName, GetColumnNames(productTable).Where(c => c != KeyColumn)));


                insertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 0, "Name");
                insertCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 0, "Description");
                insertCommand.Parameters.Add("@UnitOfMeasure", SqlDbType.Int, 0, "UnitOfMeasure");
                insertCommand.Parameters.Add("@CostPrice", SqlDbType.Decimal, 0, "CostPrice");
                insertCommand.Parameters.Add("@SellingPrice", SqlDbType.Decimal, 0, "SellingPrice");
                insertCommand.Parameters.Add("@Weight", SqlDbType.NVarChar, 0, "Weight");
                insertCommand.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
                insertCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt").IsNullable = true;


                var updateCommand = new SqlCommand(
                    GenerateUpdateQuery(productTable.TableName, GetColumnNames(productTable), KeyColumn));
                updateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 0, "Name");
                updateCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 0, "Description");
                updateCommand.Parameters.Add("@UnitOfMeasure", SqlDbType.Int, 0, "UnitOfMeasure");
                updateCommand.Parameters.Add("@CostPrice", SqlDbType.Decimal, 0, "CostPrice");
                updateCommand.Parameters.Add("@SellingPrice", SqlDbType.Decimal, 0, "SellingPrice");
                updateCommand.Parameters.Add("@Weight", SqlDbType.NVarChar, 0, "Weight");
                updateCommand.Parameters.Add("@ProductCode", SqlDbType.VarChar, 50, "ProductCode");
                updateCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt").IsNullable = true;
                updateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");


                var deleteCommand = new SqlCommand(GenerateDeleteQuery(productTable.TableName, KeyColumn));
                deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");


                commands.Add("Insert", insertCommand);
                commands.Add("Update", updateCommand);
                commands.Add("Delete", deleteCommand);


                return await ExecuteDataAdapterUpdateAsyncOnDataTabel(productTable, commands);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Failed to SaveChangesFromDataTable Table {UserId}", productTable.TableName);
                throw new DatabaseException("Failed to SaveChangesFromDataTable Table",
                    $"Error SaveChanges user",
                    ErrorCode.DataBaseError, ex);
            }

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
                CreateParameter("@CreatedAt", DateTime.Now, DbType.DateTime),
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