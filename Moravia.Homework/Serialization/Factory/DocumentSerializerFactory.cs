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

      string serializerGenericTypeName = $"{serializerTypeName}`1[{documentTypeName}]";

      try
      {
        Type serializerType = Type.GetType(serializerGenericTypeName);

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
