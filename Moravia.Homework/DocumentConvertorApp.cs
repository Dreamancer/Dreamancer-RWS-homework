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

namespace Moravia.Homework
{
  public class DocumentConvertorApp
  {
    private ConvertorAppSettings _settings;

    public DocumentConvertorApp(IOptions<ConvertorAppSettings> settings)
    {
      _settings = settings.Value;
    }

    public async Task ExecuteDocumentConversionAsync()
    {
      try
      {
        //read input from source
        IDocumentRepo sourceRepo = DocumentRepoFactory.GetDocumentRepo(_settings.Input.RepoSettings);
        string sourceFileContent = await sourceRepo.ReadInputFile();

        if (string.IsNullOrWhiteSpace(sourceFileContent))
        {
          throw new Exception("The source file content is empty");
        }

        //deserialize to POCO class
        IDocumentSerializer deserializer = DocumentSerializerFactory.GetDocumentSerializer(_settings.Input.FileType, _settings.Input.DocumentType);
        IDocument document = deserializer.DeserializeDocument(sourceFileContent);

        //serialize to target format
        IDocumentSerializer serializer = DocumentSerializerFactory.GetDocumentSerializer(_settings.Output.FileType, _settings.Output.DocumentType);
        string targetContent = serializer.SerializeDocument(document);

        //write converted document to target 
        IDocumentRepo targetRepo = DocumentRepoFactory.GetDocumentRepo(_settings.Output.RepoSettings);
        await targetRepo.WriteToOutputFile(targetContent);
      }
      catch (Exception ex)
      {
        //todo log
        throw;
      }
    }
  }
}
