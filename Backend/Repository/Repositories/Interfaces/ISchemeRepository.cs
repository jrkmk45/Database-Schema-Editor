using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface ISchemeRepository
    {
        Task CreateSchemeAsync(Scheme scheme);
        Task<Scheme> GetSchemeAsync(int schemeId);
        Task<IEnumerable<Scheme>> GetUserSchemesAsync(int userId);
        Task DeleteSchemeasync(Scheme scheme);
    }
}
