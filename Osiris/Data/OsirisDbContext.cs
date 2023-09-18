using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Osiris.Models;

namespace Osiris.Data;

public class OsirisDbContext : IdentityDbContext
{
    public OsirisDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<MyAppUser> MyAppUsers { get; set; }

}