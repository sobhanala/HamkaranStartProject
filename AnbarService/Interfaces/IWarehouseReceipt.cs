using System.Threading.Tasks;
using AnbarDomain.Tabels;
using Domain.SharedSevices;

namespace AnbarService.Interfaces
{
    public interface IWarehouseReceipt : IMasterDetailService<AnbarDataSet>
    {
        Task<AnbarDataSet.WarehouseReceiptItemsWithProductViewDataTable> FetchDetailsByMasterIdAsync(int receiptId);
        Task<string> GenerateNewReceiptNumber();
    }
}