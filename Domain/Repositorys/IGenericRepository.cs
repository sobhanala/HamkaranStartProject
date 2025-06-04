using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;



namespace Domain.Repositorys
{

    [Obsolete()]//TODO 
    public interface IGenericRepository<TEntity, TKey, TDataset> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity, string key = "");
        Task<int> DeleteAsync(TKey id, string key = "");
        Task<int> SaveChangesFromDataTable(DataTable productTable);
        Task<TDataset> GetDataSetAsync();
        Task<int> SaveChangesFromDataSet(TDataset dataSet);


    }
}