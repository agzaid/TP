using Domain.DTOs;

namespace Application.Services.Interfaces
{
    public interface IRoleService
    {
        Result<string> AddRole(UserRegisterDto userRegisterDto);
    }
}
