using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthControllers : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public AuthControllers(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }
}
//Data transfer objects for user registration and login
public class RegisterDto
{
    private string Email { get; set; }
    private string UserName { get; set; }
    private string NIC { get; set; }
    private int PhoneNumber { get; set; }
    private string Password { get; set; }
}

public class LoginDto
{
    private string Identifier { get; set; }
    private string Password { get; set; }
}