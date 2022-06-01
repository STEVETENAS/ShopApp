using Microsoft.AspNetCore.Identity;

namespace ShopApp.API.Models
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
