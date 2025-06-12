using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Permissons;
using Domain.Users;

namespace Domain.Repositorys.Interfaces
{
    [Obsolete("Obsolete")]
    public interface IUserRepository : IGenericRepository<User, int, UserPermissionDataset>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId);
    }
}