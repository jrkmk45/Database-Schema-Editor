using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface IConnectionRepository
    {
        Task CreateConnectionAsync(Connection connection);
        Task<Connection> GetConnectionAsync(int connectionId);
        Task DeleteConnectionAsync(Connection connection);
    }
}
