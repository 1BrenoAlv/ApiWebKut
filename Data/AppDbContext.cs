using ApiWebKut.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebKut.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}
