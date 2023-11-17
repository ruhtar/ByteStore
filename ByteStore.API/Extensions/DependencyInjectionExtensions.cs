using ByteStore.Application.Services;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Application.Validator;
using ByteStore.Infrastructure.Cache;
using ByteStore.Infrastructure.Hasher;
using ByteStore.Infrastructure.Repository;
using ByteStore.Infrastructure.Repository.Interfaces;

namespace ByteStore.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ICacheConfigs, CacheConfigs>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IUserValidator, UserValidator>();
        services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddTransient<IShoppingCartService, ShoppingCartService>();

        return services;
    }
}