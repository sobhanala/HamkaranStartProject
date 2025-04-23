using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.Module;
using Domain.Permissons;
using Domain.Repositorys;
using Domain.Users;

namespace Persistence
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private const string SelectAll =
            "SELECT Id, Username, IsActive, CreatedAt, LastLogin, Role, PasswordHash FROM Users";

        private const string SelectByUsername =
            "SELECT Id, Username, IsActive, CreatedAt, LastLogin, Role, PasswordHash FROM Users WHERE Username = @Username";

        private const string InsertUser = @"
    INSERT INTO Users (Username, PasswordHash, IsActive, CreatedAt, LastLogin, Role) 
    VALUES (@Username, @PasswordHash, @IsActive, @CreatedAt, @LastLogin, @Role);
    SELECT CAST(SCOPE_IDENTITY() AS INT)";

        private const string SelectUserPermissions = @"
    SELECT p.Id, p.UserId, p.ModuleId,p.Authority,
           p.CreatedAt, m.Name as ModuleName, m.Description as ModuleDescription
    FROM Permissions p
    INNER JOIN Modules m ON p.ModuleId = m.Id
    WHERE p.UserId = @UserId";


        public UserRepository(DbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var parameter =
                CreateParameter("@Username", username, DbType.String);
            var users = await ExecuteReaderQueryAsync(SelectByUsername, CommandType.Text, MapUserFromReader, parameter);
            return users.FirstOrDefault();
        }

        public async Task<int> InsertAsync(User user)
        {
            var parameters = new[]
            {
                CreateParameter("@Username", user.Username, DbType.String),
                CreateParameter("@IsActive", user.IsActive, DbType.Boolean),
                CreateParameter("@CreatedAt", user.CreatedAt, DbType.DateTime),
                CreateParameter("@LastLogin", user.LastLogin, DbType.DateTime),
                CreateParameter("@PasswordHash", user.PasswordHash, DbType.String),
                CreateParameter("@Role", user.Role, DbType.Byte)
            };
            var result = await ExecuteScalarAsync<int>(InsertUser, CommandType.Text, parameters);
            return result;
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await ExecuteReaderQueryAsync(SelectAll, CommandType.Text, MapUserFromReader);
        }

        public async Task<IEnumerable<Permission>> GetUserPermissions(int userId)
        {
            var parameter = CreateParameter("@UserId", userId, DbType.Int32);
            return await ExecuteReaderQueryAsync(SelectUserPermissions, CommandType.Text, MapPermissionFromReader,
                parameter);
        }


        private Permission MapPermissionFromReader(IDataReader reader)
        {
            return new Permission
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                ModuleId = reader.GetInt32(reader.GetOrdinal("ModuleId")),
                Authority = (Authority)reader.GetInt32(reader.GetOrdinal("Authority")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                Module = new Module
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ModuleId")),
                    Name = reader.GetString(reader.GetOrdinal("ModuleName")),
                    Description = reader.IsDBNull(reader.GetOrdinal("ModuleDescription"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("ModuleDescription"))
                }
            };
        }


        private User MapUserFromReader(IDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                LastLogin = reader.IsDBNull(reader.GetOrdinal("LastLogin"))
                    ? (DateTime?)null
                    : reader.GetDateTime(reader.GetOrdinal("LastLogin")),
                Role = (Roles)reader.GetInt32(reader.GetOrdinal("Role")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
            };
        }
    }
}