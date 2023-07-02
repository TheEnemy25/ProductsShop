using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.Context;
using ProductsShop.Models;
using ProductsShop.Services.Interfaces;

namespace ProductsShop.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _context;

        public DiscountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Discount> GetByIdAsync(int id) => await _context.Discounts.FindAsync(id);

        public async Task<List<Discount>> GetActiveDiscountsAsync()
        {
            var discounts = await _context.Discounts
                .Where(d => d.EndDate >= DateTime.Today)
                .ToListAsync();
            return discounts;
        }
        //asd
        public async Task<Discount> AddAsync(Discount discount)
        {
            await _context.AddAsync(discount);
            await _context.SaveChangesAsync();

            return discount;
        }

        public async Task<Discount> UpdateAsync(Discount discount)
        {
            _context.Update(discount);
            await _context.SaveChangesAsync();

            return discount;
        }

        public async Task DeleteAsync(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);

            if (discount is not null)
            {
                _context.Remove(discount);
                await _context.SaveChangesAsync();
            }
        }
    }
}
