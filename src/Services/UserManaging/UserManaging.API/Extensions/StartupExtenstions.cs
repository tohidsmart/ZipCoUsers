using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManaging.Infrastructure;
using UserManaging.CQRS.Commands.Create;
using UserManaging.CQRS.Queries;
using UserManaging.Domain.Repository;
using UserManaging.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;


namespace UserManaging.API.Extensions
{
    public static class StartupExtenstions
    {

        public static void ResolveDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddMediatR(typeof(CreateUserCommandHandler));
            services.AddDbContext<UserManagingContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"),
                    actions => actions.MigrationsAssembly("UserManaging.Infrastructure"));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUserAccountQueries, UserAccountQueries>();
        }

        public static void ConfigureExternalDependencies(this IServiceCollection services)
        {

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "ZIPCO API", Version = "v1" });

            });

            services.AddAutoMapper(typeof(CreateUserCommand).Assembly);
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommand>())
                .AddNewtonsoftJson(option =>
                {
                    {
                        option.SerializerSettings.Formatting = Formatting.Indented;
                    }
                });
        }

        public static void ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("", builder =>
                {
                    builder.SetIsOriginAllowed((host) => true).
                   WithOrigins().
                   AllowCredentials()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
                });
            });
        }


        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware.ErrorHandlerMiddleware>();
        }
    }
}
