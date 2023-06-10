using Microsoft.AspNetCore.Mvc;
using ProductsShop.Services.Interfaces;

namespace ProductsShop.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
