using AnbarDomain.Tabels;
using System.Threading.Tasks;

namespace AnbarService
{

    public interface IProductService
    {
        Task SaveAllChanges(AnbarDataSet.ProductsDataTable changesTable);
        Task<AnbarDataSet> GetDataSet();
        Task<AnbarDataSet.ProductsDataTable> SetProductValues(AnbarDataSet.ProductsDataTable changedTable);

    }
}
