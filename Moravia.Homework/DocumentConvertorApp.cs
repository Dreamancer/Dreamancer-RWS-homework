using Microsoft.Extensions.Options;
using Moravia.Homework.DAL;
using Moravia.Homework.Models;
using Moravia.Homework.Serialization;
using Moravia.Homework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
//using Microsoft.Extensions.Logging;

namespace Moravia.Homework
{
  public class DocumentConvertorApp
  {
    private ConvertorAppSettings _settings;
    private readonly ILogger _logger;

    public DocumentConvertorApp(IOptions<ConvertorAppSettings> settings)
    {
      _settings = settings.Value;
      _logger = Log.Logger;
    }

    public async Task ExecuteDocumentConversionAsync()
    {
      try
      {
        //read input from source
        IDocumentRepo sourceRepo = DocumentRepoFactory.GetDocumentRepo(_settings.Input.RepoSettings, _logger);
        _logger.Information($"Source {sourceRepo.GetType()} initiated in {sourceRepo.Mode} mode");
        string sourceContent = await sourceRepo.ReadInputFileAsync();
        if (string.IsNullOrWhiteSpace(sourceContent))
        {
          throw new Exception("Source file content is empty");
        }
        _logger.Information($"Source file content loaded successfully");

        //deserialize to IDocument
        IDocumentSerializer deserializer = DocumentSerializerFactory.GetDocumentSerializer(_settings.Input.SerializerTypeName, _settings.Input.DocumentTypeName, _logger);
        _logger.Information($"Deserializer {deserializer.GetType()} initiated");
        IDocument document = deserializer.DeserializeDocument(sourceContent);
        if (document == null)
        {
          throw new Exception($"Deserialized document is null");
        }
        _logger.Information($"Content sucessfully deserialized to {document.GetType()}");

        //serialize to target format
        IDocumentSerializer serializer = DocumentSerializerFactory.GetDocumentSerializer(_settings.Output.SerializerTypeName, _settings.Output.DocumentTypeName, _logger);
        _logger.Information($"Serializer {serializer.GetType()} initiated");
        string targetContent = serializer.SerializeDocument(document);
        if (string.IsNullOrWhiteSpace(targetContent))
        {
          throw new Exception("Serialized target content is null");
        }
        _logger.Information($"{document.GetType()} successfully serialized to target format");

        //write converted document to target 
        IDocumentRepo targetRepo = DocumentRepoFactory.GetDocumentRepo(_settings.Output.RepoSettings, _logger);
        _logger.Information($"Target {targetRepo.GetType()} initiated in {targetRepo.Mode} mode");
        await targetRepo.WriteToOutputFileAsync(targetContent);
        _logger.Information($"Document format conversion complete. Target file can be found at '{targetRepo.Location}'");
      }
      catch (Exception ex)
      {
        _logger.Error(ex, $"Error Executing document conversion");
        throw;
      }
    }
  }
}
