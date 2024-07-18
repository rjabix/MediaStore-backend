using MediaStore_backend.Models;
using MediaStore_backend.Models.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MediaStore_backend.Models
{
    public class StoreDbContext : DbContext
    {

        public DbSet<BigPromoItem> BigPromoItems { get; set; }
        public DbSet<NewsCarouselItem> NewsCarouselItems { get; set; }

        public DbSet<Laptop> Laptops { get; set; }

        public DbSet<Smartphone> Smartphones { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mediastore_database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BigPromoItem>().ToTable("BigPromoItems");
            modelBuilder.Entity<NewsCarouselItem>().ToTable("NewsCarouselItems");
            modelBuilder.Entity<Smartphone>().ToTable("Smartphones");
            modelBuilder.Entity<Laptop>().ToTable("Laptops");
        }
    }
}
