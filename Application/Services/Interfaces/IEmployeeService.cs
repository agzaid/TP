using Domain.DTOs;

namespace Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Result<IEnumerable<EmployeeDto>>> GetAllEmployees();
        Task<Result<EmployeeDto>> GetEmployee(int id);
        Task<Result<string>> AddEmployee(EmployeeDto employee);
        Task<Result<string>> UpdateEmployee(EmployeeDto employee);
        Task<Result<string>> DeleteEmployee(int id);
    }
}
