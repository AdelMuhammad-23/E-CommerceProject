using E_CommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
