using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Reflection;
using Moravia.Homework.Models;

namespace Moravia.Homework.Serialization.Factory
{
  /// <summary>
  /// IDocumentSerializerFactory implementation
  /// </summary>
  public class DocumentSerializerFactory : IDocumentSerializerFactory
  {
    public IDocumentSerializer GetDocumentSerializer(string serializerTypeName, string documentTypeName, ILogger logger)
    {
      if (string.IsNullOrEmpty(serializerTypeName))
        throw new ArgumentNullException(nameof(serializerTypeName));

      if (string.IsNullOrEmpty(documentTypeName))
        throw new ArgumentNullException(nameof(documentTypeName));

      Type documentType = Type.GetType(documentTypeName);

      if (documentType == null)
        throw new ArgumentException($"Invalid documentTypeName value {documentTypeName}");


      if (documentType.GetInterface(typeof(IDocument).Name) == null)
        throw new ArgumentException($"Invalid Type: {documentTypeName} does not implement IDocument");


      string serializerGenericTypeName = $"{serializerTypeName}`1[{documentTypeName}]";

      Type serializerType = Type.GetType(serializerGenericTypeName);

      if (serializerType == null)
        throw new ArgumentException($"Invalid serializerTypeName {serializerTypeName}");

      if (serializerType.GetInterface(typeof(IDocumentSerializer).Name) == null)
        throw new ArgumentException($"Invalid serializer type to create: '{serializerTypeName}'");

      return (IDocumentSerializer)Activator.CreateInstance(serializerType, logger);

    }
  }
}
