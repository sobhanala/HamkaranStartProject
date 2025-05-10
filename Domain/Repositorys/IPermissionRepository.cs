using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Permissons;

namespace Domain.Repositorys
{
    public interface IPermissionRepository: IGenericRepository<Permission, int>
    {
        Task SyncPermissionsViaDataTable(List<Permission> selectedModuleIds, int userId);

    }
}