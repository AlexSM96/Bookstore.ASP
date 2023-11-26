using Microsoft.Extensions.Configuration;

namespace Bookstore.Application.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetValueFromConfig(this IConfiguration configuration, string sectionKey, string key)
        {
            return configuration
                .GetRequiredSection(sectionKey)
                .GetChildren().FirstOrDefault(x => x.Key == key).Value;
        }
    }
}
