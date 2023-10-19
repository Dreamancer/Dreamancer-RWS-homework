using Moravia.Homework.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.DAL.Factory
{
  public interface IDocumentRepoFactory
  {
    public IDocumentRepo GetDocumentRepo(DocumentRepoSettings settings, ILogger logger);
  }
}
