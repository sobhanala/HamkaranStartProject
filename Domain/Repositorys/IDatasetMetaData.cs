using System.Data;

namespace Domain.Repositorys
{
    public interface IDatasetMetaData<THeaderTable, TDetailTable>
        where THeaderTable : DataTable
        where TDetailTable : DataTable
    {
        THeaderTable GetHeaderTable();
        TDetailTable GetDetailTable();
    }
}