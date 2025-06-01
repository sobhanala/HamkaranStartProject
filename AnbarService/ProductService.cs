using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;
using System.Data;
using System.Threading.Tasks;

namespace AnbarService
{
    [Service]
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;



        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task SaveAllChanges(AnbarDataSet.ProductsDataTable changesTable)
        {

            await _productRepository.SaveChangesFromDataTable(changesTable);
        }
        public async Task<AnbarDataSet> GetDataSet()
        {
            return await _productRepository.GetDataSetAsync();
        }

        public async Task<AnbarDataSet.ProductsDataTable> SetProductValues(AnbarDataSet.ProductsDataTable changedTable)
        {

            var count = 1;
            foreach (var row in changedTable)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (row.IsProductCodeNull())
                    {
                        var max = await _productRepository.GetMaxProductCode();
                        row.ProductCode = (max + count).ToString("D6");
                        count++;
                    }
                }
            }

            return changedTable;
        }
    }
}
