using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moravia.Homework.Settings;
using Serilog;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;
namespace Moravia.Homework
{
  class Program
  {
    private static ILogger? _logger;
    static async Task Main(string[] args)
    {
      try
      {
        //setup logger 
        _logger = ConfigureLogger();

        string? alternativeConfig = GetAlternativeConfigPath(args);

        IConfiguration configuration =
          alternativeConfig == null
          ? new ConfigurationBuilder()
           .SetBasePath(Environment.CurrentDirectory)
           .AddJsonFile("appsettings.json", optional: false)
           .Build()
          : new ConfigurationBuilder()
           .AddJsonFile(alternativeConfig, optional: false)
           .Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSerilog(_logger);
        ConfigureServices(serviceCollection, configuration);

        await serviceCollection.BuildServiceProvider().GetService<DocumentConvertorApp>().ExecuteDocumentConversionAsync();
      }
      catch (Exception ex)
      {
        if (_logger != null)
        {
          _logger.Fatal(ex, "Error running program");
        }
        else
        {
          Console.WriteLine("Error running program");
          Console.Error.WriteLine(ex);
        }
        throw;
      }
    }
    private static void ConfigureServices(ServiceCollection serviceCollection, IConfiguration configuration)
    {
      serviceCollection
        .AddTransient<DocumentConvertorApp>()
        .AddOptions<ConvertorAppSettings>().Bind(configuration.GetSection("ConvertorAppSettings"));
    }

    private static ILogger ConfigureLogger()
    {
      IConfiguration loggerConfig;
      if (File.Exists(Path.Combine(Environment.CurrentDirectory, "logsettings.json")))
      {
        loggerConfig =
        new ConfigurationBuilder()
         .SetBasePath(Environment.CurrentDirectory)
         .AddJsonFile("logsettings.json", optional: false)
         .Build();

        return new LoggerConfiguration()
              .ReadFrom.Configuration(loggerConfig)
              .CreateLogger();
      }
      else
      {
        return new LoggerConfiguration()
          .MinimumLevel.Is(Serilog.Events.LogEventLevel.Debug)
          .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
          .WriteTo.File("./Logs/DocumentConvertorAppLog.log")
          .CreateLogger();
      }
    }

    private static string? GetAlternativeConfigPath(string[] args)
    {
      string? alternativeConfig = null;
      if (args.Length == 1)
      {
        if (File.Exists(args[0]))
        {
          alternativeConfig = args[0];
        }
        else
        {
          throw new ArgumentException($"Invalid alternative config file path {args[0]}");
        }
      }

      return alternativeConfig;
    }
  }
}