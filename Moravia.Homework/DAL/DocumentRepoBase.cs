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
  public abstract class DocumentRepoBase : IDocumentRepo
  {
    protected readonly ILogger _logger;
    protected readonly string _location;

    public DocumentRepoMode Mode { get; protected set; }

    public virtual string Location {get { return _location;}}



    /// <summary>
    /// Base class constructor using basic arguments
    /// </summary>
    /// <param name="mode">I/O mode of the repo</param>
    /// <param name="location">location of source/target file.</param>
    public DocumentRepoBase(DocumentRepoMode mode, string? location, ILogger logger)
    {
      if (String.IsNullOrWhiteSpace(location))
      {
        throw new ArgumentNullException(nameof(location));
      }

      Mode = mode;

      _location = location;
      _logger = logger;
    }

    public DocumentRepoBase(DocumentRepoSettings settings, ILogger logger)
    {
      if (settings == null)
      {
        throw new ArgumentNullException(nameof(settings));
      }

      if (String.IsNullOrWhiteSpace(settings.Location))
      {
        throw new ArgumentNullException(nameof(settings.Location));
      }

      Mode = settings.Mode;
      _location = settings.Location;
      _logger = logger;
    }

    public abstract Task<string> ReadInputFileAsync();

    public abstract Task<string> WriteToOutputFileAsync(string content);


  }
}
