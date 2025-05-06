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


        protected override IEnumerable<Party> MapResultsToEntities(AnbarDataSet dataSet)
        {
            return dataSet.Parties.Select(MapToDomainEntity).ToList();
        }

        protected override Party MapSingleResultToEntity(AnbarDataSet dataSet)
        {
            return dataSet.Parties.Count > 0 ? MapToDomainEntity(dataSet.Parties[0]) : null;
        }

        protected override IEnumerable<SqlParameter> CreateParametersFromEntity(Party entity)
        {
            return new[]
            {
                CreateParameter("@Username", entity.Name, DbType.String),
                CreateParameter("@IsActive", entity.IsActive, DbType.Boolean),
                CreateParameter("@CreatedAt", entity.CreatedAt, DbType.DateTime),
                CreateParameter("@LastLogin", entity.LastUpdated ?? DateTime.Now, DbType.DateTime),
                CreateParameter("@Role", entity.IsActive, DbType.Byte),
                CreateParameter("@PasswordHash", entity.Address, DbType.String),
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
                IsActive = row.IsActive
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
            row.LastUpdated=DateTime.Now;
        }

        private void AddPartyParameters(IDbCommand command, Party party)
        {
            command.Parameters.Add(CreateParameter("@Name", party.Name, DbType.String));
            command.Parameters.Add(CreateParameter("@Street", party.Address?.Street, DbType.String));
            command.Parameters.Add(CreateParameter("@City", party.Address?.City, DbType.String));
            command.Parameters.Add(CreateParameter("@PostalCode", party.Address?.PostalCode, DbType.String));
            command.Parameters.Add(CreateParameter("@Country", party.Address?.Country, DbType.String));
            command.Parameters.Add(CreateParameter("@Email", party.Email, DbType.String));
            command.Parameters.Add(CreateParameter("@PartyType", (int)party.PartyType, DbType.Int32));
            command.Parameters.Add(CreateParameter("@IsActive", party.IsActive, DbType.Boolean));
            command.Parameters.Add(CreateParameter("@CreatedAt", DateTime.Now, DbType.DateTime));
            command.Parameters.Add(CreateParameter("@LastUpdated", DateTime.Now, DbType.DateTime));
        }
    }
}