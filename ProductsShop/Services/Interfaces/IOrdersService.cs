using ProductsShop.Models;

namespace ProductsShop.Services.Interfaces
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCart> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
