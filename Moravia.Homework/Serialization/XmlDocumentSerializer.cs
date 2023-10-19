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
  internal class XmlDocumentSerializer : DocumentSerializerBase
  {
    public XmlDocumentSerializer(Type documentType, ILogger logger) : base(documentType, logger) { }

    public override IDocument DeserializeDocument(string obj)
    {
      try
      {
        _logger.Debug($"Input xml string:\n{obj}");

        XDocument xdoc = XDocument.Parse(obj);
        xdoc.Root.Name = _documentType.Name;

        using (StringReader sr = new StringReader(xdoc.ToString()))
        {
          XmlSerializer serializer = new XmlSerializer(_documentType);
          return (IDocument)serializer.Deserialize(sr);
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error deserializing xml string into {_documentType}");
        throw;
      }
    }

    public override string SerializeDocument(IDocument obj)
    {
      try
      {
        XmlSerializer serializer = new XmlSerializer(_documentType);

        StringBuilder result = new StringBuilder();
        using (StringWriter sw = new StringWriter(result))
        {
          serializer.Serialize(sw, obj);
        }

        _logger.Debug($"Serialized xml string:\n{result}");

        return result.ToString();
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error serializing {_documentType} to xml");
        throw;
      }
    }
  }
}
