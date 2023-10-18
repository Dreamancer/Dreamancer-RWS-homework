using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.DAL
{
    public interface IDocumentRepo
  {
    /// <summary>
    /// Sets the IDocumentRepo instance to a read or write only mode.
    /// Must be set in the constructor.
    /// </summary>
    public DocumentRepoMode Mode { get; }
    /// <summary>
    /// Location of the file we want to read from/write to. a filesystem path, https address, ftp address, etc...
    /// Must be set in the constructor
    /// </summary>
    public string Location { get; }

    /// <summary>
    /// Returns the content of the file defined in the 'Location' property
    /// </summary>
    /// <returns></returns>
    Task<string> ReadInputFileAsync();

    /// <summary>
    /// Outputs the 'Location' property after sucessfully writing into the file defined in the same 'Location' property
    /// </summary>
    /// <returns></returns>
    Task<string> WriteToOutputFileAsync(string content);
  }
}
