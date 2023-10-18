using Microsoft.Extensions.Logging;
using Moravia.Homework.Attributes;
using Moravia.Homework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

      if (settings.DocumentRepoType == null)
      {
        throw new ArgumentNullException(nameof(settings.DocumentRepoType));
      }

      Type repoType = settings.DocumentRepoType;

      if(!repoType.IsSubclassOf(typeof(IDocumentRepo))) 
      {
        throw new ArgumentException($"Invalid Type {repoType}");
      }

      return (IDocumentRepo)Activator.CreateInstance(repoType, settings, logger);
    }
  }
}
