using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var allowedRoles = new List<string> { "Farmer", "Buyer" };
        if(!allowedRoles.Contains(model.Role))
        {
            return BadRequest("Invalid role");
        }
        //Check if the user already exists
        if(await _userManager.FindByEmailAsync(model.Email) != null)
        {
            return BadRequest("This Email is already exists");
        }
        if(await _userManager.FindByNameAsync(model.UserName) != null)
        {
            return BadRequest("This User Name is already exists");
        }
        if(await _userManager.Users.AnyAsync(u => u.NIC == model.NIC))
        {
            return BadRequest("This NIC is already exists");
        }

        var User = new User
        {
            Email = model.Email,
            UserName = model.UserName,
            NIC = model.NIC,
            PhoneNumber = model.PhoneNumber.ToString(),
            IsVerified = false
        };
        var result = await _userManager.CreateAsync(User, model.Password);
        if(!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        if(!await _roleManager.RoleExistsAsync(model.Role))
        {
            await _roleManager.CreateAsync(new IdentityRole(model.Role));
        }
        await _userManager.AddToRoleAsync(User, model.Role);
        return Ok(new{Message=$"User {model.UserName} registered successfully as {model.Role}"});
    }
}
//Data transfer objects for user registration and login
public class RegisterDto
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string NIC { get; set; }
    public int PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}

public class LoginDto
{
    public string Identifier { get; set; }
    public string Password { get; set; }
}