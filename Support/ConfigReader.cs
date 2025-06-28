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

        public static string Get(string key) => config[key];
        public static string GetSection(string section, string key) => config.GetSection(section)[key];

    }
}
