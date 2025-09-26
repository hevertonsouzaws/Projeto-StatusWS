﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusWS.Data;
using StatusWS.Dtos;
using StatusWS.Models;
using StatusWS.Utils;

namespace StatusWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Where(e => e.IsActive)
                .Include(e => e.Status)
                .ThenInclude(s => s.StatusType)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Position = e.Position,
                    Photo = e.Photo,
                    CreatedAt = e.CreatedAt,
                    IsActive = e.IsActive,
                    Status = new StatusDto
                    {
                        StatusId = e.Status.StatusId,
                        CustomText = e.Status.CustomText,
                        UpdateAt = e.Status.UpdateAt,
                        StatusType = new StatusTypeDto
                        {
                            StatusTypeId = e.Status.StatusType.StatusTypeId,
                            Description = e.Status.StatusType.Description,
                            IconUrl = e.Status.StatusType.IconUrl,
                        }
                    }
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Where(e => e.IsActive)
                .Include(e => e.Status)
                .ThenInclude(s => s.StatusType)
                 .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Position = employee.Position,
                Photo = employee.Photo,
                CreatedAt = employee.CreatedAt,
                IsActive = employee.IsActive,
                Status = new StatusDto
                {
                    StatusId = employee.Status.StatusId,
                    CustomText = employee.Status.CustomText,
                    UpdateAt = employee.Status.UpdateAt,
                    StatusType = new StatusTypeDto
                    {
                        StatusTypeId = employee.Status.StatusType.StatusTypeId,
                        Description = employee.Status.StatusType.Description,
                        IconUrl = employee.Status.StatusType.IconUrl,
                    }
                }
            };
            return (employeeDto);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeCreateDto employeeCreateDto)
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
                Position = employeeCreateDto.Position,
                Photo = employeeCreateDto.Photo ?? "https://tarefas.websupply.com.br/painel/assets/userDefault-uMDAqLiz.png",
                CreatedAt = DateTimeUtils.GetBrazilTime(),
                Status = defaultStatus
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            await _context.Entry(employee).Reference(e => e.Status).LoadAsync();
            await _context.Entry(employee.Status).Reference(s => s.StatusType).LoadAsync();

            var employeeDto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Position = employee.Position,
                Photo = employee.Photo,
                CreatedAt = employee.CreatedAt,
                IsActive = employee.IsActive,
                Status = new StatusDto
                {
                    StatusId = employee.Status.StatusId,
                    CustomText = employee.Status.CustomText,
                    UpdateAt = employee.Status.UpdateAt,
                    StatusType = new StatusTypeDto
                    {
                        StatusTypeId = employee.Status.StatusType.StatusTypeId,
                        Description = employee.Status.StatusType.Description,
                        IconUrl = employee.Status.StatusType.IconUrl
                    }
                }
            };

            return CreatedAtAction(nameof(GetEmployees), new { id = employeeDto.EmployeeId }, employeeDto);
        }

        [HttpGet("Inactive")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetInactiveEmployees()
        {
            var employees = await _context.Employees
                .Where(e => !e.IsActive)
                .Include(e => e.Status)
                .ThenInclude(s => s.StatusType)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Position = e.Position,
                    Photo = e.Photo,
                    CreatedAt = e.CreatedAt,
                    IsActive = e.IsActive,
                    Status = new StatusDto
                    {
                        StatusId = e.Status.StatusId,
                        CustomText = e.Status.CustomText,
                        UpdateAt = e.Status.UpdateAt,
                        StatusType = new StatusTypeDto
                        {
                            StatusTypeId = e.Status.StatusType.StatusTypeId,
                            Description = e.Status.StatusType.Description,
                            IconUrl = e.Status.StatusType.IconUrl,
                        }
                    }
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var employee = await _context.Employees
                .Include(e => e.Status)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }


            if (employeeUpdateDto.Name != null)
            {
                employee.Name = employeeUpdateDto.Name;
            }


            if (employeeUpdateDto.Position != null)
            {
                employee.Position = employeeUpdateDto.Position;
            }

            if (employeeUpdateDto.Photo != null)
            {
                employee.Photo = employeeUpdateDto.Photo;
            }

            if (employeeUpdateDto.StatusTypeId != null)
            {
                employee.Status.StatusTypeId = employeeUpdateDto.StatusTypeId.Value;
                employee.Status.UpdateAt = DateTime.UtcNow;


                if (employeeUpdateDto.StatusTypeId == 1)
                {
                    employee.Status.CustomText = null;
                }
            }

            if (employeeUpdateDto.CustomText != null)
            {
                employee.Status.CustomText = employeeUpdateDto.CustomText;
                employee.Status.UpdateAt = DateTimeUtils.GetBrazilTime();
            }

            if (employeeUpdateDto.IsActive != null)
            {
                employee.IsActive = employeeUpdateDto.IsActive.Value;
            }


            await _context.SaveChangesAsync();

            return NoContent();
        }


    }

}


