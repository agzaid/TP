using Application.Common.Interfaces;
using Application.Services.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public Task<Result<string>> AddUser(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
