using Moravia.Homework.Settings;
using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Moravia.Homework.DAL
{
  public class FileSystemDocumentRepo : DocumentRepoBase
  {
    /// <summary>
    /// Gets full path of the file
    /// </summary>
    public override string Location { get { return Path.GetFullPath(_location); } }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileSystemDocumentRepo"/> class.
    /// </summary>
    /// <param name="mode">IO mode of the initialized repo. Can be Read or Write.</param>
    /// <param name="location">Location of the source/target file</param>
    /// <param name="logger">aA logger instance</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the file specified in the settings.Location does not exist, while the repository mode is set to DocumentRepoMode.Read.
    /// </exception>
    public FileSystemDocumentRepo(DocumentRepoMode mode, string? location, ILogger logger) : base(mode, location, logger)
    {
      if (!File.Exists(location) && Mode == DocumentRepoMode.Read)
        throw new ArgumentException($"No file at path {location} to read");
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="FileSystemDocumentRepo"/> class.
    /// </summary>
    /// <param name="settings">The settings for the file system document repository.</param>
    /// <param name="logger">A logger for logging error messages.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the file specified in the settings.Location does not exist, while the repository mode is set to DocumentRepoMode.Read.
    /// </exception>
    public FileSystemDocumentRepo(DocumentRepoSettings settings, ILogger logger) : base(settings, logger)
    {
      if (!File.Exists(settings.Location) && Mode == DocumentRepoMode.Read)
        throw new ArgumentException($"No file at path {settings.Location} to read");
    }

    public async override Task<string> ReadInputFileAsync()
    {
      if (Mode == DocumentRepoMode.Write)
        throw new InvalidOperationException($"Only possible in 'Read' mode");

      _logger.Information($"Reading content from source file at '{Location}'");

      try
      {
        using (var reader = new StreamReader(Location, new FileStreamOptions { Mode = FileMode.Open }))
        {
          return await reader.ReadToEndAsync();
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error reading content from source file");
        throw;
      }
    }

    public async override Task<string> WriteToOutputFileAsync(string content)
    {
      if (Mode == DocumentRepoMode.Read)
        throw new InvalidOperationException($"Only possible in 'Write' mode");

      _logger.Information($"Writing content to target file at '{Location}'");

      try
      {
        using (var writer = new StreamWriter(Location, new FileStreamOptions { Mode = FileMode.Create, Access = FileAccess.Write }))
        {
          await writer.WriteAsync(content);
        }

        return Location;
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error writing content to target file");
        throw;
      }
    }
  }
}
