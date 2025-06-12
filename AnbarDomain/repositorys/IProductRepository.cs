using System;
using AnbarDomain.Products;
using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;
using Domain.Repositorys.Interfaces;

namespace AnbarDomain.repositorys
{
    [Obsolete("Obsolete")]
    public interface IProductRepository : IGenericRepository<Product, int, ProductDataset>
    {
        Task<int> GetMaxProductCode();

    }
}