using Application.Common.Authentication;
using Application.Common.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtToken;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IJwtTokenGenerator jwtToken, IPasswordHasher<User> passwordHasher, IUnitOfWork unitOfWork)
        {
            _jwtToken = jwtToken;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            try
            {
                var user = new User { Username = userRegisterDto.Username.ToLower() };
                var isExistUser = await _unitOfWork.Users.Get(s => s.Username == user.Username);

                if (isExistUser == null)
                {
                    // Hash the password
                    user.PasswordHash = _passwordHasher.HashPassword(user, userRegisterDto.Password);

                    var roleId = await _unitOfWork.Roles.Get(s => s.RoleName == userRegisterDto.Role.ToLower());
                    user.RoleId = roleId.Id;

                    _unitOfWork.Users.Add(user);
                    _unitOfWork.Save();

                    return Result<string>.Success("user", "User registered successfully");
                }
                else
                    return Result<string>.Failure("User is already registered");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(ex.Message);
            }
        }

        public Task<string> TokenGenerator(string role)
        {
            var token = _jwtToken.GenerateToken(role);
            return Task.FromResult(token);
        }

        public async Task<(bool, string)> ValidateUserAsync(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _unitOfWork.Users.Get(s => s.Username == userLoginDto.Username);
                var role = await _unitOfWork.Roles.Get(s => s.Id == user.RoleId);
                if (user is not null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password);
                    return (result == PasswordVerificationResult.Success, role.RoleName);
                }
                else
                    return (false, "");
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
    }
}
