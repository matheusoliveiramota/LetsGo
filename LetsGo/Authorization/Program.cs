using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostUrl = "http://0.0.0.0:6003/";

            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseUrls(hostUrl)
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .UseApplicationInsights()
            //    .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(hostUrl)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
