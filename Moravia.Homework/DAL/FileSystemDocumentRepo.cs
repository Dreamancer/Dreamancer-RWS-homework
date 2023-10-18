using Moravia.Homework.Settings;
using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.DAL
{
    internal class FileSystemDocumentRepo : DocumentRepoBase
  {

    public FileSystemDocumentRepo(DocumentRepoMode mode, string? location) : base(mode, location)
    {
      if (!File.Exists(Location) && Mode == DocumentRepoMode.Read)
      {
        throw new ArgumentException($"No file at path {Location} to read");
      }
    }

    public FileSystemDocumentRepo(DocumentRepoSettings settings) : base(settings)
    {
      if (!File.Exists(Location) && Mode == DocumentRepoMode.Read)
      {
        throw new ArgumentException($"No file at path {Location} to read");
      }
    }

    public async override Task<string> ReadInputFile()
    {
      if (Mode == DocumentRepoMode.Write)
      {
        throw new InvalidOperationException($"Only possible in 'Read' mode");
      }
      //todo logging
      try
      {
        using (var reader = new StreamReader(Location, new FileStreamOptions { Mode = FileMode.Open }))
        {
          return await reader.ReadToEndAsync();
        }
      }
      catch (Exception ex)
      {
        //todo
        throw;
      }
    }

    public async override Task<string> WriteToOutputFile(string content)
    {
      if (Mode == DocumentRepoMode.Read)
      {
        throw new InvalidOperationException($"Only possible in 'Write' mode");
      }
      try
      {
        using (var writer = new StreamWriter(Location, new FileStreamOptions { Mode = FileMode.Create, Access = FileAccess.Write }))
        {
          await writer.WriteAsync(content);
        }

        return Location;
      }
      catch (Exception ex)
      {
        //todo log
        throw;
      }
    }
  }
}
