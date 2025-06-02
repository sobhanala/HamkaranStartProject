using AnbarDomain.Tabels;
using Domain.Repositorys;
using System.Threading.Tasks;

namespace AnbarDomain.repositorys
{
    public interface IInventoryRepository : IEnhancedTableAdapter
    {

        Task<AnbarDataSet.InventoryDataTable> GetAvailableStock(int productId, int warehouseId);
        Task<int> UpdateATrack(int productId, int warehouseId, int finalVal);
    }
}