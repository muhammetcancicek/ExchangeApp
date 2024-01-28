using Microsoft.Extensions.DependencyInjection;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using System.Reflection;
using Application.Features.Shares.Rules;
using Application.Features.Users.Rules;
using Application.Features.Portfolios.Rules;
using Application.Services.BusinessObjects;
using Application.BusinessObjects;
using Application.Features.Trades.Rules;
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
            services.AddScoped<PortfolioBusinessRules>();
            services.AddScoped<TradeBusinessRules>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient<ITradeBO, TradeBO>();


            return services;

        }
    }
}