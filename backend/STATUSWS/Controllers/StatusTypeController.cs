using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusWS.Data;
using StatusWS.Dtos;
using StatusWS.Models;


namespace StatusWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatusTypeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusTypeDto>>> GetStatusTypes()
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

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusTypeDto>> GetStatusType(int id)
        {
            var statusType = await _context.StatusTypes.FirstOrDefaultAsync(st => st.StatusTypeId == id);

            if (statusType == null)
            {
                return NotFound();
            }

            var statusTypeDto = new StatusTypeDto
            {
                StatusTypeId = statusType.StatusTypeId,
                Description = statusType.Description,
                IconUrl = statusType.IconUrl
            };

            return statusTypeDto;
        }

        [HttpPost]
        public async Task<ActionResult<StatusTypeDto>> PostStatusType(StatusTypeCreateDto statusTypeDto)
        {
            var statusType = new StatusType
            {
                Description = statusTypeDto.Description,
                IconUrl = statusTypeDto.IconUrl 
            };

            try
            {
                _context.StatusTypes.Add(statusType);
                await _context.SaveChangesAsync();

                var createdStatusTypeDto = new StatusTypeDto
                {
                    StatusTypeId = statusType.StatusTypeId,
                    Description = statusType.Description,
                    IconUrl = statusType.IconUrl ?? "https://tarefas.websupply.com.br/painel/assets/StatusGeolocalizacao-DxUl3vfK.png",

                };

                return CreatedAtAction(nameof(GetStatusType), new { id = createdStatusTypeDto.StatusTypeId }, createdStatusTypeDto);
            }
            catch ( Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusType(int id, StatusTypeCreateDto statusTypeDto)
        {
            var statusType = await _context.StatusTypes.FindAsync(id);

            if (statusType == null)
            {
                return NotFound();
            }

            try
            {
                statusType.Description = statusTypeDto.Description;
                statusType.IconUrl = statusTypeDto.IconUrl ?? "https://tarefas.websupply.com.br/painel/assets/StatusGeolocalizacao-DxUl3vfK.png";

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {

            var statusType = await _context.StatusTypes.FindAsync(id);

            if (statusType == null)
            {
                return NotFound();
            }

            _context.StatusTypes.Remove(statusType);

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
