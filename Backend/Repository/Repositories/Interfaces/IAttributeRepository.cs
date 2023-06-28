using Attribute = Domain.Models.Attribute;

namespace Repository.Repositories.Interfaces
{
    public interface IAttributeRepository
    {
        Task CreateAttributeAsync(Attribute attribute);
        Task<Attribute> GetAttributeAsync(int attributeId);
        Task DeleteAttributeAsync(Attribute attribute);
        Task DeleteAttributeConnectionsAsync(int attributeId);
    }
}
