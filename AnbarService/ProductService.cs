using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnbarDomain.repositorys;
using AnbarDomain.Tabels;
using Domain.Attribute;

namespace AnbarService
{
    [Service]
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;



        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task SaveAllChanges(DataTable changesTable)
        {

            await _productRepository.SaveChangesFromDataTable(changesTable);
        }
        public async Task<AnbarDataSet> GetDataSet()
        {
           return  await _productRepository.GetDataSetAsync();
        }


    }
}
