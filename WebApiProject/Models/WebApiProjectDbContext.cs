using Microsoft.EntityFrameworkCore;
using WebApiProject.Models.DomainModels.ProductAggregates;

namespace WebApiProject.Models
{
    public class WebApiProjectDbContext : DbContext
    {
        public WebApiProjectDbContext()
        {

        }

        public WebApiProjectDbContext(DbContextOptions options) : base(options)
        {

        }






        public DbSet<Product> Product { get; set; }
    }
}
