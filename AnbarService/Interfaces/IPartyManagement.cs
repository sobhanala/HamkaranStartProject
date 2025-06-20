﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AnbarDomain.Partys;
using AnbarDomain.Tabels;

namespace AnbarService.Interfaces
{
    public interface IPartyManagement
    {
        Task DeleteParty(int id);

        Task UpdateParty(Party party);

        Task<List<Party>> GetAllParties();
        Task<warhouses> GetPartyDataSetAsync();


        Task AddParty(Party party);
        Task SaveAllChanges(DataTable partiesTable);
        Task<Party> GetPartyById(int id);

    }
}