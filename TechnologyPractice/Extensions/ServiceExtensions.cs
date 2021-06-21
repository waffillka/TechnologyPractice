using Contracts;
using Entities;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnologyPractice.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection service) =>
            service.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => 
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("TechnologyPractice")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureEmailService(this IServiceCollection services) =>
            services.AddScoped<IEmailService, EmailService.EmailService>();

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechnologyPractice", Version = "v1" });
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(x => 
            { 
                x.Password.RequireDigit = true; 
                x.Password.RequireLowercase = false; 
                x.Password.RequireUppercase = false; 
                x.Password.RequireNonAlphanumeric = false; 
                x.Password.RequiredLength = 10; 
                x.User.RequireUniqueEmail = true; 
            }); 
            
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services); 
            builder.AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();
        }
    }
}
