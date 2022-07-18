using System;
using Microsoft.Extensions.DependencyInjection;

namespace JPN.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection service, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

            service.AddSingleton<IMessageBus>(new MessageBus(connection));

            return service;
        }
    }
}