using Health_Joy_Mobile_Backend.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Health_Joy_Mobile_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // User ve Product arasındaki ilişki
            modelBuilder.Entity<User>()
                .HasMany(u => u.Favorites)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired();
            
            
            // Configure many-to-many relationship between Product and Ingredient
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Ingredients)
                .WithMany(i => i.Products)
                .UsingEntity(join => join.ToTable("ProductIngredients")); // Customize the join table name if needed
        

            base.OnModelCreating(modelBuilder);
        }
    }
}
