using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AuthenticationService.Infrastructure;
using AuthenticationService.Shared.Configs.Authentication;
using AuthenticationService.Shared.Configs.DI;

namespace AuthenticationService
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

            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                Array.Empty<string>()
                }
                });
            });

            //DEPENDENCY INJECTION CONFIGURATION
            DependencyRegistration.RegisterDependencies(builder.Services);

            //JWT CONFIGURATION
            AuthenticationConfiguration.Configure(builder.Services);

            builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"), ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("CONNECTION_STRING"))));

            builder.Services.AddAuthorization();

            var app = builder.Build();

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