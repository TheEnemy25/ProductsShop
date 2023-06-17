using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.Context;
using ProductsShop.Models;
using ProductsShop.Services.Interfaces;

namespace ProductsShop.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _context;

        public OrdersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.ShoppingCart).ThenInclude(n => n.ShoppingCartItems).ThenInclude(n => n.Product).Include(n => n.User).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCart> carts, string userId, string userEmailAddress)
        {

        }
    }
}
