using System.Data;
using System.Threading.Tasks;
using AnbarDomain.Tabels;

namespace AnbarDomain.repositorys
{
    public interface IProductRepository
    {
        Task<AnbarDataSet> GetDataSetAsync();
        Task<int> SaveChangesFromDataTable(DataTable productTable);

    }
}