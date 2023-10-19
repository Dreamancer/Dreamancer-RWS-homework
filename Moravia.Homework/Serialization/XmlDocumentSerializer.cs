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
  internal class XmlDocumentSerializer : DocumentSerializerBase
  {
    public XmlDocumentSerializer(Type documentType, ILogger logger) : base(documentType, logger) { }

    public override IDocument DeserializeDocument(string obj)
    {
      try
      {
        _logger.Debug($"Input xml:\n{obj}");

        XDocument xdoc = XDocument.Parse(obj);
        xdoc.Root.Name = DocumentType.Name;

        using (StringReader sr = new StringReader(xdoc.ToString()))
        {
          XmlSerializer serializer = new XmlSerializer(DocumentType);
          return (IDocument)serializer.Deserialize(sr);
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error deserializing xml into {DocumentType}");
        throw;
      }
    }

    public override string SerializeDocument(IDocument obj)
    {
      try
      {
        XmlSerializer serializer = new XmlSerializer(DocumentType);

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
        _logger.Error(ex, $"Error serializing {DocumentType} to xml");
        throw;
      }
    }
  }
}
