using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace HbCampaignModule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile(
            //        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            //        optional: true)
            //    .Build();

            //Log.Logger = new LoggerConfiguration()
            //   .Enrich.FromLogContext()
            //   .WriteTo.Debug()
            //   .WriteTo.Console()
            //  // .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
            //   .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            //   .CreateLogger();
            CreateHostBuilder(args).Build().Run();
            //try
            //{
               
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, "Startup error");
            //}
        }

        //private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        //{
        //    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
        //    {
        //        AutoRegisterTemplate = true,
        //        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        //    };
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        //     .ConfigureAppConfiguration(configuration =>
        //     {
        //         configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        //         configuration.AddJsonFile(
        //             $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        //             optional: true);
        //     })
        //.UseSerilog();
    }
}
