using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AnbarDomain.Partys;
using AnbarDomain.Tabels;
using Domain.Repositorys;
using Domain.Users;

namespace AnbarDomain.repositorys
{
    public interface IPartyRepository : IGenericRepository<Party, int,AnbarDataSet>
    {
    }
}
