using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAggregate> UserAggregates { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAggregate>()
            .HasOne(e => e.ShoppingCart)
            .WithOne(e => e.UserAggregate)
            .HasForeignKey<ShoppingCart>(e => e.UserAggregateId);

            modelBuilder.Ignore<Address>();

            modelBuilder.Entity<UserAggregate>(entity =>
            {
                entity.OwnsOne(u => u.Address, address =>
                {
                    address.Property(a => a.Street).HasColumnName("Street");
                    address.Property(a => a.StreetNumber).HasColumnName("StreetNumber");
                    address.Property(a => a.City).HasColumnName("City");
                    address.Property(a => a.State).HasColumnName("State");
                    address.Property(a => a.Country).HasColumnName("Country");
                });
            });

            modelBuilder.Entity<UserAggregate>().Property(e => e.Role)
                .HasConversion<string>();

            //TODO: ShoppingCart - Product (N:N)

            modelBuilder.Entity<ShoppingCartProduct>()
                .HasKey(scp => new { scp.ShoppingCartId, scp.ProductId });

            modelBuilder.Entity<ShoppingCartProduct>()
                .HasOne(scp => scp.ShoppingCart)
                .WithMany(sc => sc.ShoppingCartProducts)
                .IsRequired(false)
                .HasForeignKey(scp => scp.ShoppingCartId);

            modelBuilder.Entity<ShoppingCartProduct>()
                .HasOne(scp => scp.Product)
                .WithMany(p => p.ShoppingCartProducts)
                .IsRequired(false)
                .HasForeignKey(scp => scp.ProductId);


            //Order - ShoppingCart (1:1)

            //modelBuilder.Entity<ShoppingCart>()
            //.HasOne(e => e.Order)
            //.WithOne(e => e.ShoppingCart)
            //.HasForeignKey<Order>(e => e.ShoppingCartId);

            ////Order - Products (N:N)

            //modelBuilder.Entity<OrderProduct>()
            //    .HasKey(op => new { op.OrderId, op.ProductId });

            //modelBuilder.Entity<OrderProduct>()
            //    .HasOne(op => op.Order)
            //    .WithMany(o => o.OrderProducts)
            //    .HasForeignKey(op => op.OrderId);

            //modelBuilder.Entity<OrderProduct>()
            //    .HasOne(op => op.Product)
            //    .WithMany(p => p.OrderProducts)
            //    .IsRequired(false)
            //    .HasForeignKey(op => op.ProductId);
        }
    }
}
