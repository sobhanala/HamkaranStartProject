using AnbarDomain.Partys;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using Domain.Common;
using Domain.Repositorys;
using Domain.SharedSevices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;



namespace AnbarPersitence
{
    [Repository]
    public class PartyRepository : TypedDataSetRepository<Party, int, warhouses>, IPartyRepository
    {


        private readonly ISessionService _sessionService;


        public PartyRepository(DbConnectionFactory connectionFactory, ILogger<PartyRepository> logger, ISessionService sessionService)
            : base(connectionFactory, "Parties", "Id", GetColumnNames(new warhouses.PartiesDataTable()), logger, sessionService)
        {
            _sessionService = sessionService;
        }





        protected override IEnumerable<Party> MapResultsToEntities(warhouses dataSet)
        {
            return dataSet.Parties.Select(MapToDomainEntity).ToList();
        }

        protected override Party MapSingleResultToEntity(warhouses dataSet)
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


        private Party MapToDomainEntity(warhouses.PartiesRow row)
        {
            return new Party
            {
                Id = row.Id,
                Name = row.Name,
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



    }
}