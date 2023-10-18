using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moravia.Homework.Settings;
//using Microsoft.Extensions.Logging;
using Serilog;

namespace Moravia.Homework
{
  class Program
  {
    static async Task Main(string[] args)
    {
      IConfiguration configuration = new ConfigurationBuilder()
         .SetBasePath(Environment.CurrentDirectory)
         .AddJsonFile("appsettings.json", optional: false)
         .Build();

      var serviceCollection = new ServiceCollection();
      ConfigureServices(serviceCollection, configuration);

      await serviceCollection.BuildServiceProvider().GetService<DocumentConvertorApp>().ExecuteDocumentConversionAsync();
    }
    private static void ConfigureServices(ServiceCollection serviceCollection, IConfiguration configuration)
    {
      Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
      serviceCollection
        //  .AddLogging(loggerBuilder =>
        //{


        //  //loggerConf.AddConfiguration(configuration.GetSection("LoggingSettings"));
        //  // configure.AddConsole();
        //  loggerBuilder.AddSerilog(logger, dispose: true);
        //})
        .AddTransient<DocumentConvertorApp>()
        .AddOptions<ConvertorAppSettings>().Bind(configuration.GetSection("ConvertorAppSettings"));
    }
  }
}