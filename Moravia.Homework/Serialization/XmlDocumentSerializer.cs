using Moravia.Homework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Serilog;


namespace Moravia.Homework.Serialization
{
  /// <summary>
  /// XML document serializer implementation
  /// </summary>
  internal class XmlDocumentSerializer<T> : DocumentSerializerBase<T> where T: IDocument
  {
    public XmlDocumentSerializer(ILogger logger) : base(logger) { }

    public override IDocument DeserializeDocument(string obj)
    {
      try
      {
        _logger.Debug($"Input xml:\n{obj}");

        XDocument xdoc = XDocument.Parse(obj);
        //rename the root node to the Type name of the serialized class to prevent 'incorrect xml data' exception
        xdoc.Root.Name = typeof(T).Name;

        using (StringReader sr = new StringReader(xdoc.ToString()))
        {
          XmlSerializer serializer = new XmlSerializer(typeof(T));
          return (T)serializer.Deserialize(sr);
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error deserializing xml into {typeof(T)}");
        throw;
      }
    }

    public override string SerializeDocument(IDocument obj)
    {
      try
      {
        XmlSerializer serializer = new XmlSerializer(typeof(T));

        StringBuilder result = new StringBuilder();
        using (StringWriter sw = new StringWriter(result))
        {
          serializer.Serialize(sw, obj);
        }

        _logger.Debug($"Serialized xml:\n{result}");

        return result.ToString();
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error serializing {typeof(T)} to xml");
        throw;
      }
    }
  }
}
