using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;

namespace ProductsShop.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<DiscountProduct> DiscountProducts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ShoppingCart>().ToTable(nameof(ShoppingCart));

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<DiscountProduct>()
                .HasOne(dp => dp.Product)
                .WithMany(p => p.DiscountProducts)
                .HasForeignKey(dp => dp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DiscountProduct>()
                .HasOne(dp => dp.Discount)
                .WithMany(d => d.DiscountProducts)
                .HasForeignKey(dp => dp.DiscountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Carts)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(x => x.ShoppingCart)
                .WithMany(x => x.ShoppingCartItems)
                .HasForeignKey(p => p.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShoppingCart)
                .WithOne(s => s.Order)
                .HasForeignKey<ShoppingCart>(s => s.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(o => o.Order)
                .WithOne(s => s.ShoppingCart)
                .HasForeignKey<Order>(s => s.ShoppingCartId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}