using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProducts> ShoppingCartProducts { get; set; }
        public DbSet<UserAggregate> UserAggregates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAggregate>()
            .HasOne(e => e.ShoppingCart)
            .WithOne(e => e.UserAggregate)
            .HasForeignKey<ShoppingCart>(e => e.UserAggregateId);

            modelBuilder.Entity<ShoppingCartProducts>().HasKey(c => new { c.ShoppingCartId, c.ProductId });

            modelBuilder.Entity<ShoppingCartProducts>()
                .HasOne(c => c.Product)
                .WithMany(c => c.ShoppingCartProducts)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<ShoppingCartProducts>()
                .HasOne(c => c.ShoppingCart)
                .WithMany(c => c.ShoppingCartProducts)
                .HasForeignKey(c => c.ShoppingCartId);
        }
    }
}
