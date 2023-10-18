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
    public static IDocumentRepo GetDocumentRepo(DocumentRepoSettings settings)
    {
      if (settings == null)
      {
        throw new ArgumentNullException(nameof(settings));
      }

      if (settings.RepoType == null)
      {
        throw new ArgumentNullException(nameof(settings.RepoType));
      }

      Type? repoType = settings.RepoType.GetTypeToCreateAttribute()?.TypeToCreate;

      if (repoType == null)
      {
        throw new ArgumentException($"Invalid enum value {settings.RepoType}");
      }

      if(!repoType.IsSubclassOf(typeof(IDocumentRepo))) 
      {
        throw new ArgumentException($"Invalid Type {repoType}");
      }

      return (IDocumentRepo)Activator.CreateInstance(repoType, settings);
    }
  }
}
