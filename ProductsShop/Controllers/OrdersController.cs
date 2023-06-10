using Microsoft.AspNetCore.Mvc;

namespace ProductsShop.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
