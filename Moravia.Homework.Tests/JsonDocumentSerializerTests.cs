using Moravia.Homework.Models;
using Moravia.Homework.Serialization;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Tests
{
  [TestFixture]
  public class JsonDocumentSerializerTests
  {
    private ILogger _logger;

    [SetUp]
    public void SetUp()
    {
      _logger = new LoggerConfiguration()
          .WriteTo.Console()
          .CreateLogger();
    }

    [Test]
    public void DeserializeDocument_ValidJson_ReturnsDocument()
    {
      var serializer = new JsonDocumentSerializer<BaseDocument>(_logger);
      string json = "{\"Title\": \"Document 1\", \"Text\": \"Sample text\"}";

      BaseDocument result = serializer.DeserializeDocument(json) as BaseDocument;

      Assert.IsNotNull(result);
      Assert.AreEqual("Document 1", result.Title);
      Assert.AreEqual("Sample text", result.Text);
    }

    [Test]
    public void DeserializeDocument_InvalidJson_ThrowsException()
    {
      var serializer = new JsonDocumentSerializer<BaseDocument>(_logger);
      string invalidJson = "Invalid JSON";

      Assert.Throws<JsonReaderException>(() => serializer.DeserializeDocument(invalidJson));
    }

    [Test]
    public void SerializeDocument_ValidDocument_ReturnsJson()
    {
      var serializer = new JsonDocumentSerializer<BaseDocument>(_logger);
      var document = new BaseDocument { Title = "Document 2", Text = "Another sample text" };

      string json = serializer.SerializeDocument(document);

      Assert.IsNotEmpty(json);
      Assert.IsTrue(json.Contains("Document 2"));
      Assert.IsTrue(json.Contains("Another sample text"));
    }

    [Test]
    public void SerializeDocument_NullDocument_ThrowsException()
    {
      var serializer = new JsonDocumentSerializer<BaseDocument>(_logger);

      Assert.Throws<ArgumentNullException>(() => serializer.SerializeDocument(null));
    }
  }
}
