using ByteStore.API.Extensions;
using ByteStore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.API;

public class Program
{
    public static void Main(string[] args)
    {
        DotNetEnv.Env.Load();

        var host = Environment.GetEnvironmentVariable("HOST");
        var port = Environment.GetEnvironmentVariable("PORT");
        var database = Environment.GetEnvironmentVariable("DATABASE");
        var user = Environment.GetEnvironmentVariable("USER");
        var password = Environment.GetEnvironmentVariable("PASSWORD");

        var connectionString = $"Server={host};Port={port};Database={database};User={user};Password={password}";

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services
            .AddMemoryCache() //Adding In-Memory cache.
            .AddStackExchangeRedisCache(o => //docker run -d -p 6379:6379 --name redis redis.
            {
                o.InstanceName = "instance";
                o.Configuration = Environment.GetEnvironmentVariable("REDIS_LOCALHOST");
            })
            .AddHostedService<Migrator>() //Migrates database
            .AddSwaggerGen() //SWAGGER GENERATOR
            .AddDependencies() //DEPENDENCY INJECTION CONFIGURATION
            .AddJWTConfiguration() //JWT CONFIGURATION
            .AddDbContext<AppDbContext>(options => options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                x => x.MigrationsAssembly("ByteStore.Infrastructure")))
            .AddAuthorization()
            .AddCors();

        var app = builder.Build();

        app.UseCors(corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .WithOrigins()
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