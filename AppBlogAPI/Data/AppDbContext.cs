using AppBlogAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppBlogAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
              
        public DbSet<Post> Posts { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content);
                entity.Property(e => e.Title).HasMaxLength(50);
                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.AspNetUsersId);
              
            });         


            base.OnModelCreating(modelBuilder);
        }

    }
}
