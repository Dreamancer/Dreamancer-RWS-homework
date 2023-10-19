using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Settings
{
  /// <summary>
  /// Contains configuration for data format conversion
  /// </summary>
  public class DataFormatConversionSettings
  {
    /// <summary>
    /// Settings for the repository containing file to be serialized/deserialized
    /// </summary>
    public DocumentRepoSettings RepoSettings { get; set; }

    /// <summary>
    /// Type name of the IDocumentSerializer implementation to be used to serialize/deserialize data.
    /// </summary>
    public string SerializerTypeName { get; set; }

    /// <summary>
    /// Type name of the model class deriving from IDocument to be serialized/deserialized
    /// </summary>
    public string DocumentTypeName { get; set; }
  }
}
