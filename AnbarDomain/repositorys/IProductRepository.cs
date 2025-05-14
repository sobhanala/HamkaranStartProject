using System.Data;
using System.Threading.Tasks;
using AnbarDomain.Products;
using AnbarDomain.Tabels;
using Domain.Repositorys;

namespace AnbarDomain.repositorys
{
    public interface IProductRepository:IGenericRepository<Product,int,AnbarDataSet>
    {
        Task<int> GetMaxProductCode();

    }
}