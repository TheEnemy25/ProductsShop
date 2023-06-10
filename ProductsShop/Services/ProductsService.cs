using Microsoft.EntityFrameworkCore;
using ProductsShop.Data.Context;
using ProductsShop.Data.Repositories;
using ProductsShop.Data.ViewModels;
using ProductsShop.Models;
using ProductsShop.Services.Interfaces;

namespace ProductsShop.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly ApplicationDbContext _context;

        public ProductsService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                ProductName = data.ProductName,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CategoryId = data.CategoryId,
                CompanyId = data.CompanyId,

            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            //Add Product Discount
            //foreach (var discountId in data.DiscountId)
            //{
            //    var newDiscountProduct = new DiscountProduct()
            //    {
            //        ProductId = newProduct.Id,
            //        DiscountId = discountId
            //    };
            //    await _context.DiscountProducts.AddAsync(newDiscountProduct);
            //}
            //await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(x => x.Company)
                .Include(x => x.Category)
                .Include(x => x.DiscountProducts)
                    .ThenInclude(x => x.Discount)
                .ToListAsync();

            return products;
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {
                Categories = await _context.Categories.OrderBy(n => n.CategoryName).ToListAsync(),
                Companies = await _context.Companies.OrderBy(n => n.CompanyName).ToListAsync()
            };
            return response;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetail = await _context.Products
                .Include(c => c.Company)
                .Include(c => c.Category)
                .Include(dp => dp.DiscountProducts)
                    .ThenInclude(d => d.Discount)
                .FirstOrDefaultAsync(n => n.Id == id);

            return productDetail;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.ProductName = data.ProductName;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageURL = data.ImageURL;
                dbProduct.CategoryId = data.CategoryId;
                dbProduct.CompanyId = data.CompanyId;

                if (data.DiscountId != default)
                {
                    var discountProduct = new DiscountProduct
                    {
                        ProductId = dbProduct.Id,
                        DiscountId = data.DiscountId
                    };
                    await _context.DiscountProducts.AddAsync(discountProduct);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
