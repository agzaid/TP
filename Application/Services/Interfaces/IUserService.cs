using Domain.DTOs;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<string>> AddUser(UserRegisterDto userRegisterDto);
        //Task<Result<string>> UpdateEmployee(EmployeeVM employee);
        //Task<Result<string>> DeleteEmployee(int id);
    }
}
