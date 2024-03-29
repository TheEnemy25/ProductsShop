﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
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
            var company = await _companyService.GetByIdAsync(id);
            return company is null ? View("NotFound") : View(company);
        }
        //Get: Categories/Edit/1
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyService.GetByIdAsync(id);
            return company is null ? View("NotFound") : View(company);
        }

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _companyService.GetByIdAsync(id);
            return company is null ? View("NotFound") : View(company);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var companyDetails = await _companyService.GetByIdAsync(id);

            if (companyDetails is null)
            {
                return View("NotFound");
            }

            await _companyService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
