using Services.Dtos.Connection;

namespace Services.Services.Interfaces
{
    public interface IConnectionService
    {
        Task<ConnectionDTO> CreateConnectionAsync(int attributeId, int userId, CreateConnectionDTO createConnectionDTO);
    }
}
