using Moravia.Homework.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.DAL.Factory
{
  /// <summary>
  /// Represents a factory for creating instances of objects that implement the IDocumentRepo interface.
  /// </summary>
  public interface IDocumentRepoFactory
  {
    /// <summary>
    /// Gets an instance of an object that implements the IDocumentRepo interface based on the provided settings.
    /// </summary>
    /// <param name="settings">The settings for creating the document repository instance.</param>
    /// <param name="logger">A logger intance.</param>
    /// <returns>An instance of an object that implements the IDocumentRepo interface.</returns>
    public IDocumentRepo GetDocumentRepo(DocumentRepoSettings settings, ILogger logger);
  }
}
