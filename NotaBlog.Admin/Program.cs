using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NotaBlog.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var configFile = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configFile)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls(configuration.GetValue<string>("Urls"))
                .UseStartup<Startup>()
                .Build();
        }
            
    }
}
