using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Attribute;

namespace AnbarService
{
    [Service]
    public class ProductService:IProductService
    {
        public Task SaveAllChanges(DataTable changesTable)
        {
            //await _pro.SaveChangesFromDataTable(partiesTable);
        }
    }
}
