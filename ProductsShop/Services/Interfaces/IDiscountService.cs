using ProductsShop.Models;

namespace ProductsShop.Services.Interfaces
{
    public interface IDiscountService
    {
        public Task<List<Discount>> GetActiveDiscountsAsync();
        public Task<Discount> AddAsync(Discount discount);
        public Task DeleteAsync(int id);
    }
}
