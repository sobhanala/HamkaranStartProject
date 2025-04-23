using System.Threading.Tasks;
using Domain.Users;

namespace Application
{
    public interface IUserService
    {
        Task<bool> AddUser(string name, string password, Roles role = Roles.User);
        Task<User> LoginUser(string name, string password);
    }
}