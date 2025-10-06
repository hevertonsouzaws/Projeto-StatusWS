using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusWS.Data;
using StatusWS.Dtos;
using StatusWS.Models;
using StatusWS.Services;

namespace StatusWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTypeController : ControllerBase
    {
        private readonly IStatusTypeService _statusTypeService;

        public StatusTypeController(IStatusTypeService statusTypeService)
        {
            _statusTypeService = statusTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusTypeDto>>> GetStatusTypes()
        {
            var statusTypes = await _statusTypeService.GetAllStatusTypesAsync();
            return Ok(statusTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusTypeDto>> GetStatusType(int id)
        {
            var statusTypeDto = await _statusTypeService.GetStatusTypeByIdAsync(id);
            return Ok(statusTypeDto);
        }

        [HttpPost]
        public async Task<ActionResult<StatusTypeDto>> PostStatusType(StatusTypeCreateDto statusTypeDto)
        {
            var createdStatusTypeDto = await _statusTypeService.CreateStatusTypeAsync(statusTypeDto);

            return CreatedAtAction(nameof(GetStatusType), new { id = createdStatusTypeDto.StatusTypeId }, createdStatusTypeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusType(int id, StatusTypeCreateDto statusTypeDto)
        {
            await _statusTypeService.UpdateStatusTypeAsync(id, statusTypeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            await _statusTypeService.DeleteStatusTypeAsync(id);
            return NoContent();
        }
    }
}
