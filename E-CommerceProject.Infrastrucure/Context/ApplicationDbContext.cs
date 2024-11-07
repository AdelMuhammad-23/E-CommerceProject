using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_CommerceProject.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }




        public static async Task SeedData(ApplicationDbContext context)
        {
            var categories = ApplicationContextSeed.LoadCategoriesFromJson("D:\\Projects\\E-CommerceProject\\E-CommerceProject.Infrastrucure\\DataSeeding\\category.json");
            var products = ApplicationContextSeed.LoadProductsFromJson("D:\\Projects\\E-CommerceProject\\E-CommerceProject.Infrastrucure\\DataSeeding\\products.json");

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(categories);
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(products);
            }

            var productCategories = new List<ProductCategory>();
            foreach (var product in products)
            {
                foreach (var category in categories)
                {
                    if (product.ProductCategories != null && product.ProductCategories.Any(pc => pc.CategoryId == category.CategoryId))
                    {
                        productCategories.Add(new ProductCategory { ProductId = product.ProductId, CategoryId = category.CategoryId });
                    }
                }
            }

            if (!context.ProductCategories.Any())
            {
                context.ProductCategories.AddRange(productCategories);
            }

            await context.SaveChangesAsync();
        }

    }

}
