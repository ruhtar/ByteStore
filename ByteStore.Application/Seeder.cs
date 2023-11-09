using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain;
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
        using (var scope = _serviceProvider.CreateScope())
        {
            //https://stackoverflow.com/questions/48590579/cannot-resolve-scoped-service-from-root-provider-net-core-2
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // https://stackoverflow.com/questions/51618406/cannot-consume-scoped-service-mydbcontext-from-singleton-microsoft-aspnetcore
            var result =
                await context.SeederFlag.FirstOrDefaultAsync(x => x.IsSeeded, cancellationToken: cancellationToken);
            if (result == null)
            {
                await context.SeederFlag.AddAsync(new SeederFlag
                {
                    IsSeeded = true
                }, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                await SeedUserAsync(scope);
                await SeedProductsAsync(scope);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private static async Task SeedUserAsync(IServiceScope scope)
    {
        var user = new User()
        {
            Username = "Admin",
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
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        await userService.RegisterUser(userDto);
    }

    private async Task SeedProductsAsync(IServiceScope scope)
    {
        var products = new List<ProductDto>
        {
            new ProductDto
            {
                Name = "Amazfit Huami 2022 model",
                Price = 100M,
                ProductQuantity = 10,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Frelogio.jpg?alt=media&token=2a56a1d2-4650-4e94-86b6-34a3c2e0c392",
                Description =
                    "Smartwatch Amazfit Bip 3 Bluetooth 5.0 1.69-Inch Screen Sports Water Resistance\n\nAmazfit's smartwatches make a difference: modern design and performance combined for a great experience. The screen stands out from other watches for its quality and excellent display, even in full daylight. Furthermore, they offer very useful sports modes for all types of exercises."
            },
            new ProductDto
            {
                Name = "Apple iPhone 12 (128GB)",
                Price = 600M,
                ProductQuantity = 17,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FIphone?alt=media&token=c594fa4d-e34f-482c-bcfa-8aba8db88dc4&_gl=1*qkib5q*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTIzMTE5MC4xMi4xLjE2OTkyMzI1NDIuMS4wLjA.",
                Description =
                    "This Apple iPhone 12 has been professionally restored to working order by an approved vendor. Manufacturer Refurbished Grade B Fully Functional in Excellent Working Condition. 8/10 Cosmetic Rating. Fully Unlocked compatible with all carriers. Internally, the iPhone 12 is powered by a 64-bit 3.0 GHz \"Apple A14 Bionic\" processor with six cores -- two performance cores and four high-efficiency cores -- and a 16-core Neural Engine. It has 4 GB of RAM, 128GB of flash storage."
            },
            new ProductDto
            {
                Name = "MacBook Pro M3",
                Price = 1599.00M,
                ProductQuantity = 5,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FMacbook?alt=media&token=cd409557-97e2-488b-b995-04cf099558e1&_gl=1*1dhwcny*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTIzMTE5MC4xMi4xLjE2OTkyMzI1NDkuNTQuMC4w",
                Description =
                    "14-inch Liquid Retina XDR display\u00b2\nTwo Thunderbolt / USB 4 ports, HDMI port, SDXC card slot, headphone jack, MagSafe 3 port\nMagic Keyboard with Touch ID\nForce Touch trackpad\n70W USB-C Power Adapter."
            },
            new ProductDto
            {
                Name = "Monitor AOC 21.5\" VGA e HDMI e painel VA",
                Price = 1299.00M,
                ProductQuantity = 47,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FMonitor?alt=media&token=002b76f3-c911-4447-809e-348b3942ee01&_gl=1*vumpi1*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTIzMTE5MC4xMi4xLjE2OTkyMzI2NDkuMzIuMC4w",
                Description =
                    "Developed to support all the emotions of your games, the incredible 1ms response time offers high speed, definition, and smoothness in all your movements. Improve the accuracy and speed of your plays with the aim mode feature: a red crosshair positioned in the center of the screen to make you the best shooter in the match."
            },
            new ProductDto
            {
                Name = "PlayStation 5 Console (PS5)",
                Price = 400.00M,
                ProductQuantity = 15,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Fplay5.webp?alt=media&token=a515ea7b-4141-40c0-bd48-ae9bc3ac6228",
                Description =
                    "The PS5 console unleashes new gaming possibilities that you never anticipated. Experience lightning fast loading with an ultra-high speed SSD, deeper immersion with support for haptic feedback, adaptive triggers, and 3D Audio, and an all-new generation of incredible PlayStation games."
            },
            new ProductDto
            {
                Name = "AirPods Pro 2 with MagSafe Charging Case (USB-C)",
                Price = 249.00M,
                ProductQuantity = 60,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FTeste?alt=media&token=1846569e-1c80-4244-8e4a-44a9c539bdb0&_gl=1*1cj9i93*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTI2Nzc3OC4xMy4xLjE2OTkyNjgwMDkuNjAuMC4w",
                Description =
                    "\nRicher audio experience - Apple's H2 chip helps create smarter noise cancellation and deeply immersive sound. The custom low-distortion driver delivers crisp high notes and full, rich bass in stunning clarity.\n\nLEGAL DISCLAIMERS - This is a summary of the product's main features. Refer to \"Additional Information\" for more details.\n\nNEXT-LEVEL ACTIVE NOISE CANCELLATION - Up to 2x more active noise cancellation to significantly reduce more noise when you want to focus. The Transparency mode allows you to hear the world around you, and adaptive audio seamlessly combines active noise cancellation and transparency mode for the best listening experience in any environment."
            },
            new ProductDto
            {
                Name = "Logitech - C920s Pro 1080 Webcam with Privacy Shutter - Black",
                Price = 69.99M,
                ProductQuantity = 35,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FWebcam?alt=media&token=edcf051f-fbdb-4130-9f2e-7e9af051a84c&_gl=1*1p28m2n*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTI2Nzc3OC4xMy4xLjE2OTkyNjgxMzIuNjAuMC4w",
                Description =
                    "The webcam offers design and optimization for professional video streaming. Realistic 1920 x 1080p video, 5-layer anti-reflective lens, providing smooth video. The fixed focal length makes the object within the focal range of 30 to 300 cm, delivering a clearer image. The USB webcam C960 has a cover that can be automatically removed to meet your protection needs. It's a great choice for a home office.\n\n\u200b2\u200bThe webcam features 2 built-in omnidirectional noise reduction microphones, capturing your voice and filtering out background noise to create an excellent audio effect. The EMEET webcam allows you to enjoy crystal-clear voice for hassle-free communication. (When installing the webcam, remember to select the EMEET C960 USB webcam as the default device for the microphones)."
            },
            new ProductDto
            {
                Name = "SAMSUNG Android Tablet Galaxy Tab A8 10.5 inches 32GB",
                Price = 172.99M,
                ProductQuantity = 29,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Ftablet.jpg?alt=media&token=0f73503d-83e8-4665-8b53-a348a5c4216d&_gl=1*wgs4vu*_ga*MTM2NjQyMDEyNS4xNjk3NzIzMDg2*_ga_CW55HF8NVT*MTY5OTI2Nzc3OC4xMy4xLjE2OTkyNjg0MTUuNTUuMC4w",
                Description =
                    "A screen that everyone will love: whether your family is streaming or video chatting with friends, the Galaxy Tab A8 tablet brings out the best in every moment on a 10.5\" LCD screen. Card Description: Dedicated\nPower and storage for all: get the power, storage, and speed your family needs with an updated chipset and plenty of space to store files—up to 128GB of storage; a long-lasting battery allows you to disconnect for hours to keep the family fun going.\nFast charge, hours of power: go for hours on a single charge and return to 100% with the fast-charging USB-C port; battery life may vary depending on the network environment, usage patterns, and other factors."
            },
            new ProductDto
            {
                Name = "Samsung Galaxy S23 Smartphone",
                Price = 799.00M,
                ProductQuantity = 50,
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Fs23.webp?alt=media&token=f4385ad9-84de-41d3-aae2-9b2cd5836981",
                Description =
                    "Experience the latest in smartphone technology with the Samsung Galaxy S23. This powerful device features a high-resolution display, a fast processor, and a versatile camera system. With 5G connectivity and a sleek design, it's the perfect choice for tech enthusiasts."
            },
            new ProductDto
            {
                Name = "Apple MacBook Pro M2 14-inch (2023)",
                Price = 1899.00M,
                ProductQuantity = 40,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Fmac%20m2.webp?alt=media&token=3ab8b525-fcbc-48bd-96ca-30fc52d47d8b",
                Description =
                    "Elevate your productivity with the new Apple MacBook Pro 14-inch. Featuring Apple's M2 Pro chip, this laptop delivers incredible performance and efficiency. The Retina XDR display provides stunning visuals, while the redesigned keyboard ensures a comfortable typing experience. Get ready to take your work and creativity to the next level."
            },
            new ProductDto
            {
                Name = "Xbox Series X Gaming Console",
                Price = 499.99M,
                ProductQuantity = 25,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Fxbox.webp?alt=media&token=cde102b0-80ae-40d8-bfd1-d16b10920e0a",
                Description =
                    "Get ready for a gaming revolution with the Xbox Series X. This next-gen gaming console offers 4K gaming, lightning-fast load times, and a vast library of games. With improved graphics and performance, it's a must-have for any gaming enthusiast."
            },
            new ProductDto
            {
                Name = "DJI Air 2S Drone",
                Price = 999.00M,
                ProductQuantity = 15,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Fdrone.webp?alt=media&token=2ffc746b-e88e-4789-9771-ec5adab4e29e",
                Description =
                    "Explore the skies and capture stunning aerial footage with the DJI Air 2S Drone. Equipped with a powerful camera, intelligent flight modes, and obstacle avoidance technology, this drone is perfect for photographers and videographers. It's easy to fly and delivers professional-grade results."
            },
            new ProductDto
            {
                Name = "Bose QuietComfort 45 Wireless Noise-Canceling Headphones",
                Price = 299.99M,
                ProductQuantity = 30,
                ImageStorageUrl =
                    "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Fbose%20headphone.webp?alt=media&token=7f9d41f3-0352-4a8b-9d6c-c38368e870c3",
                Description =
                    "Enjoy peace and quiet with the Bose QuietComfort 45 headphones. These wireless over-ear headphones offer industry-leading noise cancellation, crystal-clear sound quality, and all-day comfort. Whether you're traveling or working from home, these headphones will enhance your audio experience."
            },
            new ProductDto
            {
                Name = "Canon EOS 90D DSLR Camera",
                Price = 1199.00M,
                ProductQuantity = 20,
                Description =
                    "Capture stunning photos and videos with the Canon EOS 90D DSLR camera. With a 32.5-megapixel sensor, 4K video recording, and fast autofocus, it's a versatile choice for both photographers and videographers. Perfect for capturing your creative vision.",
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2Fcanon.webp?alt=media&token=7ae08205-77e4-4f4b-bba4-84edd1f4aa9d"
            },
            new ProductDto
            {
                Name = "LG OLED 4K Smart TV (65-inch)",
                Price = 1499.99M,
                ProductQuantity = 15,
                Description =
                    "Transform your home entertainment experience with the LG OLED 4K Smart TV. This 65-inch TV features OLED technology for vibrant colors and deep blacks. With built-in smart features and voice control, it offers a cinematic viewing experience in the comfort of your home.",
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FLG%20OLED%204K%20Smart%20TV%20(65-inch).jpg?alt=media&token=159d4c5b-2af6-40db-8d82-7ec1ae54ef1e"
            },
            new ProductDto
            {
                Name = "Razer Blade 17 Gaming Laptop",
                Price = 2499.00M,
                ProductQuantity = 10,
                Description =
                    "Elevate your gaming experience with the Razer Blade 17 gaming laptop. Featuring a high-refresh-rate display, powerful GPU, and customizable RGB keyboard, it's designed for serious gamers. Dominate the competition and enjoy immersive gameplay.",
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FRazer%20Blade%2017%20Gaming%20Laptop.jpg?alt=media&token=b3f9eeb0-b88d-4a97-92e2-6ebacdeee6e9"
            },
            new ProductDto
            {
                Name = "Nest Hello Video Doorbell",
                Price = 199.99M,
                ProductQuantity = 30,
                Description =
                    "Enhance your home security with the Nest Hello Video Doorbell. It provides 24/7 streaming and alerts, two-way communication, and facial recognition. Know who's at your door and keep your home safe with this smart doorbell.",
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FNest%20Hello%20Video%20Doorbell.webp?alt=media&token=9c107aff-5512-4ee4-aa5d-1db25a54be92"
            },
            new ProductDto
            {
                Name = "Bose SoundLink Revolve+ Portable Bluetooth Speaker",
                Price = 249.00M,
                ProductQuantity = 25,
                Description =
                    "Take your music anywhere with the Bose SoundLink Revolve+ portable Bluetooth speaker. With 360-degree sound, long battery life, and water-resistant design, it's perfect for outdoor adventures and gatherings. Enjoy high-quality audio on the go.",
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FBose%20SoundLink%20Revolve%2B%20Portable%20Bluetooth%20Speaker.webp?alt=media&token=6d32a82e-a2ae-4291-a313-1d1ca2a7ceeb"
            },
            new ProductDto
            {
                Name = "Sony WH-1000XM5 Wireless Noise-Canceling Headphones",
                Price = 349.99M,
                ProductQuantity = 40,
                Description =
                    "Experience world-class audio with the Sony WH-1000XM5 headphones. These wireless headphones offer industry-leading noise cancellation, high-resolution audio, and all-day comfort. With touch controls and voice assistant support, they are perfect for music lovers on the go.",
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FSony%20WH-1000XM5%20Wireless%20Noise-Canceling%20Headphones.jpg?alt=media&token=faa46f90-35b1-41cd-9969-bb165e78bdd6"
            },
            new ProductDto
            {
                Name = "Samsung QLED 8K Smart TV (75-inch)",
                Price = 2999.99M,
                ProductQuantity = 12,
                Description =
                    "Upgrade your home entertainment with the Samsung QLED 8K Smart TV. This 75-inch TV features stunning 8K resolution, Quantum Dot technology, and a range of smart features. Immerse yourself in breathtaking visuals and enjoy the latest in TV technology.",
                ImageStorageUrl = "https://firebasestorage.googleapis.com/v0/b/bytestore-dd3c8.appspot.com/o/ByteStore%2FSamsung%20QLED%208K%20Smart%20TV%20(75-inch).webp?alt=media&token=39908899-9968-49af-b35f-21304cb6cc63"
            }
        };
        foreach (var product in products)
        {
            var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

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