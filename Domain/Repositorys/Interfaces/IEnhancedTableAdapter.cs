﻿using System;
using System.Data;
using System.Threading.Tasks;

namespace Domain.Repositorys.Interfaces
{
    public interface IEnhancedTableAdapter : IDisposable
    {
        Task<int> FillAsync(DataTable dataTable);
        Task<int> UpdateAsync(DataTable dataTable);
        Task<int> GetByIdAsync(DataTable dataTable, int id);
        Task<int> DeleteByIdAsync(int id);

    }
}