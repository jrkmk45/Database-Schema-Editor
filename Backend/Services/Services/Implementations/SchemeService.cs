using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.Dtos.Scheme;
using Services.Services.Interfaces;

namespace Services.Services.Implementations
{
    public class SchemeService : ISchemeService
    {
        private readonly ISchemeRepository _schemeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public SchemeService(ISchemeRepository schemeRepository,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _schemeRepository = schemeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<SchemeListItemDTO> CreateSchemeAsync(CreateSchemeDTO schemeDto, int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            var scheme = _mapper.Map<Scheme>(schemeDto);
            scheme.User = user;

            await _schemeRepository.CreateSchemeAsync(scheme);
            var result = await _schemeRepository.GetSchemeAsync(scheme.Id);

            return _mapper.Map<SchemeListItemDTO>(result);
        }

        public async Task<SchemeDTO> GetSchemeAsync(int schemeId)
        {
            var result = await _schemeRepository.GetSchemeAsync(schemeId);

            if (result == null)
                throw new ResourceNotFoundException($"No scheme with id: {schemeId}");

            return _mapper.Map<SchemeDTO>(result);
        }

        public async Task<IEnumerable<SchemeListItemDTO>> GetUserSchemesAsync(int userId)
        {
            var schemes = await _schemeRepository.GetUserSchemesAsync(userId);
            return _mapper.Map<ICollection<SchemeListItemDTO>>(schemes);
        }

        public async Task DeleteSchemeAsync(int schemeId)
        {
            var scheme = await _schemeRepository.GetSchemeAsync(schemeId);
            await _schemeRepository.DeleteSchemeasync(scheme);
        }
    }
}
