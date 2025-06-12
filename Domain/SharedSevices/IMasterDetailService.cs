using System.Threading.Tasks;

namespace Domain.SharedSevices
{
    public interface IMasterDetailService<TDataSet>
    {
        Task<TDataSet> GetFullDatasetAsync();
        Task FillReceiptById(TDataSet dataSet,int id);
        Task SaveMasterDetailAsync(TDataSet dataset);
        Task DeleteMasterDetailAsync(int id);
    }
}