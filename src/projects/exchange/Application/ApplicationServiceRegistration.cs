using Microsoft.Extensions.DependencyInjection;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using System.Reflection;
using Application.Services.Repositories;
using Application.Features.Shares.Rules;
using Application.Features.Users.Rules;
namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ShareBusinessRules>();
            services.AddScoped<UserBusinessRules>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


            return services;

        }
    }
}