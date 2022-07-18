using FluentValidation.Results;
using JPN.Core.Mediator;
using JPN.Customer.API.Application.Commands;
using JPN.Customer.API.Application.Events;
using JPN.Customer.API.Data;
using JPN.Customer.API.Data.Repositorys;
using JPN.Customer.API.Models.Interfaces;
using JPN.Customer.API.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JPN.Customer.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<CustomerRegisterCommand, ValidationResult>, CustomerCommandHandler>();

            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomerContext>();
        }
    }
}
