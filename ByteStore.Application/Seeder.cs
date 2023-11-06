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
        await SeedProducts();
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
        var products = new List<ProductDto>
        {
            new ProductDto
            {
                Name = "Amazfit Huami 2022 model",
                Price = 100M,
                ProductQuantity = 10,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FAmazfit%20Huami%202022%20model.webp?alt=media&token=67924ccf-432d-4959-ba20-c23edcd4b633&_gl=1*vclouz*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTIzMTE5MC4xMi4xLjE2OTkyMzI1MzMuMTAuMC4w",
                Description =
                    "Smartwatch Amazfit Bip 3 Bluetooth 5.0 1.69-Inch Screen Sports Water Resistance\n\nAmazfit's smartwatches make a difference: modern design and performance combined for a great experience. The screen stands out from other watches for its quality and excellent display, even in full daylight. Furthermore, they offer very useful sports modes for all types of exercises."
            },
            new ProductDto
            {
                Name = "Apple iPhone 12 (128GB)",
                Price = 600M,
                ProductQuantity = 17,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FIphone?alt=media&token=c594fa4d-e34f-482c-bcfa-8aba8db88dc4&_gl=1*qkib5q*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTIzMTE5MC4xMi4xLjE2OTkyMzI1NDIuMS4wLjA.",
                Description =
                    "This Apple iPhone 12 has been professionally restored to working order by an approved vendor. Manufacturer Refurbished Grade B Fully Functional in Excellent Working Condition. 8/10 Cosmetic Rating. Fully Unlocked compatible with all carriers. Internally, the iPhone 12 is powered by a 64-bit 3.0 GHz \"Apple A14 Bionic\" processor with six cores -- two performance cores and four high-efficiency cores -- and a 16-core Neural Engine. It has 4 GB of RAM, 128GB of flash storage."
            },
            new ProductDto
            {
                Name = "MacBook Pro M3",
                Price = 1599.00M,
                ProductQuantity = 5,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FMacbook?alt=media&token=cd409557-97e2-488b-b995-04cf099558e1&_gl=1*1dhwcny*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTIzMTE5MC4xMi4xLjE2OTkyMzI1NDkuNTQuMC4w",
                Description =
                    "14-inch Liquid Retina XDR display\u00b2\nTwo Thunderbolt / USB 4 ports, HDMI port, SDXC card slot, headphone jack, MagSafe 3 port\nMagic Keyboard with Touch ID\nForce Touch trackpad\n70W USB-C Power Adapter"
            },
            new ProductDto
            {
                Name = "Monitor AOC 21.5\" VGA e HDMI e painel VA",
                Price = 1299.00M,
                ProductQuantity = 47,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FMonitor?alt=media&token=002b76f3-c911-4447-809e-348b3942ee01&_gl=1*vumpi1*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTIzMTE5MC4xMi4xLjE2OTkyMzI2NDkuMzIuMC4w",
                Description =
                    "Developed to support all the emotions of your games, the incredible 1ms response time offers high speed, definition, and smoothness in all your movements. Improve the accuracy and speed of your plays with the aim mode feature: a red crosshair positioned in the center of the screen to make you the best shooter in the match."
            },
            new ProductDto
            {
                Name = "Monitor AOC 21.5\" VGA e HDMI e painel VA",
                Price = 1299.00M,
                ProductQuantity = 47,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FMonitor?alt=media&token=002b76f3-c911-4447-809e-348b3942ee01&_gl=1*vumpi1*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTIzMTE5MC4xMi4xLjE2OTkyMzI2NDkuMzIuMC4w",
                Description =
                    "Developed to support all the emotions of your games, the incredible 1ms response time offers high speed, definition, and smoothness in all your movements. Improve the accuracy and speed of your plays with the aim mode feature: a red crosshair positioned in the center of the screen to make you the best shooter in the match."
            },
        };
        foreach (var product in products)
        {
            await _productService.AddProduct(new ProductDto
            {
                Name = product.Name,
                Price = product.Price,
                ProductQuantity = product.ProductQuantity,
                ImageStorageUrl = product.ImageStorageUrl,
                Description = product.Description
            });
        }
    }
}