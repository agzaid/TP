using Application.Services.Authentication;
using Application.Services.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService,IAuthenticationService authenticationService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _authenticationService = authenticationService;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet("GenerateToken")]
        public async Task<IActionResult> GenerateToken()
        {
            try
            {
                var token = await _authenticationService.TokenGenerator("admin");
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
        [Authorize(Roles = "admin,employee")]
        [HttpGet("EmployeeList")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
        [HttpGet("GetEmployeeById")]
        [Authorize(Roles = "admin,employee")]

        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employees = await _employeeService.GetEmployee(id);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> Create(EmployeeDto employeeVM)
        {
            try
            {
                var employees = await _employeeService.AddEmployee(employeeVM);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPut("UpdateEmployee")]

        public async Task<IActionResult> Update(EmployeeDto employeeVM)
        {
            try
            {
                var employees = await _employeeService.UpdateEmployee(employeeVM);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employees = await _employeeService.DeleteEmployee(id);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
