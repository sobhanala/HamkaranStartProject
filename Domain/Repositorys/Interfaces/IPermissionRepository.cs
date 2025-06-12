using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Permissons;

namespace Domain.Repositorys.Interfaces
{
    [Obsolete("Obsolete")]
    public interface IPermissionRepository : IGenericRepository<Permission, int, UserPermissionDataset>
    {
        Task SyncPermissionsViaDataTable(List<Permission> selectedModuleIds, int userId);

    }
}