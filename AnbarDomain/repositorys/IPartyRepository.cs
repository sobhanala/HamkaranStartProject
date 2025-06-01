using AnbarDomain.Partys;
using AnbarDomain.Tabels;
using Domain.Repositorys;

namespace AnbarDomain.repositorys
{
    public interface IPartyRepository : IGenericRepository<Party, int, AnbarDataSet>
    {
    }
}
