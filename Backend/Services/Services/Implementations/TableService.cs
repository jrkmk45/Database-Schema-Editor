using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.Dtos.Table;
using Services.Services.Interfaces;

namespace Services.Services.Implementations
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly ISchemeRepository _schemeRepository;
        private readonly IMapper _mapper;
        public TableService(ITableRepository tableRepository,
            ISchemeRepository schemeRepository,
            IMapper mapper)
        {
            _tableRepository = tableRepository;
            _schemeRepository = schemeRepository;
            _mapper = mapper;
        }
        public async Task<TableDTO> CreateTableAsync(int schemeId, int userId, CreateTableDTO createTableDTO)
        {
            var scheme = await _schemeRepository.GetSchemeAsync(schemeId);
            if (scheme == null)
                throw new ResourceNotFoundException($"No scheme with id {schemeId}");

            var table = _mapper.Map<Table>(createTableDTO);

            table.UserId = userId;
            table.Scheme = scheme;

            await _tableRepository.CreateTableAsync(table);

            var result = await _tableRepository.GetTableAsync(table.Id);
            return _mapper.Map<TableDTO>(result);
        }

        public async Task PatchTableAsync(int tableId, PatchTableDTO patchTableDTO)
        {
            var table = await _tableRepository.GetTableAsync(tableId);
            if (table == null)
                throw new ResourceNotFoundException($"No table with id {tableId}");

        //    _mapper.Map(patchTableDTO, table);
           
            if (patchTableDTO.X != null)
                table.X = (int)patchTableDTO.X;

            if (patchTableDTO.Y != null)
                table.Y = (int)patchTableDTO.Y;

            if (patchTableDTO.Name != null)
                table.Name = patchTableDTO.Name;

            if (patchTableDTO.Attributes != null)
            {
                table.Attributes = patchTableDTO.Attributes.Zip(table.Attributes, (first, second) => _mapper.Map(first, second));
            }
            
            await _tableRepository.UpdateTableAsync(table);
        }
    
        public async Task DeleteTableAsync(int tableId)
        {
            var table = await _tableRepository.GetTableAsync(tableId);
            await _tableRepository.DeleteTableAsync(table);
        }
    }
}
