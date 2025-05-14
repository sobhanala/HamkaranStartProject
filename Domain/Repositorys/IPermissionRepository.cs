﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Permissons;

namespace Domain.Repositorys
{
    public interface IPermissionRepository: IGenericRepository<Permission, int,AnbarProjectDataSet>
    {
        Task SyncPermissionsViaDataTable(List<Permission> selectedModuleIds, int userId);

    }
}