using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Settings
{
  /// <summary>
  /// Settings object containing configuration for the format conversion
  /// </summary>
  public class ConvertorAppSettings
  {
    /// <summary>
    /// Input/source file settings
    /// </summary>
    public DataFormatConversionSettings Input { get; set; }
    /// <summary>
    /// Output/target file settings
    /// </summary>
    public DataFormatConversionSettings Output { get; set; }
  }
}
