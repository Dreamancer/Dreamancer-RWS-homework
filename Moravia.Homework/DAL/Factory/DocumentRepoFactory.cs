using Moravia.Homework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Moravia.Homework.DAL.Factory
{
  /// <summary>
  /// A factory class for creating instances of objects that implement the IDocumentRepo interface.
  /// </summary>
  public class DocumentRepoFactory : IDocumentRepoFactory
  {
    private readonly ILogger _logger;
    public DocumentRepoFactory(ILogger logger)
    {
      _logger = logger;
    }

    /// <summary>
    /// Gets an instance of an object that implements the IDocumentRepo interface based on the provided settings.
    /// </summary>
    /// <param name="settings">The settings for creating the document repository instance.</param>
    /// <param name="logger">A logger instance</param>
    /// <returns>An instance of an object that implements the IDocumentRepo interface based on the provided settings.</returns>
    /// <exception cref="ArgumentNullException">Thrown when settings or settings.DocumentRepoTypeName is null.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the specified DocumentRepoTypeName is not a valid type or does not implement IDocumentRepo.
    /// </exception>
    public IDocumentRepo GetDocumentRepo(DocumentRepoSettings settings)
    {
      if (settings is null)
        throw new ArgumentNullException(nameof(settings), "Parameter 'settings' cannot be null.");

      if (string.IsNullOrWhiteSpace(settings.DocumentRepoTypeName))
        throw new ArgumentNullException(nameof(settings.DocumentRepoTypeName), "Parameter 'settings.DocumentRepoTypeName' cannot be null or empty.");

      try
      {
        Type repoType = Type.GetType(settings.DocumentRepoTypeName);

        return (IDocumentRepo)Activator.CreateInstance(repoType, settings, _logger);
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error creating an instance of '{settings.DocumentRepoTypeName}");
        throw;
      }
    }
  }
}
