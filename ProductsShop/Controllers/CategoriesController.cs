using Microsoft.AspNetCore.Mvc;
using ProductsShop.Models;
using ProductsShop.Services.Interfaces;

namespace ProductsShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var categies = await _categoriesService.GetAllAsync();
            return View(categies);
        }

        //Get: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CategoryName,ImageURL,Description")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _categoriesService.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        //Get Categories/Details/
        public async Task<IActionResult> Details(int id)
        {
            var categoryDetails = await _categoriesService.GetByIdAsync(id);

            if (categoryDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(categoryDetails);
            }
        }
        //Get: Categories/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDetails = await _categoriesService.GetByIdAsync(id);

            if (categoryDetails == null)
            {
                return View("NotFound");
            }
            return View(categoryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,ImageURL,Description")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _categoriesService.UpdateAsync(id, category);
            return RedirectToAction(nameof(Index));
        }

        //Get: Categories/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDetails = await _categoriesService.GetByIdAsync(id);

            if (categoryDetails == null)
            {
                return View("NotFound");
            }
            return View(categoryDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var categoryDetails = await _categoriesService.GetByIdAsync(id);
            if (categoryDetails == null)
            {
                return View("NotFound");
            }
            await _categoriesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}