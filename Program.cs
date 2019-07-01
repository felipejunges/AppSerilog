using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace AppSerilog
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            Log.Information("Teste");
            Log.Logger.Information("Teste2");
            Log.Logger.Warning("Teste3");

            Console.WriteLine("Hello World!");
        }
    }
}