using System;
using AnbarDomain.Partys;
using AnbarDomain.Tabels;
using Domain.Repositorys;
using Domain.Repositorys.Interfaces;

namespace AnbarDomain.repositorys
{
    [Obsolete("Obsolete")]
    public interface IPartyRepository : IGenericRepository<Party, int, warhouses>
    {
    }
}
