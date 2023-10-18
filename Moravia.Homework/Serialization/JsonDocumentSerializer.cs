using Moravia.Homework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Moravia.Homework.Serialization
{
  public class JsonDocumentSerializer : DocumentSerializerBase
  {
    public JsonDocumentSerializer(Type documentType) :base(documentType)
    {
    }

    public override IDocument DeserializeDocument(string obj)
    {
      return (IDocument)JsonConvert.DeserializeObject(obj, _documentType);
    }

    public override string SerializeDocument(IDocument obj)
    {
      return JsonConvert.SerializeObject(obj);
    }
  }
}
