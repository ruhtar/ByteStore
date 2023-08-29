using Ecommerce.Application.Services;
using Ecommerce.Infrastructure.Cache;
using Ecommerce.Infrastructure.Hasher;
using Ecommerce.Infrastructure.Repository;
using Ecommerce.Shared.Validator;
using Ecommerce.Application.Services;
using Ecommerce.Infrastructure.Repository;

namespace Ecommerce.Shared.Configs.DI
{
    public class DependencyRegistration
    {
        public static void RegisterDependencies(IServiceCollection services)
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
        }
    }
}
