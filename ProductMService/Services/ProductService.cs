using ProductMService.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductMService.Services
{
    public class ProductService(StoreDbContext context)
    {
        public async Task<List<Product>?> GetProductsByCategory(string category, int page = 0)
        {
            var dbSet = GetDbSetQueryable(category);
            var products = await dbSet.Skip(page * 20).Take(20).ToListAsync();
            foreach (var product in products)
            {
                product.description = null;
            }
            return products;
        }

        public async Task<object?> GetProductByCategoryAndIdAsync(string category, int id)
        {
            var dbSet = GetDbSetQueryable(category);
            var product = await dbSet.FirstOrDefaultAsync(i => i.id == id);
            if (product != null) product.description = null;
            return product;
        }

        public async Task<string?> GetProductDescriptionByCategoryAndIdAsync(string category, int id)
        {
            var dbSet = GetDbSetQueryable(category);
            return await dbSet.Where(i => i.id == id).Select(i => i.description).FirstOrDefaultAsync();
        }

        public async Task<List<object>?> GetPopularProductsAsync(int page)
        {
            var popularProducts = new List<object>();
            foreach (var category in Enum.GetNames(typeof(ProductCategory)))
            {
                try
                {
                    var dbSet = GetDbSetQueryable(category)
                        .Where(p => p.specialTags != null && p.specialTags.Contains("Popular"))
                        .Skip(page * 20)
                        .Take(20);
                    popularProducts.AddRange(await dbSet.ToListAsync());
                }
                catch { }
            }
            return popularProducts;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var dbSet = GetDbSetQueryable(product.category.ToCategoryString());
            var item = await dbSet.FirstOrDefaultAsync(i => i.id == product.id);
            if (item == null) throw new InvalidOperationException("Product is not found");
            context.Entry(item).CurrentValues.SetValues(product);
            await context.SaveChangesAsync();
        }
        
        public async Task DeleteProductAsync(Product product)
        {
            context.Remove(product);
            await context.SaveChangesAsync();
        }
        
        public async Task AddProductAsync(Product product)
        {
            context.Add(product);
            await context.SaveChangesAsync();
        }
        

        private IQueryable<Product> GetDbSetQueryable(string category)
        {
            var property = context.GetType().GetProperty(category);
            if (property?.GetValue(context) is not IQueryable<Product> dbSet)
                throw new InvalidOperationException("DbSet is not found");
            return dbSet;
        }
    }
}