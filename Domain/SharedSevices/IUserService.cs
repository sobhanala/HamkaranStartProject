using Domain.Permissons;
using Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.SharedSevices
{
    public interface IUserService
    {
        Task<bool> AddUser(string name, string password, Roles role = Roles.User);
        Task<User> LoginUser(string name, string password);
        Task<List<User>> GetAllUsers();
        Task AddPermissionToUser(List<int> moduleId, int useId);
        Task<List<Permission>> ShowAllUserPermission(int userId);
        Task<User> GetUserById(int userId);
    }
}