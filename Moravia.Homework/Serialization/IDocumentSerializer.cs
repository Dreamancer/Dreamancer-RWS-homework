using Moravia.Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Serialization
{
  /// <summary>
  /// Defines document serialization functionality
  /// </summary>
  public interface IDocumentSerializer
  {
    /// <summary>
    /// The Type intended to be (de)serialized.
    /// Used insted of defining the serializer as generic, since with the intended initialization through reflection, the generic type cannot be provided at runtime
    /// </summary>
    public Type DocumentType { get; }

    /// <summary>
    /// Serializes the IDocument instance
    /// </summary>
    /// <param name="obj">IDocument object to be serialized</param>
    /// <returns>string representation of serialized 'obj'</returns>
    string SerializeDocument(IDocument obj);

    /// <summary>
    /// Deserializes the string representation to a IDocument object
    /// </summary>
    /// <param name="obj">string representation of object</param>
    /// <returns>Deserialized IDocument instance</returns>
    IDocument DeserializeDocument(string obj);
  }
}
