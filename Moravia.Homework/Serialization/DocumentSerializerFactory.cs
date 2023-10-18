using Moravia.Homework.Attributes;
using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Serialization
{
  public static class DocumentSerializerFactory
  {
    public static IDocumentSerializer GetDocumentSerializer(ConvertFileType fileType, Type documentType)
    {
      Type typeToCreate = fileType.GetTypeToCreateAttribute()?.TypeToCreate;

      if (typeToCreate == null)
      {
        throw new ArgumentException($"Invalid enum value {fileType}");
      }

      if (!typeToCreate.IsSubclassOf(typeof(IDocumentSerializer)))
      {
        throw new ArgumentException($"Invalid type to create {typeToCreate}");
      }

      return (IDocumentSerializer)Activator.CreateInstance(typeToCreate, documentType);
    }
  }
}
