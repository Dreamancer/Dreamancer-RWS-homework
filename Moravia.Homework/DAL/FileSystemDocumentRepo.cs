﻿using Moravia.Homework.Settings;
using Moravia.Homework.Settings.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Moravia.Homework.DAL
{
  internal class FileSystemDocumentRepo : DocumentRepoBase
  {

    public FileSystemDocumentRepo(DocumentRepoMode mode, string? location, ILogger logger) : base(mode, location, logger)
    {
      if (!File.Exists(Location) && Mode == DocumentRepoMode.Read)
      {
        throw new ArgumentException($"No file at path {Location} to read");
      }
    }

    
    public FileSystemDocumentRepo(DocumentRepoSettings settings, ILogger logger) : base(settings, logger)
    {
      if (!File.Exists(Location) && Mode == DocumentRepoMode.Read)
      {
        throw new ArgumentException($"No file at path {Location} to read");
      }
    }

    public async override Task<string> ReadInputFileAsync()
    {
      if (Mode == DocumentRepoMode.Write)
      {
        throw new InvalidOperationException($"Only possible in 'Read' mode");
      }

      _logger.Information($"Reading content from source file at '{Location}'");

      try
      {
        using (var reader = new StreamReader(Location, new FileStreamOptions { Mode = FileMode.Open }))
        {
          return await reader.ReadToEndAsync();
        }
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error reading content from source file");
        throw;
      }
    }

    public async override Task<string> WriteToOutputFileAsync(string content)
    {
      if (Mode == DocumentRepoMode.Read)
      {
        throw new InvalidOperationException($"Only possible in 'Write' mode");
      }

      _logger.Information($"Writing content to target file at '{Location}'");

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
        _logger.Error(ex, $"Error writing content to target file");
        throw;
      }
    }
  }
}