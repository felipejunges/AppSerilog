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
            Log.Error(new Exception("Teste de erro"), "Error!");

            Console.WriteLine("Hello World!");

            Log.CloseAndFlush();
        }
    }
}