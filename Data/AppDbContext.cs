using ApiWebKut.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebKut.Data
{
    public class AppDbContext : DbContext 
    {
        // CONSTRUTOR
        public AppDbContext(DbContextOptions options) : base(options) { } 


        public DbSet<User> User { get; set; }  // CRIA A TABELA NO DB
    }
}
