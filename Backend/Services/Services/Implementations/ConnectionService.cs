using AutoMapper;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.Dtos.Connection;
using Services.Services.Interfaces;

namespace Services.Services.Implementations
{
    public class ConnectionService : IConnectionService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IMapper _mapper;
        
        public ConnectionService(IAttributeRepository attributeRepository,
            IConnectionRepository connectionRepository,
            IMapper mapper)
        {
            _attributeRepository = attributeRepository;
            _connectionRepository = connectionRepository;
            _mapper = mapper;
        }

        public async Task<ConnectionDTO> CreateConnectionAsync(int attributeId, int userId, CreateConnectionDTO createConnectionDTO)
        {
            var attribute = await _attributeRepository.GetAttributeAsync(attributeId);
            if (attribute == null)
                throw new ResourceNotFoundException($"No attribute with id {attributeId}");

            var connection = _mapper.Map<Connection>(createConnectionDTO);

            if (connection.AttributeToId == connection.AttributeFromId)
                throw new ArgumentException("Could'nt create connection with self");

            connection.UserId = userId;
            await _connectionRepository.CreateConnectionAsync(connection);

            var result = await _connectionRepository.GetConnectionAsync(connection.Id);
            return _mapper.Map<ConnectionDTO>(result);
        }
    }
}
