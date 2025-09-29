using Microsoft.AspNetCore.Mvc;
using StatusWS.Dtos;
using StatusWS.Services;

namespace StatusWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IJiraService _jiraService;

        public EmployeeController(IEmployeeService employeeService, IJiraService jiraService)
        {
            _employeeService = employeeService;
            _jiraService = jiraService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllActiveEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employeeDto = await _employeeService.GetEmployeeByIdAsync(id);
            if (employeeDto == null)
            {
                return NotFound();
            }
            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var employeeDto = await _employeeService.CreateEmployeeAsync(employeeCreateDto);
            return CreatedAtAction(nameof(GetEmployees), new { id = employeeDto.EmployeeId }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var employeeDto = await _employeeService.UpdateEmployeeAsync(id, employeeUpdateDto);

            if (employeeDto == null)
            {
                return NotFound();
            }
            // Retorna o EmployeeDto completo e atualizado (com o status enriquecido)
            return Ok(employeeDto);
        }

        [HttpGet("Inactive")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetInactiveEmployees()
        {
            var employees = await _employeeService.GetInactiveEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("jira-details/{jiraKey}")]
        public async Task<ActionResult<JiraIssueDto>> GetJiraDetails(string jiraKey)
        {
            if (!_jiraService.IsJiraKeyFormat(jiraKey))
            {
                return BadRequest("Formato de chave do Jira inválido.");
            }

            var jiraDetails = await _jiraService.GetIssueDetailsAsync(jiraKey);

            if (!string.IsNullOrEmpty(jiraDetails.ErrorMessage))
            {
                return StatusCode(500, jiraDetails.ErrorMessage);
            }

            if (string.IsNullOrEmpty(jiraDetails.Summary))
            {
                return NotFound();
            }

            return Ok(jiraDetails);
        }
    }
}