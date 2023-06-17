using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.Context;
using ProductsShop.Models;
using ProductsShop.Services.Interfaces;

namespace ProductsShop.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        public ApplicationDbContext _context { get; set; }
        public UserManager<ApplicationUser> _userManager { get; set; }

        public ShoppingCartService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddToCartAsync(int productId, string userId, int quantity)
        {
            var cart = await GenerateNewActiveCartAsync(userId);

            var product = await _context.Products.FindAsync(productId);

            if (product is not null)
            {
                cart.ShoppingCartItems ??= new List<ShoppingCartItem>();
                var cartItem = cart.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);
                if (cartItem is null)
                {
                    var newCartItem = new ShoppingCartItem
                    {
                        ProductId = product.Id,
                        ShoppingCartId = cart.Id,
                        Quantity = quantity
                    };

                    await _context.AddAsync(newCartItem);
                    await _context.SaveChangesAsync();

                    await RecalculateTotalPrice(cart.Id);
                    return;
                }

                cartItem.Quantity += quantity;
                await _context.SaveChangesAsync();
                await RecalculateTotalPrice(cart.Id);
            }
        }

        public async Task RemoveFromCart(int productId, string userId)
        {
            var cart = await GetActiveCartForUserAsync(userId);

            if (cart.ShoppingCartItems is not null)
            {
                cart.ShoppingCartItems.RemoveAll(x => x.ProductId == productId);

                _context.Update(cart.ShoppingCartItems);
                await _context.SaveChangesAsync();
            }
            await RecalculateTotalPrice(cart.Id);
        }

        public async Task RecalculateTotalPrice(int cartId)
        {
            var cart = await _context.ShoppingCarts.Where(x => x.Id == cartId && x.IsActive)
                .Include(c => c.ShoppingCartItems)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync();

            if (cart is not null && cart.ShoppingCartItems is not null)
            {
                foreach (var item in cart.ShoppingCartItems)
                {
                    item.TotalPrice = item.Quantity * item.Product.Price;
                    _context.Update(item);
                }
                await _context.SaveChangesAsync();

                var prices = cart.ShoppingCartItems.Select(x => new
                {
                    price = x.TotalPrice,
                    quantity = x.Quantity
                });

                cart.TotalPrice = (double)prices.Select(x => x.price).Sum();
                cart.NumberOfProducts = prices.Select(x => x.quantity).Sum();

                _context.Update(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ShoppingCart?> GetActiveCartForUserAsync(string userId)
        {
            var cart = await _context.ShoppingCarts
                .Where(x => x.UserId == userId && x.IsActive)
                .Include(x => x.ShoppingCartItems)
                    .ThenInclude(x => x.Product)
                .SingleOrDefaultAsync();

            return cart;
        }

        public async Task<ShoppingCart> GenerateNewActiveCartAsync(string userId)
        {
            var cart = await _context.ShoppingCarts
                .Where(x => x.IsActive && x.UserId == userId)
                .Include(x => x.ShoppingCartItems)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync();

            if (cart is null)
            {
                var newCart = new ShoppingCart
                {
                    IsActive = true,
                    UserId = userId,
                };

                await _context.ShoppingCarts.AddAsync(newCart);
                await _context.SaveChangesAsync();

                return newCart;
            }

            return cart;
        }

        public async Task RemoveItemFromCartAsync(int cartItemId)
        {
            var cartItem = await _context.ShoppingCartItems.FindAsync(cartItemId);

            if (cartItem is not null)
            {
                _context.Remove(cartItem);
                await _context.SaveChangesAsync();
                await RecalculateTotalPrice(cartItem.ShoppingCartId);
            }
        }
    }
}