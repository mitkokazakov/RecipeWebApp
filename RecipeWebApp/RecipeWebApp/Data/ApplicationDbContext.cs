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
        public DbSet<SubIngredient> SubIngredients { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Recipe>()
                                 .HasOne(r => r.Image)
                                 .WithOne(i => i.Recipe)
                                 .HasForeignKey<Image>(i => i.RecipeId)
                                 .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Recipe>()
                                 .HasMany(r => r.SubIngredients)
                                 .WithOne(si => si.Recipe)
                                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SubIngredient>()
                                 .HasOne(si => si.Recipe)
                                 .WithMany(r => r.SubIngredients)
                                 .HasForeignKey(si => si.RecipeId)
                                 .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

    }
}