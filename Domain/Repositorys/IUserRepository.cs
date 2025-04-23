using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Permissons;
using Domain.Users;

namespace Domain.Repositorys
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<int> InsertAsync(User user);
        Task<IEnumerable<Permission>> GetUserPermissions(int userId);
        Task<IEnumerable<User>> GetAllAsync();
    }
}