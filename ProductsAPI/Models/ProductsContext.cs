using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models;

public class ProductsContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    
    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Product>().HasData(new Product
            { ProductId = 1, ProductName = "Product 1", Price = 100, IsActive = true });
        modelBuilder.Entity<Product>().HasData(new Product
            { ProductId = 2, ProductName = "Product 2", Price = 200, IsActive = true });
        modelBuilder.Entity<Product>().HasData(new Product
            { ProductId = 3, ProductName = "Product 3", Price = 300, IsActive = true });
        modelBuilder.Entity<Product>().HasData(new Product
            { ProductId = 4, ProductName = "Product 4", Price = 400, IsActive = true });
        modelBuilder.Entity<Product>().HasData(new Product
            { ProductId = 5, ProductName = "Product 5", Price = 500, IsActive = true });
        
    }
}