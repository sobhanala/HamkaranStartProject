using System.Threading.Tasks;
using AnbarDomain.Tabels;

namespace AnbarDomain.repositorys
{
    public interface IWarehouseReceiptItemRepository
    {
        Task<AnbarDataSet> FillByReceiptIdWithProductInfo(int receiptId);
    }
}