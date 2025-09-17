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
        public DbSet<Likes> Likes { get; set; }
        public DbSet<TypeContent> TypeContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Configura a chave primária composta para a entidade Likes
        {
            
            modelBuilder.Entity<Likes>()
                .HasKey(l => new { l.UserId, l.PostId });

            modelBuilder.Entity<Likes>()
                .HasOne(l => l.User)          // um like tem um User
                .WithMany(u => u.Likes)       // um user tem muitos Likes
                .HasForeignKey(l => l.UserId) // FK é userid
                .OnDelete(DeleteBehavior.Restrict); // impede o delete em cascata
        }
    }
}
