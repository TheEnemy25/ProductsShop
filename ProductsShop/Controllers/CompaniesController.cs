using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.Context;
using ProductsShop.Models;
using ProductsShop.Services.Interfaces;

namespace ProductsShop.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService _companyService;

        public CompaniesController(ICompaniesService service)
        {
            _companyService = service;
        }

        public async Task<IActionResult> Index()
        {
            var categies = await _companyService.GetAllAsync();
            return View(categies);
        }

        //Get: Categories/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CompanyName,ImageURL,Description")] Company company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }
            await _companyService.AddAsync(company);
            return RedirectToAction(nameof(Index));
        }

        //Get Categories/Details/
        public async Task<IActionResult> Details(int id)
        {
            var companyDetails = await _companyService.GetByIdAsync(id);

            if (companyDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(companyDetails);
            }
        }
        //Get: Categories/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var companyDetails = await _companyService.GetByIdAsync(id);

            if (companyDetails == null)
            {
                return View("NotFound");
            }
            return View(companyDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,ImageURL,Description")] Company company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }
            await _companyService.UpdateAsync(id, company);
            return RedirectToAction(nameof(Index));
        }

        //Get: Categories/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var companyDetails = await _companyService.GetByIdAsync(id);

            if (companyDetails == null)
            {
                return View("NotFound");
            }
            return View(companyDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var companyDetails = await _companyService.GetByIdAsync(id);
            if (companyDetails == null)
            {
                return View("NotFound");
            }
            await _companyService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
