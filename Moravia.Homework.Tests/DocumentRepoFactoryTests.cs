using Moravia.Homework.DAL.Factory;
using Moravia.Homework.DAL;
using Moravia.Homework.Settings.Enum;
using Moravia.Homework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Moravia.Homework.Tests
{
  [TestFixture]
  public class DocumentRepoFactoryTests
  {
    private DocumentRepoFactory documentRepoFactory;
    private Serilog.ILogger _logger;
    private string _filePath = ".\\..\\..\\..\\..\\Moravia.Homework.Tests\\TestSource\\Document1.json"; //test source file path


    [OneTimeSetUp]
    public void Setup()
    {
      _logger = new LoggerConfiguration()
          .WriteTo.Console()
          .CreateLogger();

      documentRepoFactory = new DocumentRepoFactory(_logger);
    }

    [Test]
    public void GetDocumentRepo_ValidSettings_ReturnsCorrectTypeAndMode()
    {
      var validSettings = new DocumentRepoSettings
      {
        DocumentRepoTypeName = "FileSystemDocumentRepo",
        Mode = DocumentRepoMode.Read,
        Location = _filePath
      };

      var documentRepo = documentRepoFactory.GetDocumentRepo(validSettings);

      Assert.IsInstanceOf<IDocumentRepo>(documentRepo);
      Assert.IsInstanceOf<FileSystemDocumentRepo>(documentRepo);
      Assert.AreEqual(validSettings.Mode, documentRepo.Mode);
    }

    [Test]
    public void GetDocumentRepo_InvalidType_ThrowsArgumentException()
    {
      var invalidSettings = new DocumentRepoSettings
      {
        DocumentRepoTypeName = "InvalidTypeName", // Incorrect type name
        Mode = DocumentRepoMode.Write,
        Location = _filePath
      };

      Assert.Throws<ArgumentException>(() => documentRepoFactory.GetDocumentRepo(invalidSettings));
    }

    [Test]
    public void GetDocumentRepo_NullSettings_ThrowsArgumentNullException()
    {
      DocumentRepoSettings? nullSettings = null;


      Assert.Throws<ArgumentNullException>(() => documentRepoFactory.GetDocumentRepo(nullSettings));
    }

    [Test]
    public void GetDocumentRepo_NullTypeName_ThrowsArgumentNullException()
    {
      var settings = new DocumentRepoSettings
      {
        DocumentRepoTypeName = null, // Null type name
        Mode = DocumentRepoMode.Write,
        Location = "SampleLocation"
      };


      Assert.Throws<ArgumentNullException>(() => documentRepoFactory.GetDocumentRepo(settings));
    }
  }
}
