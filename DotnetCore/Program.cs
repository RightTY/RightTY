using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotnetCore
{
#pragma warning disable 1591
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    #region --設置讀取設定檔(Json)--
                            .ConfigureAppConfiguration((hostContext, config) =>
                            {
                                var env = hostContext.HostingEnvironment;
                                config.SetBasePath(Path.Combine(env.ContentRootPath, "System"))
                                      .AddJsonFile(path: "setting.json", optional: false, reloadOnChange: true)
                                      .AddJsonFile(path: $"settings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                            }) 
                    #endregion
                    .UseStartup<Startup>();
                });
    }
}
#pragma warning restore 1591
