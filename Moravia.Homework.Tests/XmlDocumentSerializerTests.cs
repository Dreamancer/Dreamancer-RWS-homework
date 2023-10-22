using Moravia.Homework.Models;
using Moravia.Homework.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Moravia.Homework.Tests
{
  [TestFixture]
  public class XmlDocumentSerializerTests
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
    public void DeserializeDocument_ValidXml_ReturnsDocument()
    {
      var serializer = new XmlDocumentSerializer<BaseDocument>(_logger);
      string xml = "<BaseDocument><Title>Document 1</Title><Text>Sample text</Text></BaseDocument>";

      BaseDocument result = serializer.DeserializeDocument(xml) as BaseDocument;

      Assert.IsNotNull(result);
      Assert.AreEqual("Document 1", result.Title);
      Assert.AreEqual("Sample text", result.Text);
    }

    [Test]
    public void DeserializeDocument_InvalidXml_ThrowsException()
    {
      var serializer = new XmlDocumentSerializer<BaseDocument>(_logger);
      string invalidXml = "Invalid XML";

      Assert.Throws<XmlException>(() => serializer.DeserializeDocument(invalidXml));
    }

    [Test]
    public void SerializeDocument_ValidDocument_ReturnsXml()
    {
       var serializer = new XmlDocumentSerializer<BaseDocument>(_logger);
      var document = new BaseDocument { Title = "Document 2", Text = "Another sample text" };

      string xml = serializer.SerializeDocument(document);

      Assert.IsNotEmpty(xml);
      Assert.IsTrue(xml.Contains("Document 2"));
      Assert.IsTrue(xml.Contains("Another sample text"));
    }

    [Test]
    public void SerializeDocument_NullDocument_ThrowsException()
    {
      var serializer = new XmlDocumentSerializer<BaseDocument>(_logger);

      Assert.Throws<ArgumentNullException>(() => serializer.SerializeDocument(null));
    }

    [Test]
    public void DeserializeDocument_DifferentRootNodeName_ReturnsDocument()
    {
      var serializer = new XmlDocumentSerializer<BaseDocument>(_logger);
      string xml = "<DifferentRoot><Title>Document 3</Title><Text>Another sample text</Text></DifferentRoot>";

      BaseDocument result = serializer.DeserializeDocument(xml) as BaseDocument;

      Assert.IsNotNull(result);
      Assert.AreEqual("Document 3", result.Title);
      Assert.AreEqual("Another sample text", result.Text);
    }
  }
}
