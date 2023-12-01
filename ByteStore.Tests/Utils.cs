using ByteStore.Domain.Entities;
using ByteStore.Infrastructure;
using ByteStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.Tests;

public abstract class Utils
{
    internal static AppDbContext GetDbContext()
    {
        var dbOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        return new AppDbContext(dbOptionsBuilder.Options);
    }

    internal static ProductRepository GetProductRepository()
    {
        var dbContext = Utils.GetDbContext();
        return new ProductRepository(dbContext);
    }

    internal static Product GetProductMock()
    {
        return new Product
        {
            ProductId = 1,
            ProductQuantity = 10,
            Name = "BOLA QUADRADA",
            Price = 999,
            ImageStorageUrl = "URL",
            Description = "SIM, UMA BOLA QUADRADA",
            Reviews = null,
            Rate = 5,
            TimesRated = 777
        };
    }
}