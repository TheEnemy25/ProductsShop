using Microsoft.AspNetCore.Mvc;
using ProductsShop.Data.Cart;

namespace ProductsShop.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCartItem _shoppingCartItem;
        public ShoppingCartSummary(ShoppingCartItem shoppingCartItem)
        {
            _shoppingCartItem = shoppingCartItem;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCartItem.GetShoppingCartItems();

            return View(items.Count);
        }
    }
}
