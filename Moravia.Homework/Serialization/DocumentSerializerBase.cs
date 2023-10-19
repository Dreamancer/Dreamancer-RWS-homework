using Moravia.Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Moravia.Homework.Serialization
{
  /// <summary>
  /// Base abstract implementation of IDocumentSerializer
  /// </summary> 
  public abstract class DocumentSerializerBase : IDocumentSerializer
  {
    protected readonly ILogger _logger;

    public Type DocumentType { get; }

    /// <summary>
    /// DocumentSerializerBase constructor
    /// </summary>
    /// <param name="documentType"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public DocumentSerializerBase(Type documentType, ILogger logger)
    {
      if (documentType == null)
      {
        throw new ArgumentNullException(nameof(documentType));
      }

      //since we can't define our serializers as generic, we introduce a manual type constraint in constructor
      if (!documentType.GetInterfaces().Contains(typeof(IDocument)))
      {
        throw new ArgumentException($"Invalid document type {documentType}");
      }

      DocumentType = documentType;
      _logger = logger;
    }

    public abstract IDocument DeserializeDocument(string obj);

    public abstract string SerializeDocument(IDocument obj);
  }
}
