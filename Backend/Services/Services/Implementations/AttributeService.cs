using AutoMapper;
using Domain.Exceptions;
using Repository.Repositories.Interfaces;
using Services.Dtos.Attribute;
using Services.Services.Interfaces;
using Attribute = Domain.Models.Attribute;

namespace Services.Services.Implementations
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;
        public AttributeService(IAttributeRepository attributeRepository,
            ITableRepository tableRepository,
            IMapper mapper) 
        {
            _attributeRepository = attributeRepository;
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<AttributeDTO> CreateAttributeAsync(int tableId, int userId, CreateAttributeDTO createAttributeDTO)
        {
            var table = await _tableRepository.GetTableAsync(tableId);
            if (table == null)
                throw new ResourceNotFoundException($"No table with id: {tableId}");

            var attribute = _mapper.Map<Attribute>(createAttributeDTO);
            attribute.UserId = userId;
            attribute.TableId = tableId;
            await _attributeRepository.CreateAttributeAsync(attribute);

            var result = await _attributeRepository.GetAttributeAsync(attribute.Id);
            return _mapper.Map<AttributeDTO>(result);
        }

        public async Task DeleteAttributeAsync(int attributeId)
        {
            var attrbiute = await _attributeRepository.GetAttributeAsync(attributeId);
            if (attrbiute == null)
                throw new ResourceNotFoundException($"No attribute with id: {attributeId}");

            await _attributeRepository.DeleteAttributeAsync(attrbiute);
        }

        public async Task DeleteAttributeConnectionsAsync(int attributeId)
        {
            var attribute = await _attributeRepository.GetAttributeAsync(attributeId);
            if (attribute == null)
                throw new ForbiddenResourceException($"No atrribute with id: {attributeId}");

            await _attributeRepository.DeleteAttributeConnectionsAsync(attributeId);
        }
    }
}
