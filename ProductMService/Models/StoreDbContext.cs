using System.Reflection;
using ProductMService.Models;
using ProductMService.Models.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductMService.Models
{
    public class StoreDbContext : DbContext
    {
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
            modelBuilder.Entity<Smartphone>().ToTable("Smartphones");
            modelBuilder.Entity<Laptop>().ToTable("Laptops");
        }
    }
}