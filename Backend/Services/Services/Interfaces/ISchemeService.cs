using Services.Dtos.Scheme;

namespace Services.Services.Interfaces
{
    public interface ISchemeService
    {
        Task<SchemeListItemDTO> CreateSchemeAsync(CreateSchemeDTO schemeDto, int userId);
        Task<SchemeDTO> GetSchemeAsync(int schemeId);
        Task<IEnumerable<SchemeListItemDTO>> GetUserSchemesAsync(int userId);
        Task DeleteSchemeAsync(int schemeId);
    }
}
