using Microsoft.EntityFrameworkCore;
using RESTfulAPIs.Models;

namespace RESTfulAPIs.Data
{
    public class ApplicationDbContext :  DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }



    }
}
