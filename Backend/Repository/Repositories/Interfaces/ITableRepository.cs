using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface ITableRepository
    {
        Task CreateTableAsync(Table table);
        Task<Table> GetTableAsync(int tableId);
        Task UpdateTableAsync(Table table);
        Task DeleteTableAsync(Table table);
    }
}
