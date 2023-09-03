using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ecommerce.Infrastructure;
using Ecommerce.Shared.Configs;
using Ecommerce.Shared.Extensions;

namespace Ecommerce
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
            //Adding In-Memory cache.
            builder.Services.AddMemoryCache();
            //docker run -d -p 6379:6379 --name redis redis.
            builder.Services.AddStackExchangeRedisCache(o =>
            {
                o.InstanceName = "instance";
                o.Configuration = Environment.GetEnvironmentVariable("REDIS_LOCALHOST");
            });

            //SWAGGER GENERATOR
            builder.Services.AddSwaggerGen();

            //DEPENDENCY INJECTION CONFIGURATION
            builder.Services.AddDependencies();

            //JWT CONFIGURATION
            builder.Services.AddJWTConfiguration();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"), ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("CONNECTION_STRING"))));

            builder.Services.AddAuthorization();

            builder.Services.AddCors();

            var app = builder.Build();

            app.UseCors(builder =>
            {
                builder
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