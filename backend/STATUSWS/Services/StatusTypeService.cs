using Microsoft.EntityFrameworkCore;
using StatusWS.Data;
using StatusWS.Dtos;
using StatusWS.Errors;
using StatusWS.Models;

namespace StatusWS.Services
{
    public class StatusTypeService : IStatusTypeService
    {
        private readonly AppDbContext _context;

        public StatusTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StatusTypeDto>> GetAllStatusTypesAsync()
        {
            return await _context.StatusTypes
                .Select(st => new StatusTypeDto
                {
                    StatusTypeId = st.StatusTypeId,
                    Description = st.Description,
                    IconUrl = st.IconUrl
                })
                .ToListAsync();
        }

        public async Task<StatusTypeDto> GetStatusTypeByIdAsync(int id)
        {
            var statusType = await _context.StatusTypes.FirstOrDefaultAsync(st => st.StatusTypeId == id);

            if (statusType == null)
            {
                throw new NotFoundException($"Tipo de status com ID {id} não foi encontrado.");
            }

            return new StatusTypeDto
            {
                StatusTypeId = statusType.StatusTypeId,
                Description = statusType.Description,
                IconUrl = statusType.IconUrl
            };
        }

        public async Task<StatusTypeDto> CreateStatusTypeAsync(StatusTypeCreateDto statusTypeCreateDto)
        {
            var statusType = new StatusType
            {
                Description = statusTypeCreateDto.Description,
                IconUrl = statusTypeCreateDto.IconUrl ?? "https://tarefas.websupply.com.br/painel/assets/StatusGeolocalizacao-DxUl3vfK.png"
            };

            _context.StatusTypes.Add(statusType);
            await _context.SaveChangesAsync();

            return new StatusTypeDto
            {
                StatusTypeId = statusType.StatusTypeId,
                Description = statusType.Description,
                IconUrl = statusType.IconUrl
            };
        }

        public async Task UpdateStatusTypeAsync(int id, StatusTypeCreateDto statusTypeCreateDto)
        {
            var statusType = await _context.StatusTypes.FindAsync(id);

            if (statusType == null)
            {
                throw new NotFoundException($"Tipo de status com ID {id} não pode ser atualizado, pois não foi encontrado.");
            }

            statusType.Description = statusTypeCreateDto.Description;
            statusType.IconUrl = statusTypeCreateDto.IconUrl ?? "https://tarefas.websupply.com.br/painel/assets/StatusGeolocalizacao-DxUl3vfK.png";

            await _context.SaveChangesAsync();
        }

        public async Task DeleteStatusTypeAsync(int id)
        {
            var statusType = await _context.StatusTypes.FindAsync(id);

            if (statusType == null)
            {
                throw new NotFoundException($"Tipo de status com ID {id} não pode ser excluído, pois não foi encontrado.");
            }

            _context.StatusTypes.Remove(statusType);
            await _context.SaveChangesAsync();
        }
    }
}
