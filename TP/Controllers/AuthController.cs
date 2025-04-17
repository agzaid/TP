using Application.Services.Authentication;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authentication;

        public AuthController(IAuthenticationService authentication)
        {
            _authentication = authentication;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            var result = await _authentication.RegisterUserAsync(userRegister);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorCode);
            }

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var result = await _authentication.ValidateUserAsync(loginDto);
                if (!result.Item1)
                {
                    return Unauthorized(new { Message = "Invalid credentials" });
                }else
                    return Ok(new { Token = await _authentication.TokenGenerator(result.Item2) }); // Return JWT token

            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials");
            }
        }

    }
}

