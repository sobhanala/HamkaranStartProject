using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Attribute;
using Domain.Exceptions;
using Domain.Module;
using Domain.Permissons;
using Domain.Repositorys;
using Domain.SharedSevices;
using Domain.Users;

namespace Application
{
    [Service]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;

        public UserService(IUserRepository userRepository, IPermissionRepository permissionRepository)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<bool> AddUser(string name, string password, Roles role = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("name is empty", "username is empty", ErrorCode.UserNameEmpty);

            if (string.IsNullOrWhiteSpace(password))
                throw new ValidationException("password is empty", "password is empty", ErrorCode.PasswordEmpty);

            if (await _userRepository.GetByUsernameAsync(name) != null)
                throw new AuthenticationException(".", "User Already Exist", ErrorCode.UserExist);
            var user = new User
            {
                Username = name,
                PasswordHash = password,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                LastLogin = null,
                Role = role
            };

            await _userRepository.InsertAsync(user);
            return true;
        }
        public async Task<List<User>> GetAllUsers(){


            var users = await _userRepository.GetAllAsync();
            return users.Where(u => u.Role != Roles.Admin).ToList();
        }




        public async Task<User> LoginUser(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("name is empty", "username is empty", ErrorCode.UserNameEmpty);


            if (string.IsNullOrWhiteSpace(password))
                throw new ValidationException("password is empty", "password is empty", ErrorCode.PasswordEmpty);

            var u = await _userRepository.GetByUsernameAsync(name);

            if (u == null || !string.Equals(u.PasswordHash, password))
                throw new AuthenticationException(".", "Incorrect", ErrorCode.PasswordWrong);

            return u;
        }


        public async Task AddPermissionToUser(List<int>moduleId,int userId)
        {
            var permission = new List<Permission>();
            foreach (var i in moduleId)
            {
                var m = ModuleManager.Modules[i];
                var per = new Permission
                {
                    ModuleId = m.Id,
                    Authority = Authority.All,
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };  
                permission.Add(per);
            }

            await _permissionRepository.SyncPermissionsViaDataTable(permission,userId);
        }

        public async Task<List<Permission>> ShowAllUserPermission(int userId)
        {
            var permissinons= await _userRepository.GetUserPermissionsAsync(userId);
            return permissinons.ToList();
        }

    }
}