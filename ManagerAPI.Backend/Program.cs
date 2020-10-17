using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ManagerAPI.Backend
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHost(args).Run();
        }

        private static IWebHost CreateWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
.UseKestrel()
.UseContentRoot(Directory.GetCurrentDirectory())
.UseIISIntegration()
.UseStartup<Startup>()
.Build();
        }
    }
}