using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Attribute;

namespace AnbarService
{

    public interface IProductService
    {
        Task SaveAllChanges(DataTable changesTable);
    }
}
