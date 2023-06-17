using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsShop.Data.ViewModels;
using ProductsShop.Services.Interfaces;

namespace ProductsShop.Controllers
{

    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IDiscountService _discountService;

        public ProductsController(IProductsService productsService, IDiscountService discountService)
        {
            _productsService = productsService;
            _discountService = discountService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _productsService.GetAllProductsAsync();
            return View(allProducts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            searchString = searchString?.Trim(); // Видаляємо пробіли з початку і кінця рядка

            var allProducts = await _productsService.GetAllProductsAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = allProducts.Where(n =>
                    n.ProductName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                    n.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                    n.Category.CategoryName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                    n.Company.CompanyName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
                ).ToList();

                return View("Index", filteredResultNew);
            }

            return RedirectToAction(nameof(Index));
        }

        //GET: Products/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _productsService.GetProductByIdAsync(id);
            return View(productDetail);
        }

        //GET: Products/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _productsService.GetNewProductDropdownsValues();

            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "CategoryName");
            ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "CompanyName");

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _productsService.GetNewProductDropdownsValues();

                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "CategoryName");
                ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "CompanyName");

                return View(product);
            }

            await _productsService.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //GET: Product/Edit/1
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _productsService.GetProductByIdAsync(id);
            if (productDetails is null) return View("NotFound");

            var response = new NewProductVM()
            {
                Id = productDetails.Id,
                ProductName = productDetails.ProductName,
                Description = productDetails.Description,
                Price = productDetails.Price,
                Quantity = productDetails.Quantity,
                ImageURL = productDetails.ImageURL,
                CategoryId = productDetails.CategoryId,
                CompanyId = productDetails.CompanyId,
            };

            var productDropdownsData = await _productsService.GetNewProductDropdownsValues();
            var availableDiscounts = await _discountService.GetActiveDiscountsAsync();
            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "CategoryName");
            ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "CompanyName");
            ViewBag.Discounts = new SelectList(availableDiscounts, "Id", "Name");

            return View(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _productsService.GetNewProductDropdownsValues();
                var availableDiscounts = await _discountService.GetActiveDiscountsAsync();

                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "CategoryName");
                ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "CompanyName");
                ViewBag.Discounts = new SelectList(availableDiscounts, "Id", "Name");

                return View(product);
            }

            await _productsService.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}