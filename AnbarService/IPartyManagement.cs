using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AnbarDomain.Partys;
using Domain.Common;
using Domain.Users;

namespace AnbarService
{
    public interface IPartyManagement
    {
        Task DeleteParty(int id);

        Task UpdateParty(Party party);

        Task<List<Party>> GetAllParties();

        Task AddParty(Party party);
        Task SaveAllChanges(DataTable partiesTable);


    }
}