using Microsoft.AspNetCore.Identity;
using StatusWS.Dtos;
using StatusWS.Models;

namespace StatusWS.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllActiveEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeCreateDto employeeCreateDto);
        Task<EmployeeDto> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeUpdateDto);
        Task<IEnumerable<EmployeeDto>> GetInactiveEmployeesAsync();
        Task<Employee> LoginAsync(LoginDto loginDto);
    }
}