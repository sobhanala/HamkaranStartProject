using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Permissons;
using Domain.Users;

namespace Domain.Repositorys
{
    public interface IUserRepository: IGenericRepository<User, int, AnbarProjectDataSet>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId);
    }
}