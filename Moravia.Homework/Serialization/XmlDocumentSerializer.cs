using Microsoft.Extensions.Logging;
using Moravia.Homework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Moravia.Homework.Serialization
{
  internal class XmlDocumentSerializer : DocumentSerializerBase
  {
    public XmlDocumentSerializer(Type documentType, ILogger logger) : base(documentType, logger) { }

    public override IDocument DeserializeDocument(string obj)
    {
      try
      {
        //_logger

        XmlSerializer serializer = new XmlSerializer(_documentType);

        using (StringReader sr = new StringReader(obj))
        {
          return (IDocument)serializer.Deserialize(sr);
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Error deserializing XML string into {_documentType}");
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

        return result.ToString();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Error serializing {_documentType} to XML");
        throw;
      }
    }
  }
}
