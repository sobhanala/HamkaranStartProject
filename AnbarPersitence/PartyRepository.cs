using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AnbarDomain.Partys;
using AnbarDomain.repositorys;
using Domain.Attribute;
using Domain.Common;
using Domain.Exceptions;
using Domain.Repositorys;
using Microsoft.Extensions.Logging;


//TODO check the persistance 

namespace AnbarPersitence
{
    [Repository]
    public class PartyRepository : TypedDataSetRepository<Party, int, AnbarDataSet>, IPartyRepository
    {
        private readonly ILogger<PartyRepository> _logger;




        public PartyRepository(DbConnectionFactory connectionFactory, ILogger<PartyRepository> logger)
            : base(connectionFactory, "Parties", "Id", GetColumnNames(new AnbarDataSet.PartiesDataTable()))
        {
            _logger = logger;
        }

        //public async Task SyncPermissionsViaDataTable(List<Party> selectedModuleIds, int userId)
        //{
        //    var existing = (await GetAllAsync()).Where(p => p.UserId == userId).ToList();
        //    var existingModules = existing.ToHashSet();

        //    var ds = new AnbarDataSet();
        //    var permissionTable = ds.Parties;

        //    foreach (var permission in selectedModuleIds)
        //    {
        //        if (!existingModules.Contains(permission))
        //        {
        //            var row = MapPermissionToRow(permission, permissionTable);
        //            permissionTable.AddPermissionsRow(row);

        //        }
        //    }

        //    foreach (var permission in existingModules)
        //    {
        //        if (!selectedModuleIds.Contains(permission))
        //        {
        //            var deleteRow = MapPermissionToRow(permission, permissionTable);
        //            permissionTable.AddPermissionsRow(deleteRow);
        //            deleteRow.AcceptChanges();
        //            deleteRow.Delete();
        //        }
        //    }

        //    var commands = new Dictionary<string, SqlCommand>();

        //    var insert =
        //        new SqlCommand(
        //            "INSERT INTO Permissions (UserId, ModuleId, Authority, CreatedAt) VALUES (@UserId, @ModuleId, @Authority, @CreatedAt)");
        //    insert.Parameters.Add("@UserId", SqlDbType.Int, 0, "UserId");
        //    insert.Parameters.Add("@ModuleId", SqlDbType.Int, 0, "ModuleId");
        //    insert.Parameters.Add("@Authority", SqlDbType.Int, 0, "Authority");
        //    insert.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt");

        //    var delete = new SqlCommand("DELETE FROM Permissions WHERE UserId = @UserId AND ModuleId = @ModuleId");
        //    delete.Parameters.Add("@UserId", SqlDbType.Int, 0, "UserId");
        //    delete.Parameters.Add("@ModuleId", SqlDbType.Int, 0, "ModuleId");

        //    commands.Add("Insert", insert);
        //    commands.Add("Delete", delete);

        //    await ExecuteDataAdapterUpdateAsync(ds, "Permissions", commands);
        //}


        public override async Task<IEnumerable<Party>> GetAllAsync()
        {
            try
            {
                return await base.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all Party");
                throw new DatabaseException("Failed to get Party",
                    "Error retrieving all users from database",
                    ErrorCode.DataBaseError, ex);
            }
        }


        public override async Task<Party> GetByIdAsync(int id)
        {
            try
            {
                return await base.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get Party by ID {UserId}", id);
                throw new DatabaseException("Failed to get Party",
                    $"Error retrieving user with ID {id}",
                    ErrorCode.DataBaseError, ex);
            }
        }



        public override async Task<int> InsertAsync(Party party)
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


        public override async Task<int> UpdateAsync(Party party,string key = "")
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

        public async Task<int> SaveChangesFromDataTable(DataTable partiesTable)
        {
            var commands = new Dictionary<string, SqlCommand>();


            var insertCommand = new SqlCommand(
                GenerateInsertQuery(partiesTable.TableName, GetColumnNames(partiesTable).Where(c => c!=KeyColumn)));
            insertCommand.Parameters.Add("@Name", SqlDbType.NVarChar,0, "Name");
            insertCommand.Parameters.Add("@Email", SqlDbType.NVarChar,0, "Email");
            insertCommand.Parameters.Add("@Street", SqlDbType.NVarChar,0, "Street");
            insertCommand.Parameters.Add("@City", SqlDbType.NVarChar, 50, "City");
            insertCommand.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 0, "PostalCode");
            insertCommand.Parameters.Add("@Country", SqlDbType.NVarChar, 0, "Country");
            insertCommand.Parameters.Add("@PartyType", SqlDbType.Int, 0, "PartyType");
            insertCommand.Parameters.Add("@IsActive", SqlDbType.Bit, 0, "IsActive");
            insertCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt").IsNullable = true;


            var updateCommand = new SqlCommand(
                GenerateUpdateQuery(partiesTable.TableName, GetColumnNames(partiesTable),KeyColumn));
            updateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 0, "Name");
            updateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 0, "Email");
            updateCommand.Parameters.Add("@Street", SqlDbType.NVarChar, 0, "Street");
            updateCommand.Parameters.Add("@City", SqlDbType.NVarChar, 50, "City");
            updateCommand.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 0, "PostalCode");
            updateCommand.Parameters.Add("@Country", SqlDbType.NVarChar, 0, "Country");
            updateCommand.Parameters.Add("@PartyType", SqlDbType.Int, 0, "PartyType");
            updateCommand.Parameters.Add("@IsActive", SqlDbType.Bit, 0, "IsActive");
            updateCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt").IsNullable = true;
            updateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");


            var deleteCommand = new SqlCommand(GenerateDeleteQuery(partiesTable.TableName,KeyColumn));
            deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");


            commands.Add("Insert", insertCommand);
            commands.Add("Update", updateCommand);
            commands.Add("Delete", deleteCommand);


            return await ExecuteDataAdapterUpdateAsyncOnDataTabel(partiesTable,commands);
        }


        protected override IEnumerable<Party> MapResultsToEntities(AnbarDataSet dataSet)
        {
            return dataSet.Parties.Select(MapToDomainEntity).ToList();
        }

        protected override Party MapSingleResultToEntity(AnbarDataSet dataSet)
        {
            return dataSet.Parties.Count > 0 ? MapToDomainEntity(dataSet.Parties[0]) : null;
        }

        protected override IEnumerable<SqlParameter> CreateParametersFromEntity(Party party)
        {
            return new[]
            {
                CreateParameter("@Name", party.Name, DbType.String),
                CreateParameter("@Street", party.Address?.Street, DbType.String),
                CreateParameter("@City", party.Address?.City, DbType.String),
                CreateParameter("@PostalCode", party.Address?.PostalCode, DbType.String),
                CreateParameter("@Country", party.Address?.Country, DbType.String),
                CreateParameter("@Email", party.Email, DbType.String),
                CreateParameter("@PartyType", (int)party.PartyType, DbType.Int32),
                CreateParameter("@IsActive", party.IsActive, DbType.Boolean),
                CreateParameter("@CreatedAt", DateTime.Now, DbType.DateTime),
            };
        }


        private Party MapToDomainEntity(AnbarDataSet.PartiesRow row)
        {
            return new Party
            {
                Id = row.Id,
                Name = row. Name,
                Address = new Address(
                    row.IsStreetNull() ? null : row.Street,
                    row.IsCityNull() ? null : row.City,
                    row.IsPostalCodeNull() ? null : row.PostalCode,
                    row.IsCountryNull() ? null : row.Country),
                Email = row.Email,
                PartyType = (PartyType)row.PartyType,
                IsActive = row.IsActive,
                CreatedAt = row.IsCreatedAtNull() ? DateTime.Now : row.CreatedAt
            };
        }

        private void MapToDataRow(Party party, AnbarDataSet.PartiesRow row)
        {
            row.Name = party.Name;
            row.Street = party.Address?.Street;
            row.City = party.Address?.City;
            row.PostalCode = party.Address?.PostalCode ;
            row.Country = party.Address?.Country;
            row.Email = party.Email;
            row.PartyType = (int)party.PartyType;
            row.IsActive = party.IsActive;
            row.CreatedAt=DateTime.Now;
        }

    }
}