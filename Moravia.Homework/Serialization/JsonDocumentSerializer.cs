using Moravia.Homework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Serilog;

namespace Moravia.Homework.Serialization
{
  /// <summary>
  /// JSON document serializer implementation
  /// </summary>
  public class JsonDocumentSerializer : DocumentSerializerBase
  {
    public JsonDocumentSerializer(Type documentType, ILogger logger) : base(documentType, logger)
    {
    }


    public override IDocument DeserializeDocument(string obj)
    {
      try
      {
        _logger.Debug($"Input json:\n{obj}");

        return (IDocument)JsonConvert.DeserializeObject(obj, DocumentType);
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error deserializing json into {DocumentType}");
        throw;
      }
    }

    public override string SerializeDocument(IDocument obj)
    {
      try
      {
        string jsonString = JsonConvert.SerializeObject(obj);

        _logger.Debug($"Serialized json:\n{jsonString}");

        return jsonString;
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error serializing {DocumentType} to json");
        throw;
      }
    }
  }
}
