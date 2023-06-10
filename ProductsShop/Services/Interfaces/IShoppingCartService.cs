using ProductsShop.Models;

namespace ProductsShop.Services.Interfaces
{
    public interface IShoppingCartService
    {
        public Task AddToCartAsync(int productId, string userId, int quantity);
        public Task RecalculateTotalPrice(int cartId);
        public Task<ShoppingCart?> GetActiveCartForUserAsync(string userId);
        public Task<ShoppingCart> GenerateNewActiveCartAsync(string userId);
    }
}
