using Microsoft.AspNetCore.Mvc;
using ProductsShop.Models;
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

        public async Task<IActionResult> Index()
        {
            var discounts = await _discountService.GetActiveDiscountsAsync();
            return View(discounts);
        }

        //Get: Disount/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CompanyName,ImageURL,Description")] Discount discount)
        {
            if (!ModelState.IsValid)
            {
                return View(discount);
            }

            await _discountService.AddAsync(discount);
            return RedirectToAction(nameof(Index));
        }

        //Get Disount/Details/
        public async Task<IActionResult> Details(int id)
        {
            var discount = await _discountService.GetByIdAsync(id);
            return discount is null ? View("NotFound") : View(discount);
        }
        //Get: Disount/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var discount = await _discountService.GetByIdAsync(id);
            return discount is null ? View("NotFound") : View(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,ImageURL,Description")] Discount discount)
        {
            if (!ModelState.IsValid)
            {
                return View(discount);
            }

            await _discountService.UpdateAsync(discount);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var discount = await _discountService.GetByIdAsync(id);
            return discount is null ? View("NotFound") : View(discount);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var categoryDetails = await _discountService.GetByIdAsync(id);

            if (categoryDetails is null)
            {
                return View("NotFound");
            }

            await _discountService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
