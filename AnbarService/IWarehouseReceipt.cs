using AnbarDomain.Tabels;
using System.Threading.Tasks;
using Domain.SharedSevices;

namespace AnbarService
{
    public interface IWarehouseReceipt : IMasterDetailService<AnbarDataSet>
    {
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FetchDetailsByMasterIdAsync(int receiptId);
        Task<string> GenerateNewReceiptNumber();
    }
}