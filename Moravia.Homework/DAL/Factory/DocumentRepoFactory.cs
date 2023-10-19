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
    public IDocumentRepo GetDocumentRepo(DocumentRepoSettings settings, ILogger logger)
    {
      if (settings is null)
        throw new ArgumentNullException(nameof(settings), "Parameter 'settings' cannot be null.");

      if (string.IsNullOrWhiteSpace(settings.DocumentRepoTypeName))
        throw new ArgumentNullException(nameof(settings.DocumentRepoTypeName), "Parameter 'settings.DocumentRepoTypeName' cannot be null or empty.");

      Type repoType = Type.GetType(settings.DocumentRepoTypeName);

      if (repoType == null)
        throw new ArgumentException(nameof(settings.DocumentRepoTypeName), "Invalid DocumentRepoTypeName");

      if (!repoType.GetInterfaces().Contains(typeof(IDocumentRepo)))
        throw new ArgumentException($"Invalid Type: {repoType} does not implement IDocumentRepo.");

      try
      {
        return (IDocumentRepo)Activator.CreateInstance(repoType, settings, logger);
      }
      catch (Exception ex)
      {
        logger.Error($"Error creating an instance of '{settings.DocumentRepoTypeName}': {ex.Message}");
        throw;
      }
    }
  }
}
