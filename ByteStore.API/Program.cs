using ByteStore.API.Extensions;
using ByteStore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services
                .AddMemoryCache() //Adding In-Memory cache.
                .AddStackExchangeRedisCache(o => //docker run -d -p 6379:6379 --name redis redis.
                {
                    o.InstanceName = "instance";
                    o.Configuration = Environment.GetEnvironmentVariable("REDIS_LOCALHOST");
                })
                .AddSwaggerGen() //SWAGGER GENERATOR
                .AddDependencies() //DEPENDENCY INJECTION CONFIGURATION
                .AddJWTConfiguration() //JWT CONFIGURATION
                .AddDbContext<AppDbContext>(options => options.UseMySql(
                    Environment.GetEnvironmentVariable("CONNECTION_STRING"),
                    ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("CONNECTION_STRING")),
                    x => x.MigrationsAssembly("ByteStore.Infrastructure")))
                .AddAuthorization()
                .AddCors();

            var app = builder.Build();

            app.UseCors(corsPolicyBuilder =>
            {
                corsPolicyBuilder
                .WithOrigins("http://localhost:4200")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseCors("AllowSpecificOrigin");


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpLogging();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}