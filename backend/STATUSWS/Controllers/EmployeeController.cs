using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StatusWS.Dtos;
using StatusWS.Models;
using StatusWS.Services;

namespace StatusWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IJiraService _jiraService;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public EmployeeController(IEmployeeService employeeService, IJiraService jiraService, IPasswordHasher<Employee> passwordHasher)
        {
            _employeeService = employeeService;
            _jiraService = jiraService;
            _passwordHasher = passwordHasher;
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
            var jiraDetails = await _jiraService.GetIssueDetailsAsync(jiraKey);
            return Ok(jiraDetails);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var employee = await _employeeService.LoginAsync(loginDto);

            return Ok(new
            {
                Message = "Logado com sucesso!",
                employee.EmployeeId,
                employee.Name
            });
        }


    }
}