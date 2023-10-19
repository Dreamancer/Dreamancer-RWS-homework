using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Serialization.Factory
{
  public interface IDocumentSerializerFactory
  {
    public IDocumentSerializer GetDocumentSerializer(string serializerTypeName, string documentTypeName, ILogger logger);
  }
}
