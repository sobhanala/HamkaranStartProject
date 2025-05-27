using System.Threading.Tasks;
using AnbarDomain.Tabels;
using Domain.Repositorys;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptItemRepository:IEnhancedTableAdapter
    {
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FillByReceiptIdWithProductInfo(AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable dataTable,int receiptId);
    }
}