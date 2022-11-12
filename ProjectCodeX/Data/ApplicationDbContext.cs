using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectCodeX.Models;

namespace ProjectCodeX.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<NewsPost> Posts { get; set; }
        public DbSet<User> AdditionalUserInformation { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}