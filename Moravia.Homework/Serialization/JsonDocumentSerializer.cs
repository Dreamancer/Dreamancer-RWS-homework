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
  public class JsonDocumentSerializer<T> : DocumentSerializerBase<T> where T : IDocument
  {
    public JsonDocumentSerializer(ILogger logger) : base(logger)
    {
    }


    public override IDocument DeserializeDocument(string obj)
    {
      try
      {
        _logger.Debug($"Input json:\n{obj}");

        return (IDocument)JsonConvert.DeserializeObject(obj, typeof(T));
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error deserializing json into {typeof(T)}");
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
        _logger.Error(ex, $"Error serializing {typeof(T)} to json");
        throw;
      }
    }
  }
}
