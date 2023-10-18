using Moravia.Homework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Settings.Enum
{
  public enum ConvertFileType
  {
    [TypeToCreate(typeof(Moravia.Homework.Serialization.JsonDocumentSerializer))]
    JSON,
    [TypeToCreate(typeof(Moravia.Homework.Serialization.XmlDocumentSerializer))]
    XML
  }
}
