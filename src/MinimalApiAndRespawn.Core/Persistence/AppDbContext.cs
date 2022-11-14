using Microsoft.EntityFrameworkCore;
using MinimalApiAndRespawn.Core.Persistence.Entities;

namespace MinimalApiAndRespawn.Core.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
}
