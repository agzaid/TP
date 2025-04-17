using Application.Common.Interfaces;
using Application.Services.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IUnitOfWork unitOfWork, ILogger<RoleService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Result<string> AddRole(UserRegisterDto userRegisterDto)
        {
            try
            {
                var newRole = new Role()
                {
                    RoleName = userRegisterDto.Role.ToLower(),
                    Create_Date = DateTime.Now,
                    Modified_Date = DateTime.Now,
                    IsDeleted = false
                };
                var lookForRole = _unitOfWork.Roles.Get(s => s.RoleName == newRole.RoleName);
                if (lookForRole == null)
                {
                    _unitOfWork.Roles.Add(newRole);
                    _unitOfWork.Save();
                    return Result<string>.Success("success", "Role Created");
                }


               return Result<string>.Failure("error");
            }
            catch (Exception ex)
            {
                 return Result<string>.Failure("error");
                throw;
            }
        }
    }
}
