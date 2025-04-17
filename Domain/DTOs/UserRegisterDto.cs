using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
