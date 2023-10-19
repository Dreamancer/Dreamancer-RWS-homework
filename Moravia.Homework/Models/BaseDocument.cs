using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Models
{
  /// <summary>
  /// Base Document implementation
  /// </summary>
  public class BaseDocument : IDocument
  {
    /// <summary>
    /// The document title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The document text
    /// </summary>
    public string? Text { get; set; }
  }
}
