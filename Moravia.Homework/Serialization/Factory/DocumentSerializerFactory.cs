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
    private readonly ILogger _logger;

    public DocumentSerializerFactory(ILogger logger)
    {
      _logger = logger;
    }

    public IDocumentSerializer GetDocumentSerializer(string serializerTypeName, string documentTypeName)
    {
      if (string.IsNullOrEmpty(serializerTypeName))
        throw new ArgumentNullException(nameof(serializerTypeName));

      if (string.IsNullOrEmpty(documentTypeName))
        throw new ArgumentNullException(nameof(documentTypeName));

      Type? modelType = Type.GetType($"Moravia.Homework.Models.{documentTypeName}", false);

      if (modelType == null)
        throw new ArgumentException($"Invalid document type name: {documentTypeName}");

      string serializerGenericTypeName = $"Moravia.Homework.Serialization.{serializerTypeName}`1[{modelType.FullName}]";

      Type? serializerType = Type.GetType(serializerGenericTypeName, false);

      if (serializerType == null)
        throw new ArgumentException($"Invalid serializer type name {serializerTypeName}");

      try
      {

        return (IDocumentSerializer)Activator.CreateInstance(serializerType, _logger);

      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error creating document serializer type '{serializerTypeName}' for '{documentTypeName}'");
        throw;
      }

    }
  }
}
