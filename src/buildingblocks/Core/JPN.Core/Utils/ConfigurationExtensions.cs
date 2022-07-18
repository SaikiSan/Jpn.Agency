using System.Collections.Generic;
using System.Linq;
using JPN.EmailService.Extension;
using Microsoft.Extensions.Configuration;

namespace JPN.Core.Utils
{
    public static class ConfigurationExtensions
    {
        public static string GetMessageQueueConnection(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection("MessageQueueConnection")?[name];
        }

        public static EmailSettings GetEmailSettings(this IConfiguration configuration)
        {
            return configuration?.GetSection("EmailSettings").GetChildren().OfType<EmailSettings>().First();
        }
    }
}