using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositorys
{
    public interface IGenericRepository<TEntity,TKey> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity,string key="");
        Task<int> DeleteAsync(TKey id,string key="");
    }
}