using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Settings
{
  /// <summary>
  /// Settings for the repository that contains data to be serialized/deserialized
  /// </summary>
  public class DocumentRepoSettings
  {
    /// <summary>
    /// Type name of IDocumentRepo implementation to be used to store/retrieve data to be serialized/deserialized
    /// </summary>
    public string? DocumentRepoTypeName { get; set; }

    /// <summary>
    /// I/O mode of the document repository to be used. Can be either 'Read' or 'Write'.
    /// </summary>
    public DocumentRepoMode Mode { get; set; }

    /// <summary>
    /// Location of the data to be stored/retrieved by document repository
    /// Based on the IDocumentRepo implementation, this can be a file path, http or ftp address, etc...
    /// </summary>
    public string? Location { get; set; }
  }
}
