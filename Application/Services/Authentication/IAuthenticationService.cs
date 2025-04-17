using Domain.DTOs;

namespace Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<Result<string>> RegisterUserAsync(UserRegisterDto user);
        Task<(bool, string)> ValidateUserAsync(UserLoginDto user);
        Task<string> TokenGenerator(string role);
    }
}
