using ProductsShop.Models;

namespace ProductsShop.Services.Interfaces
{
    public interface IDiscountService
    {
        public Task<Discount> GetByIdAsync(int id);
        public Task<List<Discount>> GetActiveDiscountsAsync();
        public Task<Discount> AddAsync(Discount discount);
        public Task<Discount> UpdateAsync(Discount discount);
        public Task DeleteAsync(int id);
    }
}
