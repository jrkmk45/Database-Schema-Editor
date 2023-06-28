using Services.Dtos.Table;

namespace Services.Services.Interfaces
{
    public interface ITableService
    {
        Task<TableDTO> CreateTableAsync(int schemeId, int userId, CreateTableDTO createTableDTO);
        Task PatchTableAsync(int tableId, PatchTableDTO table);
        Task DeleteTableAsync(int tableId);
    }
}
