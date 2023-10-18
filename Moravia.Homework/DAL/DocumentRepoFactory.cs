using Moravia.Homework.Attributes;
using Moravia.Homework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Moravia.Homework.DAL
{
  internal static class DocumentRepoFactory
  {
    public static IDocumentRepo GetDocumentRepo(DocumentRepoSettings settings, ILogger logger)
    {
      if (settings == null)
      {
        throw new ArgumentNullException(nameof(settings));
      }

      if (String.IsNullOrWhiteSpace(settings.DocumentRepoTypeName))
      {
        throw new ArgumentNullException(nameof(settings.DocumentRepoTypeName));
      }

      Type repoType = Type.GetType(settings.DocumentRepoTypeName);

      if (repoType == null)
      {
        throw new ArgumentException(nameof(settings.DocumentRepoTypeName));
      }

      if (!repoType.GetInterfaces().Contains(typeof(IDocumentRepo)))
      {
        throw new ArgumentException($"Invalid Type {repoType}");
      }

      return (IDocumentRepo)Activator.CreateInstance(repoType, settings, logger);
    }
  }
}
