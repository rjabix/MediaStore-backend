using BigPromoMService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace BigPromoMService.Models
{
    public class StoreDbContext : DbContext
    {
        public DbSet<BigPromoItem> BigPromoItems { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bigpromos_db.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BigPromoItem>().ToTable("BigPromoItems");
        }
    }
}