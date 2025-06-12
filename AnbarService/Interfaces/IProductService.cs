using System.Threading.Tasks;
using AnbarDomain.Tabels;

namespace AnbarService.Interfaces
{

    public interface IProductService
    {
        Task SaveAllChanges(ProductDataset.ProductsDataTable changesTable);
        Task<ProductDataset> GetDataSet();
        Task<ProductDataset.ProductsDataTable> SetProductValues(ProductDataset.ProductsDataTable changedTable);

    }
}
