using Microsoft.AspNetCore.Identity;
namespace Backend.Models
{
    public class User : IdentityUser
    {
        public bool IsVerified { get; set; }
    }
}