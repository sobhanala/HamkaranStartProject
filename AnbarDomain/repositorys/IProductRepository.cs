using AnbarDomain.Products;
using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;

namespace AnbarDomain.repositorys
{
    public interface IProductRepository : IGenericRepository<Product, int, ProductDataset>
    {
        Task<int> GetMaxProductCode();

    }
}