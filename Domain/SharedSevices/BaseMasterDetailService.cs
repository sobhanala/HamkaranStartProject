using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositorys;

namespace Domain.SharedSevices
{
    public abstract class BaseMasterDetailService<THeaderRepo,TDetailRepo,TDataSet, THeaderTable, TDetailTable>
    where THeaderRepo:class,IEnhancedTableAdapter
    where TDetailRepo:class, IEnhancedTableAdapter
    where TDataSet : DataSet, IDatasetMetaData<THeaderTable,TDetailTable>,new()   
    where THeaderTable:DataTable
    where TDetailTable:DataTable
    {
        protected readonly THeaderRepo _headerRepo;
        protected readonly TDetailRepo _detailRepo;
        protected readonly ITransactionManager _transactionManager;

        protected BaseMasterDetailService(
            THeaderRepo headerRepo,
            TDetailRepo detailRepo,
            ITransactionManager transactionManager)
        {
            _headerRepo = headerRepo;
            _detailRepo = detailRepo;
            _transactionManager = transactionManager;
        }

        public virtual  async Task<TDataSet> GetFullDatasetAsync()
        {
            var dataSet = new TDataSet();

            await _headerRepo.FillAsync(dataSet.GetHeaderTable());
            await _detailRepo.FillAsync(dataSet.GetDetailTable());

            return dataSet;
        }

        public async Task FillReceiptById(TDataSet dataSet, int receiptId)
        {
            await FillHeaderByIdAsync(dataSet, receiptId);
            await FillDetailByHeaderIdAsync(dataSet, receiptId);
        }



    }
}
