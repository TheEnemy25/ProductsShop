using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductsShop.Models;
using ProductsShop.Services.Interfaces;
using System.Security.Claims;

namespace ProductsShop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(IShoppingCartService shoppingCartService, UserManager<ApplicationUser> userManager)
        {
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);

            var cart = await _shoppingCartService.GetActiveCartForUserAsync(user.Id);

            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);

            await _shoppingCartService.AddToCartAsync(productId, user.Id, quantity);
            
            return RedirectToAction(controllerName: "Products", actionName: "Index");
        }

        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            await _shoppingCartService.RemoveItemFromCartAsync(cartItemId);

            return RedirectToAction("Index");
        }
    }
}
