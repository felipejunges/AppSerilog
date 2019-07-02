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

            // Log.Information("Teste");
            // Log.Error(new Exception("Teste de erro"), "Error!");

            // var exampleUser = new User { Id = 1, Name = "Adam", Created = DateTime.Now };
            // Log.Information("Created {@User} on {Created}", exampleUser, DateTime.Now);

            Log.Warning("Teste número do cliente: {NumeroCliente}", 10201111);

            Console.WriteLine("Hello World!");

            Log.CloseAndFlush();
        }
    }
}