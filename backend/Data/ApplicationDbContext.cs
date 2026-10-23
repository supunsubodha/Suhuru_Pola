using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Backend.Models;
namespace Backend.Data

public class  ApplicationDbContext:IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}
}