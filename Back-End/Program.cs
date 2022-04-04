using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PopCorn.Web.Infra.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var db = "popcorn.db";
            //if (File.Exists(db))
            //    File.Delete(db);

            //using var context = new PopCornContext();
            //context.Database.EnsureCreated();
             CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
