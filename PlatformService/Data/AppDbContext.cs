using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

#nullable disable
    public DbSet<Platform> Platforms { get; set; }
#nullable enable

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var platforms = new List<Platform>
        {
            new()
            {
                Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"
            },
            new()
            {
                Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free"
            },
            new()
            {
                Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free"
            }
        };

        modelBuilder.Entity<Platform>().HasData(platforms);
        base.OnModelCreating(modelBuilder);
    }
}