using Microsoft.EntityFrameworkCore;

namespace OrderMService.Models;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<Address> Addresses { get; set; }
}