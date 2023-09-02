using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Infrastructure.Cache;
using Ecommerce.Infrastructure.Hasher;
using Ecommerce.Infrastructure.Repository;
using Ecommerce.Infrastructure.Repository.Interfaces;
using Ecommerce.Shared.Validator;

namespace Ecommerce.Shared.Configs
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICacheConfigs, CacheConfigs>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserValidator, UserValidator>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();

            return services;
        }
    }
}
