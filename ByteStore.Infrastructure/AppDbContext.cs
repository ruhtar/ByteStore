using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.Infrastructure;

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
    }
}