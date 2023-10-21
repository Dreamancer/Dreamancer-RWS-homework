using Moravia.Homework.Models;
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
    /// <summary>
    /// Returns IDocumentSerializer of a derived type according to parameters
    /// </summary>
    /// <param name="serializerTypeName">The type name of IDocumentSerializer derived type we want to create</param>
    /// <param name="documentTypeName">The type name of IDocument derived type we want to serialize with created serializer</param>
    /// <param name="logger">Serilog ILogger</param>
    /// <returns>IDocumentSerializer</returns>
    public IDocumentSerializer GetDocumentSerializer(string serializerTypeName, string documentTypeName, ILogger logger);
  }
}
