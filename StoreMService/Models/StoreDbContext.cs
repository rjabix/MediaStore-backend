using Microsoft.EntityFrameworkCore;

namespace StoreMService.Models;

public class StoreDbContext : DbContext
{
    public DbSet<Delivery> Deliveries { get; set; }
    
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
    {
    }
}