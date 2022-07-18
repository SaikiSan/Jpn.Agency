using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JPN.EmailService.Configuration
{
    public static class EmailDependencyInjection
    {
        public static IServiceCollection AddEmail(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton<IEmailService, EmailService>();

            return service;
        }
    }
}
