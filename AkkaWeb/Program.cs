using System;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace AkkaWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread.Sleep(2000);

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
