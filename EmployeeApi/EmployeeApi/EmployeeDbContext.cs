using EmployeeApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi
{
    public class EmployeeDbContext: IdentityDbContext<ApplicationUser>
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options):base(options) 
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
        }

        public DbSet<Employees> employees { get; set; } 
        public DbSet<ApplicationUser>Users { get; set; }
    }
}
