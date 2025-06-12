using Domain.Attribute;
using Domain.Data;
using Domain.Exceptions;
using Domain.Permissons;
using Domain.Repositorys;
using Domain.SharedSevices;
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
    public class PermissionRepository : TypedDataSetRepository<Permission, int, UserPermissionDataset>,
        IPermissionRepository
    {
        private readonly ILogger<PermissionRepository> _logger;


        public PermissionRepository(DbConnectionFactory connectionFactory, ILogger<PermissionRepository> logger, ISessionService sessionService)
            : base(connectionFactory, "Permissions", "Id", GetColumnNames(new UserPermissionDataset.PermissionsDataTable()), logger, sessionService)
        {
            _logger = logger;
        }


        protected override IEnumerable<Permission> MapResultsToEntities(UserPermissionDataset dataSet)
        {
            try
            {
                return dataSet.Permissions.Select(MapPermissionFromRow).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to map dataset to permissions");
                throw new DatabaseException("Failed to map dataset to permissions",
                    "Error mapping database results to permission entities",
                    ErrorCode.DataBaseError, ex);
            }
        }

        protected override Permission MapSingleResultToEntity(UserPermissionDataset dataSet)
        {
            return dataSet.Permissions.Count > 0 ? MapPermissionFromRow(dataSet.Permissions[0]) : null;
        }

        protected override IEnumerable<SqlParameter> CreateParametersFromEntity(Permission entity)
        {
            return new[]
            {
                CreateParameter("@Id", entity.ModuleId, DbType.Int32),
                CreateParameter("@UserId", entity.UserId, DbType.Int32),
                CreateParameter("@ModuleId", entity.ModuleId, DbType.Int32),
                CreateParameter("@CreatedAt", entity.CreatedAt, DbType.DateTime),
                CreateParameter("@Authority", entity.Authority, DbType.Int32)
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


        public async Task SyncPermissionsViaDataTable(List<Permission> selectedModuleIds, int userId)
        {
            try
            {

                //TODO  I think the permission is not init correctly
                var existing = (await GetAllAsync()).Where(p => p.UserId == userId).ToList();

                var ds = new UserPermissionDataset();
                var permissionTable = ds.Permissions;
                var toInsert = selectedModuleIds.Where(selected => existing.All(e => e.Id != selected.Id));
                foreach (var permission in toInsert)
                {

                    var row = MapPermissionToRow(permission, permissionTable);
                    permissionTable.AddPermissionsRow(row);
                }

                var toDelete = existing
                    .Where(e => selectedModuleIds.All(selected => selected.Id == e.Id));

                foreach (var permission in toDelete)
                {
                    if (!selectedModuleIds.Contains(permission))
                    {
                        var deleteRow = MapPermissionToRow(permission, permissionTable);
                        permissionTable.AddPermissionsRow(deleteRow);
                        deleteRow.AcceptChanges();
                        deleteRow.Delete();
                    }
                }

                var commands = new Dictionary<string, SqlCommand>();

                var insert =
                    new SqlCommand(
                        "INSERT INTO Permissions (UserId, ModuleId, Authority, CreatedAt) VALUES (@UserId, @ModuleId, @Authority, @CreatedAt)");
                insert.Parameters.Add("@UserId", SqlDbType.Int, 0, "UserId");
                insert.Parameters.Add("@ModuleId", SqlDbType.Int, 0, "ModuleId");
                insert.Parameters.Add("@Authority", SqlDbType.Int, 0, "Authority");
                insert.Parameters.Add("@CreatedAt", SqlDbType.DateTime, 0, "CreatedAt");

                var delete = new SqlCommand("DELETE FROM Permissions WHERE UserId = @UserId AND ModuleId = @ModuleId");
                delete.Parameters.Add("@UserId", SqlDbType.Int, 0, "UserId");
                delete.Parameters.Add("@ModuleId", SqlDbType.Int, 0, "ModuleId");

                commands.Add("Insert", insert);
                commands.Add("Delete", delete);

                await ExecuteDataAdapterUpdateAsyncOnDataset(ds, "Permissions", commands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to synchronize permissions for user {UserId}", userId);
                throw new DatabaseException("Failed to synchronize permissions",
                    $"Error synchronizing permissions for user {userId}",
                    ErrorCode.DataBaseError, ex);
            }

        }


        private UserPermissionDataset.PermissionsRow MapPermissionToRow(Permission permission, UserPermissionDataset.PermissionsDataTable permissionTable)
        {
            var row = permissionTable.NewPermissionsRow();
            row.UserId = permission.UserId;
            row.ModuleId = permission.ModuleId;
            row.Authority = (int)permission.Authority;
            row.CreatedAt = permission.CreatedAt;
            return row;
        }
    }
}


