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
using Moravia.Homework.DAL.Factory;
using Moravia.Homework.Serialization.Factory;
//using Microsoft.Extensions.Logging;

namespace Moravia.Homework
{
  /// <summary>
  /// Application intended for data format conversion according to configuration provided
  /// </summary>
  public class DocumentConvertorApp
  {
    private ConvertorAppSettings _settings;
    private readonly ILogger _logger;
    private readonly IDocumentRepoFactory _documentRepoFactory;
    private readonly IDocumentSerializerFactory _documentSerializerFactory;

    /// <summary>
    /// DocumentConvertorApp constructor
    /// </summary>
    /// <param name="settings">configuration providing information about the source and target files, and their data formats</param>
    /// <param name="documentRepoFactory">The IDocumentRepoFactory instance used to create IDocumentRepo instances</param>
    /// <param name="documentSerializerFactory">The IDocumentSerializerFactory instance used to create IDocumentSerializer instances</param>
    /// <param name="logger">Serilog logger</param>
    public DocumentConvertorApp(IOptions<ConvertorAppSettings> settings, IDocumentRepoFactory documentRepoFactory, IDocumentSerializerFactory documentSerializerFactory, ILogger logger)
    {
      _settings = settings.Value;
      _logger = logger;
      _documentRepoFactory = documentRepoFactory;
      _documentSerializerFactory = documentSerializerFactory;
    }

    /// <summary>
    /// Executes the document format conversion
    /// </summary>
    /// <returns></returns>
    public async Task ExecuteDocumentConversionAsync()
    {
      try
      {
        //read input from source
        IDocumentRepo sourceRepo = _documentRepoFactory.GetDocumentRepo(_settings.Input.RepoSettings);
        _logger.Information($"Source {sourceRepo.GetType()} initiated in {sourceRepo.Mode} mode");
        string sourceContent = await sourceRepo.ReadInputFileAsync();
        if (string.IsNullOrWhiteSpace(sourceContent))
        {
          throw new Exception("Source file content is empty");
        }
        _logger.Information($"Source file content loaded successfully");

        //deserialize to IDocument
        IDocumentSerializer deserializer = _documentSerializerFactory.GetDocumentSerializer(_settings.Input.SerializerTypeName, _settings.Input.DocumentTypeName);
        _logger.Information($"Deserializer {deserializer.GetType()} initiated for document type: {deserializer.GetType().GetGenericArguments().FirstOrDefault()}");
        IDocument document = deserializer.DeserializeDocument(sourceContent);
        if (document == null)
        {
          throw new Exception($"Deserialized document is null");
        }
        _logger.Information($"Content sucessfully deserialized to {document.GetType()}");

        //serialize to target format
        IDocumentSerializer serializer = _documentSerializerFactory.GetDocumentSerializer(_settings.Output.SerializerTypeName, _settings.Output.DocumentTypeName);
        _logger.Information($"Serializer {serializer.GetType()} initiated for document type: {serializer.GetType().GetGenericArguments().FirstOrDefault()}");
        string targetContent = serializer.SerializeDocument(document);
        if (string.IsNullOrWhiteSpace(targetContent))
        {
          throw new Exception("Serialized target content is null");
        }
        _logger.Information($"{document.GetType()} successfully serialized to target format");

        //write converted document to target 
        IDocumentRepo targetRepo = _documentRepoFactory.GetDocumentRepo(_settings.Output.RepoSettings);
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
