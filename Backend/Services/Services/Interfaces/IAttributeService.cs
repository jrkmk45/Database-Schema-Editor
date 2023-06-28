using Services.Dtos.Attribute;

namespace Services.Services.Interfaces
{
    public interface IAttributeService
    {
        Task<AttributeDTO> CreateAttributeAsync(int tableId, int userId, CreateAttributeDTO createAttributeDTO);
        Task DeleteAttributeAsync(int attributeId);
        Task DeleteAttributeConnectionsAsync(int attributeId);
    }
}
