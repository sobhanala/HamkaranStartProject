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
using Infrastructure;
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

        public override async Task<int> UpdateAsync(User user,string key)
        {
            try
            {
                return await base.UpdateAsync(user,key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update user {UserId}", user.Id);
                throw new DatabaseException("Failed to update user",
                    $"Error updating user with ID {user.Id}",
                    ErrorCode.DataBaseError, ex);
            }
        }

        #endregion

        #region Permission Operations

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

        public async Task AddPermissionAsync(IEnumerable<Permission> permissions)
        {
            try
            {
                if (!permissions.Any())
                    return;

                var userId = permissions.First().UserId;
                var userParam = CreateParameter("@Id", userId, DbType.Int32);

                var dataset = await ExecuteTypedDataSetAsync<AnbarProjectDataSet>(
                    GenerateSelectQuery(TableName, TableColumns, "Id = @Id"),
                    CommandType.Text,
                    TableName,
                    userParam);

                foreach (var permission in permissions)
                {
                    var row = dataset.Permissions.NewPermissionsRow();
                    row.ModuleId = permission.ModuleId;
                    row.Authority = (int)permission.Authority;
                    row.UserId = permission.UserId;
                    row.CreatedAt = permission.CreatedAt;
                    dataset.Permissions.AddPermissionsRow(row);
                }

                var permissionColumns = GetColumnNames(new AnbarProjectDataSet.PermissionsDataTable());
                
                                var insertCommand = new SqlCommand(
                    GenerateInsertQuery("Permissions", permissionColumns.Where(c => c != "Id")));

                insertCommand.Parameters.Add("@UserId", SqlDbType.Int).SourceColumn = "UserId";
                insertCommand.Parameters.Add("@ModuleId", SqlDbType.Int).SourceColumn = "ModuleId";
                insertCommand.Parameters.Add("@Authority", SqlDbType.Int).SourceColumn = "Authority";
                insertCommand.Parameters.Add("@CreatedAt", SqlDbType.DateTime2).SourceColumn = "CreatedAt";

                // Execute the update
                var commands = new Dictionary<string, SqlCommand> { { "Insert", insertCommand } };
                await ExecuteDataAdapterUpdateAsync(dataset, "Permissions", commands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add permissions for user {UserId}",
                    permissions.FirstOrDefault()?.UserId);

                throw new DatabaseException("Failed to add permissions",
                    "Error adding user permissions",
                    ErrorCode.DataBaseError, ex);
            }
        }


        public async Task UpdatePermissionAsync(Permission permission)
        {
            try
            {
                var query = "UPDATE Permissions SET Authority = @Authority " +
                            "WHERE UserId = @UserId AND ModuleId = @ModuleId";

                var parameters = new[]
                {
                    CreateParameter("@UserId", permission.UserId, DbType.Int32),
                    CreateParameter("@ModuleId", permission.ModuleId, DbType.Int32),
                    CreateParameter("@Authority", (int)permission.Authority, DbType.Int32)
                };

                await ExecuteWriterCommandAsync(query, CommandType.Text, parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update permission for user {UserId}", permission.UserId);
                throw new DatabaseException("Failed to update permission",
                    $"Error updating permission for user {permission.UserId}",
                    ErrorCode.DataBaseError, ex);
            }
        }

        public async Task RemovePermissionAsync(int userId, int moduleId)
        {
            try
            {
                var query = "DELETE FROM Permissions WHERE UserId = @UserId AND ModuleId = @ModuleId";

                var parameters = new[]
                {
                    CreateParameter("@UserId", userId, DbType.Int32),
                    CreateParameter("@ModuleId", moduleId, DbType.Int32)
                };

                await ExecuteWriterCommandAsync(query, CommandType.Text, parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to remove permission for user {UserId}", userId);
                throw new DatabaseException("Failed to remove permission",
                    $"Error removing permission for user {userId}",
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