using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollProject1.Support
{
    public static class ConfigReader
    {
        private static IConfigurationRoot config;

        static ConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            config = builder.Build();
        }

        public static string Get(string key)
        {
            var value = config[key];
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"Configuration value for key '{key}' is missing or null.");

            return value;
        }

        public static string GetSection(string section, string key)
        {
            var sectionConfig = config.GetSection(section);
            if (!sectionConfig.Exists())
                throw new InvalidOperationException($"Configuration section '{section}' does not exist.");

            var value = sectionConfig[key];
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"Configuration value for section '{section}' and key '{key}' is missing or null.");

            return value;
        }
    }
}
