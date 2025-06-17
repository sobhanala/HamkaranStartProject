using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;
using Domain.Repositorys.Interfaces;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptRepository : IMasterBase
    {
        Task<string> GetNextReceiptNumberAsync();
        Task<AnbarDataSet.view_WarehouseReceiptsDataTable> FetchAsync();
    }
}