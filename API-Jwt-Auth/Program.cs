﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using API_Jwt_Auth.Data;

namespace API_Jwt_Auth {
    public class Program {
        public static void Main(string[] args) {

            var host = CreateWebHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();
        }

        private static void RunSeeding(IWebHost host) {

            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope()) {
                var seeder = scope.ServiceProvider.GetService<SeederData>();
                seeder.Seed();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .UseStartup<Startup>();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder) {

            //Removing the default configurations
            builder.Sources.Clear();
            // Add my own custom config file
            builder.AddJsonFile("config.json", false, true)
                   .AddEnvironmentVariables();

        }

    }
}
