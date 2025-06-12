using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositorys;
using Domain.Repositorys.Interfaces;

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


        protected abstract Task<THeaderTable> FetchMasterAsync();

        public  abstract Task<TDetailTable> FetchDetailsByMasterIdAsync(int masterId);

    protected virtual Task BeforeSaveAsync(TDataSet dataset)
        {
            return Task.CompletedTask;
        }

        protected virtual Task AfterSaveAsync(TDataSet dataset)
        {
            return Task.CompletedTask;
        }

        protected virtual Task BeforeDeleteAsync(TDataSet dataset)
        {
            return Task.CompletedTask;
        }

        protected virtual Task AfterDeleteAsync(TDataSet dataset)
        {
            return Task.CompletedTask;
        }


        public virtual  async Task<TDataSet> GetFullDatasetAsync()
        {
            var dataSet = new TDataSet();
            await _headerRepo.FillAsync(dataSet.GetHeaderTable());
            return dataSet;
        }

        public async Task FillReceiptById(TDataSet dataSet, int receiptId)
        {
            await _headerRepo.GetByIdAsync(dataSet.GetHeaderTable(), receiptId);
            await _detailRepo.FillByForeignKeyAsync(dataSet.GetDetailTable(), receiptId);

        }
        public  async Task<TDataSet> FetchMasterDetailDatasetAsync(int masterId)
        {
            var ds = new TDataSet();
            await _headerRepo.GetByIdAsync(ds.GetHeaderTable(), masterId);
            await _detailRepo.FillByForeignKeyAsync(ds.GetDetailTable(), masterId);
            return ds;
        }



        public virtual async Task SaveMasterDetailAsync(TDataSet dataset)
        {
             _transactionManager.BeginTransactionAsync();
            try
            {
                await BeforeSaveAsync(dataset);
                await _headerRepo.UpdateAsync(dataset.GetHeaderTable());

                await _detailRepo.UpdateAsync(dataset.GetDetailTable());

                await AfterSaveAsync(dataset);

                _transactionManager.CommitTransactionAsync();
                dataset.AcceptChanges();
              
            }
            catch
            {
                 _transactionManager.RollbackTransactionAsync();
                throw;
            }
        }

        public virtual async Task DeleteMasterDetailAsync(int id)
        {
            _transactionManager.BeginTransactionAsync();
            try
            {

                 var dataset = await FetchMasterDetailDatasetAsync(id);
                await BeforeDeleteAsync(dataset);

                await _detailRepo.DeleteByForeignKeyAsync(id);
                await _headerRepo.DeleteByIdAsync(id);
                await AfterDeleteAsync(dataset);
                _transactionManager.CommitTransactionAsync();
            }
            catch
            {
                _transactionManager.RollbackTransactionAsync();
                throw;
            }
        }


    }
}
