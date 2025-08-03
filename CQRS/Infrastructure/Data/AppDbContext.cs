using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure.Data;

public sealed class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Product>().HasData(new List<Product>()
        {
            new Product("iPhone 15 Pro",
                "Apple's latest flagship smartphone with a ProMotion display and improved cameras", 999.99m),
            new Product("Dell XPS 15", "Dell's high-performance laptop with a 4K InfinityEdge display", 1899.99m),
            new Product("Sony WH-1000XM4", "Sony's top-of-the-line wireless noise-canceling headphones", 349.99m)
        });
    }
}