using NewsMService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewsMService.Models
{
    public class StoreDbContext : DbContext
    {

        public DbSet<NewsCarouselItem> NewsCarouselItems { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=news_db.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsCarouselItem>().ToTable("NewsCarouselItems");
        }
    }
}
