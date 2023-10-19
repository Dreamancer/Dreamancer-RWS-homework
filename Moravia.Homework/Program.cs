using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moravia.Homework.Settings;
using Serilog;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;
using Moravia.Homework.DAL.Factory;
using Moravia.Homework.Serialization.Factory;

namespace Moravia.Homework
{
  class Program
  {
    private static ILogger? _logger;

    static async Task Main(string[] args)
    {
      try
      { 
        _logger = ConfigureLogger();
     
        string? alternativeConfig = GetAlternativeConfigPath(args);

        IConfiguration configuration = GetConfiguration(alternativeConfig);

        var serviceCollection = ConfigureServices(configuration);

        //run the application
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

    /// <summary>
    /// Sets up and returns collection with the needed services
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static ServiceCollection ConfigureServices(IConfiguration configuration)
    {
      ServiceCollection serviceCollection = new ServiceCollection();
      serviceCollection
        .AddSerilog(_logger)
        .AddSingleton<IDocumentRepoFactory, DocumentRepoFactory>()
        .AddSingleton<IDocumentSerializerFactory, DocumentSerializerFactory>()
        .AddTransient<DocumentConvertorApp>()
        .AddOptions<ConvertorAppSettings>()
        .Bind(configuration.GetSection("ConvertorAppSettings"));

      return serviceCollection;
    }

    /// <summary>
    /// Sets up logging from logsettings.json. If no such file is found, a default configuration is implemented
    /// </summary>
    /// <returns>ILogger instance</returns>
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

    /// <summary>
    /// Gets alternative config path from args, if there is any, otherwise returns null
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
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

    /// <summary>
    /// Builds and returns application configuration from a json file
    /// </summary>
    /// <param name="alternativeConfigPath">Path to an alternative settings file to be used instead of default appsettings.json</param>
    /// <returns>IConfiguration instance</returns>
    private static IConfiguration GetConfiguration(string? alternativeConfigPath)
    {
      return
          alternativeConfigPath == null
          ? new ConfigurationBuilder()
           .SetBasePath(Environment.CurrentDirectory)
           .AddJsonFile("appsettings.json", optional: false)
           .Build()
          : new ConfigurationBuilder()
           .AddJsonFile(alternativeConfigPath, optional: false)
           .Build();
    }
  }
}