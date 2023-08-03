using AuthenticationService.Application.Services;
using AuthenticationService.Configs.Cache;
using AuthenticationService.Infrastructure.Hasher;
using AuthenticationService.Infrastructure.Repository;
using AuthenticationService.Shared.Validator;

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
            services.AddScoped<IUserValidator, UserValidator>();
        }
    }
}
