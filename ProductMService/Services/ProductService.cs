using ProductMService.Models;
using ProductMService.Models.Categories;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Globalization;
using System.Xml.Linq;

namespace ProductMService.Services
{
    public class ProductService
    {
        private readonly StoreDbContext _context;

        public ProductService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<object>?> GetProductByCategory(string category)
        {
            var property = _context.GetType().GetProperty(category) ?? throw new ArgumentException("Entity name is not valid", nameof(category));

            if (property.GetValue(_context) is not IQueryable<object> dbSet)
            {
                throw new InvalidOperationException("DbSet is not found");
            }

            return await dbSet.ToListAsync();
        }

        public async Task<object> GetProductByCategoryAndIdAsync(string category, int id)
        {
            var property = _context.GetType().GetProperty(category) ?? throw new ArgumentException("Entity name is not valid", nameof(category));

            if (property.GetValue(_context) is not IQueryable<Product> dbSet)
            {
                throw new InvalidOperationException("DbSet is not found");
            }

            var product = await dbSet.FirstOrDefaultAsync(i => i.id == id);

            if (product == null)
            {
                return null;
            }

            product.description = null;
            return product;
        }

        public async Task<string> GetProduct_Description_ByCategoryAndIdAsync(string category, int id)
        {
            var property = _context.GetType().GetProperty(category) ?? throw new ArgumentException("Entity name is not valid", nameof(category));

            if (property.GetValue(_context) is not IQueryable<Product> dbSet)
            {
                throw new InvalidOperationException("DbSet is not found");
            }

            string? description = await dbSet.Where(i => i.id == id).Select(i => i.description).FirstOrDefaultAsync();
            return description.ToString();
        }

        public async Task<List<object>> GetPopularProductsAsync()
        {
            List<object> popularProducts = new List<object>();
            foreach (var category in Enum.GetNames(typeof(ProductCategory)))
            {
                try
                {
                    var property = _context.GetType().GetProperty(category) ?? throw new ArgumentException("Entity name is not valid", nameof(category));

                    if (property.GetValue(_context) is not IQueryable<Product> dbSet)
                    {
                        continue;
                    }
                    dbSet = dbSet.Where(p => p.specialTags != null && p.specialTags.Contains("Popular"));
                    popularProducts.AddRange(await dbSet.ToListAsync());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

            }

            return popularProducts;
        }

        //public async Task<BigPromoItem> AddProductAsync(BigPromoItem item)
        //{
        //    _context.BigPromoItems.Add(item);
        //    await _context.SaveChangesAsync();
        //    return item;
        //}
        //public async Task<List<Product>> GetAllProductsAsync()
        //{
        //    return await _context.BigPromoItems.ToListAsync();
        //}

        //public async Task<BigPromoItem> GetProductByIdAsync(int id)
        //{
        //    return await _context.BigPromoItems.FirstOrDefaultAsync(i => i.id == id);
        //}

        //public async Task UpdateProductAsync(BigPromoItem item)
        //{
        //    _context.Entry(item).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteProductAsync(int id)
        //{
        //    var item = await _context.BigPromoItems.FindAsync(id);
        //    if (item != null)
        //    {
        //        _context.BigPromoItems.Remove(item);
        //        await _context.SaveChangesAsync();
        //    }
        //}

    }
}

