using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusWS.Data;
using StatusWS.Dtos;

namespace StatusWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StatusController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetStatuses()
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
                        StatusTypeId = s.StatusTypeId,
                        Description = s.StatusType.Description,
                        IconUrl = s.StatusType.IconUrl
                    }
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDto>> GetStatus(int id)
        {
            var status = await _context.Statuses
                .Include(s => s.StatusType)
                .FirstOrDefaultAsync(s => s.StatusId == id);

            if (status == null)
            {
                return NotFound();
            }

            var statusDto = new StatusDto
            {
                StatusId = status.StatusId,
                CustomText = status.CustomText,
                UpdateAt = status.UpdateAt,
                StatusType = new StatusTypeDto
                {
                    StatusTypeId = status.StatusId,
                    Description = status.StatusType.Description,
                    IconUrl = status.StatusType.IconUrl
                }
            };

            return (statusDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {

            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            _context.Statuses.Remove(status);

            try
            {
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}
