using System.Runtime.CompilerServices;
using JPN.Core.Utils;
using JPN.Customer.API.Services;
using JPN.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JPN.Customer.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegisteredCustomerIntegrationHandler>();
        }
    }
}