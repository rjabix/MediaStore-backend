using MediaStore_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace MediaStore_backend.Services
{
    public class NewsService
    {
        static IQueryable<NewsCarouselItem> Promos { get; } //implement database 
        private readonly StoreDbContext _context;

        static int nextId = 2;
        public NewsService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<NewsCarouselItem> AddNewsCarouseleItemAsync(NewsCarouselItem item)
        {
            _context.NewsCarouselItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<List<NewsCarouselItem>> GetAllNewsCarouseleItemsAsync()
        {
            return await _context.NewsCarouselItems.ToListAsync();
        }

        public async Task<NewsCarouselItem> GetNewsCarouseleItemByIdAsync(int id)
        {
            return await _context.NewsCarouselItems.FirstOrDefaultAsync(i => i.id == id);
        }

        public async Task UpdateNewsCarouseleItemAsync(NewsCarouselItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNewsCarouseleItemAsync(int id)
        {
            var item = await _context.NewsCarouselItems.FindAsync(id);
            if (item != null)
            {
                _context.NewsCarouselItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
