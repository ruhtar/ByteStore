using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using Microsoft.Extensions.Hosting;

namespace ByteStore.Application;

public class Seeder : IHostedService
{
    private readonly IUserService _userService;
    private readonly IProductService _productService;

    public Seeder(IUserService userService, IProductService productService)
    {
        _userService = userService;
        _productService = productService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await SeedUserAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task SeedUserAsync()
    {
        var user = new User()
        {
            Username = "Arthur",
            Password = "!123Qwe",
        };
        var address = new Address
        {
            Street = "Rua dos Bobos",
            StreetNumber = 0,
            City = "Aracaju",
            State = "Sergipe",
            Country = "Brasil"
        };
        var userDto = new SignupUserDto
        {
            User = user,
            Role = Roles.Seller,
            Address = address
        };
        await _userService.RegisterUser(userDto);
    }

    private async Task SeedProducts()
    {
        await _productService.AddProduct(new ProductDto
        {
            Name = null,
            Price = 0,
            ProductQuantity = 0,
            Image = null,
            ImageStorageUrl = null,
            Description = null
        });
    }
}