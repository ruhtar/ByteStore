using AuthenticationService.Authentication;
using AuthenticationService.Cache;
using AuthenticationService.Repository;
using AuthenticationService.Services;

namespace AuthenticationService.Configs.DI
{
    public class DependencyRegistration
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRedisProductRepository, RedisProductRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICacheConfigs, CacheConfigs>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRedisProductService, RedisProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
    }
}
