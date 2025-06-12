namespace Domain.Repositorys.Interfaces
{
    public interface IEnhancedDataTableMetadata
    {
        string tableName { get; }
        string viewName { get; }
        string fkMasterDetail { get; }
    }
}