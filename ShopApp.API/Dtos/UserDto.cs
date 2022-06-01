using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.API.Dtos
{
    public class UserDto
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(26,MinimumLength = 8)]
        public string Password { get; set; }
        [Required, StringLength(25, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required, StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }
    }
    public class UserLoginDto
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(26,MinimumLength = 8)]
        public string Password { get; set; }
    }
}
