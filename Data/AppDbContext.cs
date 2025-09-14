using ApiWebKut.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebKut.Data
{
    public class AppDbContext : DbContext 
    {
        // CONSTRUTOR
        public AppDbContext(DbContextOptions options) : base(options) { } 


        public DbSet<Users> Users { get; set; }  // CRIA A TABELA NO DB
        public DbSet<Posts> Posts { get; set; }
        public DbSet<TypeContent> TypeContet { get; set; }
        public DbSet<Likes> Likes { get; set; }
    }
}
