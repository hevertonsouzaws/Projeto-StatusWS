using Microsoft.EntityFrameworkCore;
using StatusWS.Data;
using StatusWS.Dtos;
using StatusWS.Errors;

namespace StatusWS.Services
{
    public class StatusService : IStatusService
    {
        private readonly AppDbContext _context;

        public StatusService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
        {
            return await _context.Statuses
                .Include(s => s.StatusType)
                .Select(s => new StatusDto
                {
                    StatusId = s.StatusId,
                    CustomText = s.CustomText,
                    UpdateAt = s.UpdateAt,
                    StatusType = new StatusTypeDto
                    {
                        StatusTypeId = s.StatusType.StatusTypeId,
                        Description = s.StatusType.Description,
                        IconUrl = s.StatusType.IconUrl
                    }
                })
                .ToListAsync();
        }

        public async Task<StatusDto> GetStatusByIdAsync(int id)
        {
            var status = await _context.Statuses
                .Include(s => s.StatusType)
                .FirstOrDefaultAsync(s => s.StatusId == id);

            if (status == null)
            {
                throw new NotFoundException($"Status com ID {id} não foi encontrado.");
            }

            return new StatusDto
            {
                StatusId = status.StatusId,
                CustomText = status.CustomText,
                UpdateAt = status.UpdateAt,
                StatusType = new StatusTypeDto
                {
                    StatusTypeId = status.StatusType.StatusTypeId, 
                    Description = status.StatusType.Description,
                    IconUrl = status.StatusType.IconUrl
                }
            };
        }

        public async Task DeleteStatusAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                throw new NotFoundException($"Status com ID {id} não pode ser excluído, pois não foi encontrado.");
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();
        }
    }
}
