using Moravia.Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Serialization
{
  public abstract class DocumentSerializerBase : IDocumentSerializer
  {
    protected Type _documentType;

    public DocumentSerializerBase(Type documentType)
    {
      if (documentType == null)
      {
        throw new ArgumentNullException(nameof(documentType));
      }

      if (!documentType.IsSubclassOf(typeof(IDocument)))
      {
        throw new ArgumentException($"Invalid document type {typeof(IDocument)}");
      }
      _documentType = documentType;
    }

    public abstract IDocument DeserializeDocument(string obj);

    public abstract string SerializeDocument(IDocument obj);
  }
}
