using MagicVilla.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Data;

public class ApplicationDbContext : IdentityDbContext<LocalUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}