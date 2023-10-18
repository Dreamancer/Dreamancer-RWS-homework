using Microsoft.Extensions.Logging;
using Moravia.Homework.Attributes;
using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Serialization
{
  public static class DocumentSerializerFactory
  {
    public static IDocumentSerializer GetDocumentSerializer(Type serializerType, Type documentType, ILogger logger)
    {
      if (!serializerType.IsSubclassOf(typeof(IDocumentSerializer)))
      {
        throw new ArgumentException($"Invalid serializer type to create: '{serializerType}'");
      }

      return (IDocumentSerializer)Activator.CreateInstance(serializerType, documentType, logger);
    }
  }
}
