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
        
        public DbSet<UserProductFavorite> UserProductFavorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<UserProductFavorite>()
                .HasKey(upf => new { upf.UserId, upf.ProductId });

            modelBuilder.Entity<UserProductFavorite>()
                .HasOne(upf => upf.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(upf => upf.UserId);

            modelBuilder.Entity<UserProductFavorite>()
                .HasOne(upf => upf.Product)
                .WithMany(p => p.UserFavorites)
                .HasForeignKey(upf => upf.ProductId);
            
            // Configure many-to-many relationship between Product and Ingredient
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Ingredients)
                .WithMany(i => i.Products)
                .UsingEntity(join => join.ToTable("ProductIngredients")); // Customize the join table name if needed
        

            base.OnModelCreating(modelBuilder);
        }
    }
}
