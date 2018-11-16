using System;
using Microsoft.Extensions.Configuration;

namespace AffichageDynamique.Helpers
{
    public class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!String.IsNullOrWhiteSpace(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets)
            {
                builder.AddUserSecrets<ConfigurationHelper>(); // requires adding Microsoft.Extensions.Configuration.UserSecrets from NuGet.
            }

            return builder.Build();
        }
    }
}
