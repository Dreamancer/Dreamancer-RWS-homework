using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moravia.Homework.Settings;
using Serilog;

namespace Moravia.Homework
{
  class Program
  {
    static async Task Main(string[] args)
    {
      string? alternateConfig = GetAlternateConfigPath(args);

      IConfiguration configuration =
        alternateConfig == null
        ? new ConfigurationBuilder()
         .SetBasePath(Environment.CurrentDirectory)
         .AddJsonFile("appsettings.json", optional: false)
         .Build()
        : new ConfigurationBuilder()
         .AddJsonFile(alternateConfig, optional: false)
         .Build();

      var serviceCollection = new ServiceCollection();
      ConfigureLogger(serviceCollection);
      ConfigureServices(serviceCollection, configuration);

      await serviceCollection.BuildServiceProvider().GetService<DocumentConvertorApp>().ExecuteDocumentConversionAsync();
    }
    private static void ConfigureServices(ServiceCollection serviceCollection, IConfiguration configuration)
    {
      serviceCollection
        .AddTransient<DocumentConvertorApp>()
        .AddOptions<ConvertorAppSettings>().Bind(configuration.GetSection("ConvertorAppSettings"));
    }

    private static void ConfigureLogger(ServiceCollection serviceCollection)
    {
      IConfiguration loggerConfig =
        new ConfigurationBuilder()
         .SetBasePath(Environment.CurrentDirectory)
         .AddJsonFile("logsettings.json", optional: false)
         .Build();

      ILogger logger = new LoggerConfiguration()
            .ReadFrom.Configuration(loggerConfig)
            .CreateLogger();

       serviceCollection.AddSerilog(logger);
      //serviceCollection
      //  .AddLogging(loggerBuilder =>
      //  {
      //    loggerBuilder.AddSerilog(logger);
      //  });
    }

    private static string? GetAlternateConfigPath(string[] args)
    {
      string? alternateConfig = null;
      if (args.Length == 2)
      {
        if (args[0].ToLower() == "-c")
        {
          if (File.Exists(args[1]))
          {
            alternateConfig = args[1];
          }
          else
          {
            Console.WriteLine($"invalid config file path {args[1]}");
          }
        }
        else
        {
          Console.WriteLine($"ignoring invalid command {args[0]}");
        }
      }

      return alternateConfig;
    }
  }
}