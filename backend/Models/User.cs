using Microsoft.AspNetCore.Identity;
namespace Backend.Models
public class User : IdentityUser
{
    public class bool Verified { get; set; }
}