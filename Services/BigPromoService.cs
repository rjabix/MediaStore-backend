using MediaStore_backend.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Xml.Linq;

namespace MediaStore_backend.Services
{
    public class BigPromoService
    {
        static IQueryable<BigPromoItem> Promos { get; } //implement database 
        private readonly StoreDbContext _context;

        static int nextId = 2;
        public BigPromoService(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<BigPromoItem> AddBigPromoItemAsync(BigPromoItem item)
        {
            _context.BigPromoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<List<BigPromoItem>> GetAllBigPromoItemsAsync()
        {
            return await _context.BigPromoItems.ToListAsync();
        }

        public async Task<BigPromoItem> GetBigPromoItemByIdAsync(int id)
        {
            return await _context.BigPromoItems.FirstOrDefaultAsync(i => i.id == id);
        }

        public async Task UpdateBigPromoItemAsync(BigPromoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBigPromoItemAsync(int id)
        {
            var item = await _context.BigPromoItems.FindAsync(id);
            if (item != null)
            {
                _context.BigPromoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

    }
}

