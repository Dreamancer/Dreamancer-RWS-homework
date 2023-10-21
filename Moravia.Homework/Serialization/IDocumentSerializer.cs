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
    /// Serializes the IDocument
    /// </summary>
    /// <param name="obj">IDocument object to be serialized</param>
    /// <returns>string representation of serialized 'obj'</returns>
    string SerializeDocument(IDocument obj);

    /// <summary>
    /// Deserializes the string representation to a IDocument object
    /// </summary>
    /// <param name="obj">string representation of object</param>
    /// <returns>Deserialized IDocument</returns>
    IDocument DeserializeDocument(string obj);
  }
}
