using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Asp.Net.Core.Cognito
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot config;

            string baseDir = Directory.GetCurrentDirectory();

            config = new ConfigurationBuilder()
            .SetBasePath(baseDir)
            .AddJsonFile("config" + Path.DirectorySeparatorChar + "hosting.json", false)
            .Build();

            string url = config.GetValue<string>("server.urls");

            CreateWebHostBuilder(args)
                .UseUrls(url)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((hostingContext, config) =>
                {
                     config.SetBasePath(Directory.GetCurrentDirectory());
                     config.AddJsonFile(
                       "config" + Path.DirectorySeparatorChar + "cognito_config.json", optional: false, reloadOnChange: false);
                     config.AddCommandLine(args);
                })
                .UseStartup<Startup>();
    }
}
