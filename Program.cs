using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iNOBStudios.Data;
using iNOBStudios.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace iNOBStudios
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var menuRepository = services.GetRequiredService<IMenuRepository>();
                    var userRepository = services.GetRequiredService<IUserRepository>();
                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
                    var task = SeedData.Seed(services, context, config, userRepository, menuRepository);
                    task.Wait();
                }
                catch (Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                    return 1;
                }
            }
            host.Run();
            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
