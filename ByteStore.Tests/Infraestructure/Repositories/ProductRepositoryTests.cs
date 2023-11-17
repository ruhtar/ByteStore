using ByteStore.Domain.Entities;
using ByteStore.Infrastructure;
using ByteStore.Infrastructure.Repository;
using ByteStore.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ByteStore.Tests.Infraestructure.Repositories;
//https://dejanstojanovic.net/aspnet/2019/september/unit-testing-repositories-in-aspnet-core-with-xunit-and-moq/
public class ProductRepositoryTests
{
    [Fact]
    public async Task GetAllProducts_ReturnsAListOfProducts()
    {
        var input = new GetProductsInputPagination { PageSize = 2, PageIndex = 0 };
        var productsData = new List<Product>
        {
            new Product { ProductId = 1, Name = "Product1" },
            new Product { ProductId = 2, Name = "Product2" },
            new Product { ProductId = 3, Name = "Product 3" },
            new Product { ProductId = 4, Name = "Product 4" },
            new Product { ProductId = 5, Name = "Product 5" },
            new Product { ProductId = 6, Name = "Product 6" }
            // Add more products as needed
        };
        
        var context = new Mock<AppDbContext>();  
        var dbSet = new Mock<DbSet<Product>>();
        context.Setup(context => context.Set<Product>()).Returns(dbSet.Object);
        dbSet.Setup(s => s.ToList()).Returns(productsData);

        var productRepository = new ProductRepository(context.Object);
        var products = await productRepository.GetAllProducts(input);
        
        //Assert
        Assert.NotNull(products);
    }
}