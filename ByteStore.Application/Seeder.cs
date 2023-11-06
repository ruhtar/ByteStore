using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure;
using ByteStore.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ByteStore.Application;

public class Seeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Seeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var result = await context.SeederFlag.FirstOrDefaultAsync(x => x.IsSeeded, cancellationToken: cancellationToken);
        if (result == null || result.IsSeeded) return;
        result.IsSeeded = true;
        await context.SaveChangesAsync(cancellationToken);
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
        var userService = _serviceProvider.GetRequiredService<IUserService>();
        await userService.RegisterUser(userDto);
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
                    "14-inch Liquid Retina XDR display\u00b2\nTwo Thunderbolt / USB 4 ports, HDMI port, SDXC card slot, headphone jack, MagSafe 3 port\nMagic Keyboard with Touch ID\nForce Touch trackpad\n70W USB-C Power Adapter."
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
                Name = "PlayStation 5 Console (PS5)",
                Price = 400.00M,
                ProductQuantity = 15,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Fplaystation.jfif?alt=media&token=a5dc0db6-c65f-4a56-a38c-2f0facb83992&_gl=1*ki3vxp*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTI2Nzc3OC4xMy4xLjE2OTkyNjc5NTguNTEuMC4w",
                Description =
                    "The PS5 console unleashes new gaming possibilities that you never anticipated. Experience lightning fast loading with an ultra-high speed SSD, deeper immersion with support for haptic feedback, adaptive triggers, and 3D Audio, and an all-new generation of incredible PlayStation games."
            },
            new ProductDto
            {
                Name = "AirPods Pro 2 with MagSafe Charging Case (USB-C)",
                Price = 249.00M,
                ProductQuantity = 60,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FTeste?alt=media&token=1846569e-1c80-4244-8e4a-44a9c539bdb0&_gl=1*1cj9i93*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTI2Nzc3OC4xMy4xLjE2OTkyNjgwMDkuNjAuMC4w",
                Description =
                    "\nRicher audio experience - Apple's H2 chip helps create smarter noise cancellation and deeply immersive sound. The custom low-distortion driver delivers crisp high notes and full, rich bass in stunning clarity.\n\nLEGAL DISCLAIMERS - This is a summary of the product's main features. Refer to \"Additional Information\" for more details.\n\nNEXT-LEVEL ACTIVE NOISE CANCELLATION - Up to 2x more active noise cancellation to significantly reduce more noise when you want to focus. The Transparency mode allows you to hear the world around you, and adaptive audio seamlessly combines active noise cancellation and transparency mode for the best listening experience in any environment."
            },
            new ProductDto
            {
                Name = "Logitech - C920s Pro 1080 Webcam with Privacy Shutter - Black",
                Price = 69.99M,
                ProductQuantity = 35,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FWebcam?alt=media&token=edcf051f-fbdb-4130-9f2e-7e9af051a84c&_gl=1*1p28m2n*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTI2Nzc3OC4xMy4xLjE2OTkyNjgxMzIuNjAuMC4w",
                Description =
                    "The webcam offers design and optimization for professional video streaming. Realistic 1920 x 1080p video, 5-layer anti-reflective lens, providing smooth video. The fixed focal length makes the object within the focal range of 30 to 300 cm, delivering a clearer image. The USB webcam C960 has a cover that can be automatically removed to meet your protection needs. It's a great choice for a home office.\n\n\u200b2\u200bThe webcam features 2 built-in omnidirectional noise reduction microphones, capturing your voice and filtering out background noise to create an excellent audio effect. The EMEET webcam allows you to enjoy crystal-clear voice for hassle-free communication. (When installing the webcam, remember to select the EMEET C960 USB webcam as the default device for the microphones)."
            },
            new ProductDto
            {
                Name = "SAMSUNG Android Tablet Galaxy Tab A8 10.5 inches 32GB",
                Price = 172.99M,
                ProductQuantity = 29,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Ftablet.jpg?alt=media&token=0f73503d-83e8-4665-8b53-a348a5c4216d&_gl=1*wgs4vu*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTI2Nzc3OC4xMy4xLjE2OTkyNjg0MTUuNTUuMC4w",
                Description =
                    "A screen that everyone will love: whether your family is streaming or video chatting with friends, the Galaxy Tab A8 tablet brings out the best in every moment on a 10.5\" LCD screen. Card Description: Dedicated\nPower and storage for all: get the power, storage, and speed your family needs with an updated chipset and plenty of space to store files—up to 128GB of storage; a long-lasting battery allows you to disconnect for hours to keep the family fun going.\nFast charge, hours of power: go for hours on a single charge and return to 100% with the fast-charging USB-C port; battery life may vary depending on the network environment, usage patterns, and other factors."
            },
        };
        foreach (var product in products)
        {
            var productService = _serviceProvider.GetRequiredService<IProductService>();

            await productService.AddProduct(new ProductDto
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