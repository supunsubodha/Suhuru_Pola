using Microsoft.AspNetCore.Identity;
namespace Backend.Models
{
    public class User : IdentityUser
    {
        public string NIC { get; set; }=string.Empty;
        public bool IsVerified { get; set; }
    }
}