using AnbarDomain.Tabels;
using System.Threading.Tasks;

namespace AnbarService
{

    public interface IProductService
    {
        Task SaveAllChanges(ProductDataset.ProductsDataTable changesTable);
        Task<ProductDataset> GetDataSet();
        Task<ProductDataset.ProductsDataTable> SetProductValues(ProductDataset.ProductsDataTable changedTable);

    }
}
