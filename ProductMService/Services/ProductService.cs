using ProductMService.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ProductMService.Services
{
    public class ProductService
    {
        private readonly StoreDbContext _context;

        public ProductService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>?> GetProductsByCategory(string category, int page = 0)
        {
            var dbSet = GetDbSet(category);
            var products = await dbSet.Skip(page * 20).ToListAsync();
            foreach (var product in products)
            {
                product.description = null;
            }
            return products;
        }

        public async Task<object?> GetProductByCategoryAndIdAsync(string category, int id)
        {
            var dbSet = GetDbSet(category);
            var product = await dbSet.FirstOrDefaultAsync(i => i.id == id);
            if (product != null) product.description = null;
            return product;
        }

        public async Task<string?> GetProductDescriptionByCategoryAndIdAsync(string category, int id)
        {
            var dbSet = GetDbSet(category);
            return await dbSet.Where(i => i.id == id).Select(i => i.description).FirstOrDefaultAsync();
        }

        public async Task<List<object>?> GetPopularProductsAsync()
        {
            var popularProducts = new List<object>();
            foreach (var category in Enum.GetNames(typeof(ProductCategory)))
            {
                try
                {
                    var dbSet = GetDbSet(category).Where(p => p.specialTags != null && p.specialTags.Contains("Popular"));
                    popularProducts.AddRange(await dbSet.ToListAsync());
                }
                catch { }
            }
            return popularProducts;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var dbSet = GetDbSet(product.category.ToCategoryString());
            var item = await dbSet.FirstOrDefaultAsync(i => i.id == product.id);
            if (item == null) throw new InvalidOperationException("Product is not found");
            _context.Entry(item).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
        }

        private IQueryable<Product> GetDbSet(string category)
        {
            var property = _context.GetType().GetProperty(category);
            if (property?.GetValue(_context) is not IQueryable<Product> dbSet)
                throw new InvalidOperationException("DbSet is not found");
            return dbSet;
        }
    }
}