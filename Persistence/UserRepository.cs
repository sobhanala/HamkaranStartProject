using Domain.Attribute;
using Domain.Data;
using Domain.Exceptions;
using Domain.Permissons;
using Domain.Repositorys;
using Domain.SharedSevices;
using Domain.Users;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositorys.Interfaces;

namespace Persistence
{
    [Repository]
    [Obsolete("Obsolete")]
    public class UserRepository : TypedDataSetRepository<User, int, UserPermissionDataset>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;


        public UserRepository(DbConnectionFactory connectionFactory, ILogger<UserRepository> logger, ISessionService sessionService)
            : base(connectionFactory, "Users", "Id", GetColumnNames(new UserPermissionDataset.UsersDataTable()), logger, sessionService)
        {
            _logger = logger;
        }

        #region User Operations



        public async Task<User> GetByUsernameAsync(string username)
        {
            try
            {
                var whereClause = "Username = @Username";
                var query = GenerateSelectQuery(TableName, TableColumns, whereClause);
                var parameter = CreateParameter("@Username", username, DbType.String);

                var dataSet = await ExecuteTypedDataSetAsync<UserPermissionDataset>(
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




        public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId)
        {
            try
            {
                var permissionColumns = GetColumnNames(new UserPermissionDataset.PermissionsDataTable());

                var query = GenerateSelectQuery("Permissions", permissionColumns, "UserId = @UserId");
                var parameter = CreateParameter("@UserId", userId, DbType.Int32);

                var dataSet = await ExecuteTypedDataSetAsync<UserPermissionDataset>(
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

        protected override IEnumerable<User> MapResultsToEntities(UserPermissionDataset dataSet)
        {
            return dataSet.Users.Select(MapUserFromRow).ToList();
        }

        protected override User MapSingleResultToEntity(UserPermissionDataset dataSet)
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

        private User MapUserFromRow(UserPermissionDataset.UsersRow userRow)
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

        private Permission MapPermissionFromRow(UserPermissionDataset.PermissionsRow permRow)
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