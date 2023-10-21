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
  /// Base abstract implementation of IDocumentSerializer with a generic constraint
  /// </summary> 
  public abstract class DocumentSerializerBase<T> : IDocumentSerializer where T : IDocument 
  {
    protected readonly ILogger _logger;

    /// <summary>
    /// DocumentSerializerBase constructor
    /// </summary>
    /// <param name="documentType"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public DocumentSerializerBase(ILogger logger)
    {
      _logger = logger;
    }

    public abstract IDocument DeserializeDocument(string obj);

    public abstract string SerializeDocument(IDocument obj);
  }
}
