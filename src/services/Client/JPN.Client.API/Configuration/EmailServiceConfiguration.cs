using JPN.Core.Utils;
using JPN.Customer.API.Services;
using JPN.EmailService.Configuration;
using JPN.EmailService.Extension;
using JPN.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JPN.Customer.API.Configuration
{
    public static class EmailServiceConfiguration
    {
        public static void AddEmailService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddEmail(configuration);

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        }
    }
}
