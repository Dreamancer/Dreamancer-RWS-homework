using Moravia.Homework.Settings;
using NUnit.Framework.Internal;
using Serilog;
using Moravia.Homework.DAL;
using Moravia.Homework.Settings.Enum;

namespace Moravia.Homework.Tests
{
  [TestFixture]
  public class FileSystemDocumentRepoTests
  {
    private Serilog.ILogger _logger;
    private string _jsonContent; // Sample JSON content
    private string _filePath = ".\\..\\..\\..\\..\\Moravia.Homework.Tests\\TestSource\\Document1.json"; //test source file path


    [OneTimeSetUp]
    public void Setup()
    {
      _logger = new LoggerConfiguration()
          .WriteTo.Console()
          .CreateLogger();

      _jsonContent = "{\"Title\":\"This is the title\",\"Text\":\"This is the text content\"}";
    }

    [Test]
    public void Constructor_WhenFileDoesNotExistInReadMode_ThrowsArgumentException()
    {
      DocumentRepoSettings settings = new DocumentRepoSettings
      {
        Location = "NonExistentFile.txt" ,
        Mode = DocumentRepoMode.Read       
      };

      Assert.Throws<ArgumentException>(() => new FileSystemDocumentRepo(settings, _logger));
    }

    [Test]
    public void ReadInputFileAsync_WhenInWriteMode_ThrowsInvalidOperationException()
    {
      DocumentRepoSettings settings = new DocumentRepoSettings
      {
        Location = _filePath,
        Mode = DocumentRepoMode.Write
      };

      var repo = new FileSystemDocumentRepo(settings, _logger);

      Assert.ThrowsAsync<InvalidOperationException>(() => repo.ReadInputFileAsync());
    }

    [Test]
    public void WriteToOutputFileAsync_WhenInReadMode_ThrowsInvalidOperationException()
    {
      DocumentRepoSettings settings = new DocumentRepoSettings
      {
        Location = _filePath,
        Mode = DocumentRepoMode.Read
      };
      var repo = new FileSystemDocumentRepo(settings, _logger);

      Assert.ThrowsAsync<InvalidOperationException>(() => repo.WriteToOutputFileAsync(_jsonContent));
    }

    [Test]
    public async Task WriteAndVerifyJsonContent()
    {
      string testFilePath = ".\\..\\..\\..\\..\\Moravia.Homework.Tests\\TestTarget\\TestJsonOutput.json";
      DocumentRepoSettings settings = new DocumentRepoSettings
      {
        Location = testFilePath,
        Mode = DocumentRepoMode.Write
      };

      var repo = new FileSystemDocumentRepo(settings, _logger);

      try
      {
        await repo.WriteToOutputFileAsync(_jsonContent);

        Assert.IsTrue(File.Exists(testFilePath)); 

        string fileContent = File.ReadAllText(testFilePath);
        Assert.AreEqual(_jsonContent, fileContent);
      }
      finally
      {
        if (File.Exists(testFilePath))
        {
          File.Delete(testFilePath);
        }
      }
    }
  }
}