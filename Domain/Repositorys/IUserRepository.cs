using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Permissons;
using Domain.Users;

namespace Domain.Repositorys
{
    public interface IUserRepository: IGenericRepository<User, int>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<Permission>> GetUserPermissions(int userId);
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId);
        Task AddPermissionAsync(IEnumerable<Permission> permissions);
        Task UpdatePermissionAsync(Permission permission);
        Task RemovePermissionAsync(int userId, int moduleId);


    }
}