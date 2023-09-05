using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeWebApp.Data.Models;
using System.Runtime.CompilerServices;

namespace RecipeWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Recipe> Recipies { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Recipe>()
                                 .HasOne(r => r.Image)
                                 .WithOne(i => i.Recipe)
                                 .HasForeignKey<Image>(i => i.RecipeId)
                                 .OnDelete(DeleteBehavior.Restrict);



            base.OnModelCreating(builder);
        }

    }
}