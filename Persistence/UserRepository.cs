using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Domain.Attribute;
using Domain.Exceptions;
using Domain.Permissons;
using Domain.Repositorys;
using Domain.Users;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Persistence
{
    [Repository]
    public class UserRepository : TypedDataSetRepository<User, int, AnbarProjectDataSet>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;



        public UserRepository(DbConnectionFactory connectionFactory, ILogger<UserRepository> logger)
            : base(connectionFactory, "Users", "Id", GetColumnNames(new AnbarProjectDataSet.UsersDataTable()))
        {
            _logger = logger;
        }

        #region User Operations

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await base.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all users");
                throw new DatabaseException("Failed to get users",
                    "Error retrieving all users from database",
                    ErrorCode.DataBaseError, ex);
            }
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await base.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get user by ID {UserId}", id);
                throw new DatabaseException("Failed to get user",
                    $"Error retrieving user with ID {id}",
                    ErrorCode.DataBaseError, ex);
            }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            try
            {
                var whereClause = "Username = @Username";
                var query = GenerateSelectQuery(TableName, TableColumns, whereClause);
                var parameter = CreateParameter("@Username", username, DbType.String);

                var dataSet = await ExecuteTypedDataSetAsync<AnbarProjectDataSet>(
                    query,
                    CommandType.Text,
                    TableName,
                    parameter);

                return MapSingleResultToEntity(dataSet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get user by username {Username}", username);
                throw new DatabaseException("Failed to get user",
                    $"Error retrieving user with username {username}",
                    ErrorCode.DataBaseError, ex);
            }
        }

        public Task<IEnumerable<Permission>> GetUserPermissions(int userId)
        {
            throw new NotImplementedException();
        }

        public override async Task<int> InsertAsync(User user)
        {
            try
            {
                return await base.InsertAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert user {Username}", user.Username);
                throw new DatabaseException("Failed to insert user",
                    $"Error inserting user {user.Username}",
                    ErrorCode.DataBaseError, ex);
            }
        }

        public override async Task<int> UpdateAsync(User user, string key)
        {
            try
            {
                return await base.UpdateAsync(user, key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update user {UserId}", user.Id);
                throw new DatabaseException("Failed to update user",
                    $"Error updating user with ID {user.Id}",
                    ErrorCode.DataBaseError, ex);
            }
        }

        

        public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId)
        {
            try
            {
                var permissionColumns = GetColumnNames(new AnbarProjectDataSet.PermissionsDataTable());

                var query = GenerateSelectQuery("Permissions", permissionColumns, "UserId = @UserId");
                var parameter = CreateParameter("@UserId", userId, DbType.Int32);

                var dataSet = await ExecuteTypedDataSetAsync<AnbarProjectDataSet>(
                    query,
                    CommandType.Text,
                    "Permissions",
                    parameter);

                return dataSet.Permissions.Select(MapPermissionFromRow).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get user permissions for user {UserId}", userId);
                throw new DatabaseException("Failed to get user permissions",
                    $"Error retrieving permissions for user {userId}",
                    ErrorCode.DataBaseError, ex);
            }
        }




    #endregion

    #region Mapping Methods

    protected override IEnumerable<User> MapResultsToEntities(AnbarProjectDataSet dataSet)
        {
            return dataSet.Users.Select(MapUserFromRow).ToList();
        }

        protected override User MapSingleResultToEntity(AnbarProjectDataSet dataSet)
        {
            return dataSet.Users.Count > 0 ? MapUserFromRow(dataSet.Users[0]) : null;
        }

        protected override IEnumerable<SqlParameter> CreateParametersFromEntity(User user)
        {
            return new[]
            {
                CreateParameter("@Id", user.Id, DbType.Int32),
                CreateParameter("@Username", user.Username, DbType.String),
                CreateParameter("@IsActive", user.IsActive, DbType.Boolean),
                CreateParameter("@CreatedAt", user.CreatedAt, DbType.DateTime),
                CreateParameter("@LastLogin", user.LastLogin ?? DateTime.Now, DbType.DateTime),
                CreateParameter("@Role", (byte)user.Role, DbType.Byte),
                CreateParameter("@PasswordHash", user.PasswordHash, DbType.String)
            };
        }

        private User MapUserFromRow(AnbarProjectDataSet.UsersRow userRow)
        {
            return new User
            {
                Id = userRow.Id,
                Username = userRow.Username,
                IsActive = userRow.IsActive,
                CreatedAt = userRow.CreatedAt,
                LastLogin = userRow.IsLastLoginNull() ? null : (DateTime?)userRow.LastLogin,
                Role = (Roles)userRow.Role,
                PasswordHash = userRow.PasswordHash
            };
        }

        private Permission MapPermissionFromRow(AnbarProjectDataSet.PermissionsRow permRow)
        {
            return new Permission
            {
                Id = permRow.Id,
                UserId = permRow.UserId,
                ModuleId = permRow.ModuleId,
                Authority = (Authority)permRow.Authority,
                CreatedAt = permRow.CreatedAt
            };
        }

        #endregion
    }
}