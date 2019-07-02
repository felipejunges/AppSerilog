using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;
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
                .Enrich.FromLogContext()
                .Enrich.WithProperty("App", "PocSerilog")
                .CreateLogger();

            using (LogContext.PushProperty("NumeroCliente", "1234"))
            {
                Log.Information("Teste 1");
                Log.Logger.Information("Teste 2");
            }
            Log.ForContext("NumeroCliente", 12345).Information("Teste 3");

            //Log.Error(new Exception("Could not find file"), "Não foi possível escrever no arquivo {NomeArquivo}", "nome_arquivo.txt");

            // var exampleUser = new User { Id = 1, Name = "Adam", Created = DateTime.Now };
            // Log.Information("Created {@User} on {Created}", exampleUser, DateTime.Now);

            // Log.Warning("Teste número do cliente: {NumeroCliente}", 10201111);
            // Log.Warning<int, string>("Teste número do cliente: {NumeroCliente} - Nome: {Nome}", 10201112, "Felipe");

            Console.WriteLine("Hello World!");

            Log.CloseAndFlush();
        }
    }
}