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

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _productsService.GetAllAsync(m => m.Category, n => n.Company);
            return View(allProducts);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _productsService.GetAllAsync(n => n.ProductName);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = allProducts.Where(n => string.Equals(n.ProductName, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
                return View(filteredResultNew);
            }
            return View("Index", allProducts);
        }

        //GET: Products/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _productsService.GetProductByIdAsync(id);
            return View(productDetail);
        }

        //GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _productsService.GetNewProductDropdownsValues();

            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "CategoryName");
            ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "CompanyName");

            return View();
        }


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
            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "CategoryName");
            ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "CompanyName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _productsService.GetNewProductDropdownsValues();

                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "CategoryName");
                ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "CompanyName");

                return View(product);
            }

            await _productsService.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}