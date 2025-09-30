using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StatusWS.Data;
using StatusWS.Dtos;
using StatusWS.Models;
using StatusWS.Utils;

namespace StatusWS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IJiraService _jiraService;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public EmployeeService(AppDbContext context, IJiraService jiraService, IPasswordHasher<Employee> passwordHasher)
        {
            _context = context;
            _jiraService = jiraService;
            _passwordHasher = passwordHasher;
        }

        private async Task<EmployeeDto> MapToDto(Employee employee)
        {
            var enrichedStatusDto = await GetEnrichedStatusDto(employee);

            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Position = employee.Position,
                Photo = employee.Photo,
                CreatedAt = employee.CreatedAt,
                IsActive = employee.IsActive,
                Status = enrichedStatusDto
            };
        }

        private async Task<StatusDto> GetEnrichedStatusDto(Employee employee)
        {
            var statusDto = new StatusDto
            {
                StatusId = employee.Status.StatusId,
                CustomText = employee.Status.CustomText,
                UpdateAt = employee.Status.UpdateAt,
                DisplayText = null,
                StatusType = new StatusTypeDto
                {
                    StatusTypeId = employee.Status.StatusType.StatusTypeId,
                    Description = employee.Status.StatusType.Description,
                    IconUrl = employee.Status.StatusType.IconUrl,
                }
            };

            var content = statusDto.CustomText;

            if (_jiraService.IsJiraKeyFormat(content))
            {
                var jiraDetails = await _jiraService.GetIssueDetailsAsync(content);

                if (!string.IsNullOrEmpty(jiraDetails.Summary))
                {
                    statusDto.DisplayText = $"{jiraDetails.Key} - {jiraDetails.Summary}";
                    statusDto.CustomText = null;
                }
                else
                {
                    statusDto.DisplayText = $"Jira Erro: {content} ({jiraDetails.ErrorMessage})";
                    statusDto.CustomText = null;
                }
            }

            return statusDto;
        }

        // GET all Active
        public async Task<IEnumerable<EmployeeDto>> GetAllActiveEmployeesAsync()
        {
            var employees = await _context.Employees
                .Where(e => e.IsActive)
                .Include(e => e.Status)
                .ThenInclude(s => s.StatusType)
                .ToListAsync();

            var tasks = employees.Select(employee => MapToDto(employee));

            var employeeDtos = await Task.WhenAll(tasks);

            return employeeDtos;
        }

        // GET ID
        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
               .Where(e => e.IsActive)
               .Include(e => e.Status)
               .ThenInclude(s => s.StatusType)
               .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null) return null;

            return await MapToDto(employee);
        }

        // POST (Create)
        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateDto employeeCreateDto)
        {
            var defaultStatus = new Status
            {
                CustomText = "...",
                StatusTypeId = 1,
                UpdateAt = DateTimeUtils.GetBrazilTime(),
            };

            var employee = new Employee
            {
                Name = employeeCreateDto.Name,
                PasswordHash = _passwordHasher.HashPassword(null, employeeCreateDto.Password),
                Position = employeeCreateDto.Position,
                Photo = employeeCreateDto.Photo ?? "https://tarefas.websupply.com.br/painel/assets/userDefault-uMDAqLiz.png",
                CreatedAt = DateTimeUtils.GetBrazilTime(),
                Status = defaultStatus
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            await _context.Entry(employee.Status).Reference(s => s.StatusType).LoadAsync();

            return await MapToDto(employee);
        }

        // PUT (Update)
        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var employee = await _context.Employees
                .Include(e => e.Status)
                .ThenInclude(s => s.StatusType)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null) return null;

            bool statusOrTextUpdated = false;

            if (employeeUpdateDto.Name != null) employee.Name = employeeUpdateDto.Name;
            if (!string.IsNullOrEmpty(employeeUpdateDto.Password))
            {
                employee.PasswordHash = _passwordHasher.HashPassword(employee, employeeUpdateDto.Password);
            }
            if (employeeUpdateDto.Position != null) employee.Position = employeeUpdateDto.Position;
            if (employeeUpdateDto.Photo != null) employee.Photo = employeeUpdateDto.Photo;
            if (employeeUpdateDto.IsActive.HasValue) employee.IsActive = employeeUpdateDto.IsActive.Value;

            if (employeeUpdateDto.StatusTypeId.HasValue && employee.Status.StatusTypeId != employeeUpdateDto.StatusTypeId.Value)
            {
                employee.Status.StatusTypeId = employeeUpdateDto.StatusTypeId.Value;
                statusOrTextUpdated = true;
            }

            if (employeeUpdateDto.CustomText != employee.Status.CustomText)
            {
                employee.Status.CustomText = employeeUpdateDto.CustomText;
                statusOrTextUpdated = true;
            }

            if (employeeUpdateDto.StatusTypeId == 1 && employee.Status.CustomText != null)
            {
                employee.Status.CustomText = null;
                statusOrTextUpdated = true;
            }

            if (statusOrTextUpdated)
            {
                employee.Status.UpdateAt = DateTimeUtils.GetBrazilTime();
            }

            await _context.SaveChangesAsync();

            if (statusOrTextUpdated)
            {
                employee.Status.StatusType = await _context.StatusTypes
                    .FirstOrDefaultAsync(st => st.StatusTypeId == employee.Status.StatusTypeId);
            }

            return await MapToDto(employee);
        }

        // GET Inactive
        public async Task<IEnumerable<EmployeeDto>> GetInactiveEmployeesAsync()
        {
            var employees = await _context.Employees
                .Where(e => !e.IsActive)
                .Include(e => e.Status)
                .ThenInclude(s => s.StatusType)
                .ToListAsync();

            var employeeDtos = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                employeeDtos.Add(await MapToDto(employee));
            }

            return employeeDtos;
        }

        public async Task<(Employee? Employee, PasswordVerificationResult VerificationResult)> LoginAsync(LoginDto loginDto)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == loginDto.EmployeeId && e.IsActive);

            if (employee == null)
            {
                return (null, PasswordVerificationResult.Failed);
            }

            var result = _passwordHasher.VerifyHashedPassword(employee, employee.PasswordHash, loginDto.Password);

            return (employee, result);
        }

    }
}