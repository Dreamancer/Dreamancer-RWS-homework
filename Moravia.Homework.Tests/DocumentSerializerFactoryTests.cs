using Moravia.Homework.Models;
using Moravia.Homework.Serialization.Factory;
using Moravia.Homework.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Moravia.Homework.Tests
{
  [TestFixture]
  public class DocumentSerializerFactoryTests
  {
    private ILogger _logger;
    private DocumentSerializerFactory _serializerFactory;

    [SetUp]
    public void SetUp()
    {
      _logger = new LoggerConfiguration()
          .WriteTo.Console()
          .CreateLogger();
      _serializerFactory = new DocumentSerializerFactory(_logger);
    }

    [Test]
    public void GetDocumentSerializer_ValidTypes_ReturnsSerializer()
    {
      // Arrange
      string serializerTypeName = "JsonDocumentSerializer";
      string documentTypeName = "BaseDocument";

      // Act
      IDocumentSerializer serializer = _serializerFactory.GetDocumentSerializer(serializerTypeName, documentTypeName);

      // Assert
      Assert.IsNotNull(serializer);
      Assert.IsInstanceOf<JsonDocumentSerializer<BaseDocument>>(serializer);
    }

    [Test]
    public void GetDocumentSerializer_InvalidTypes_ThrowsException()
    {
      // Arrange
      string invalidSerializerTypeName = "NonExistentSerializer";
      string invalidDocumentTypeName = "NonExistentDocument";

      // Act and Assert
      Assert.Throws<ArgumentException>(() => _serializerFactory.GetDocumentSerializer(invalidSerializerTypeName, invalidDocumentTypeName));
    }

    [Test]
    public void GetDocumentSerializer_NullSerializerType_ThrowsArgumentNullException()
    {
      // Arrange
      string documentTypeName = "BaseDocument";

      // Act and Assert
      Assert.Throws<ArgumentNullException>(() => _serializerFactory.GetDocumentSerializer(null, documentTypeName));
    }

    [Test]
    public void GetDocumentSerializer_NullDocumentType_ThrowsArgumentNullException()
    {
      // Arrange
      string serializerTypeName = "JsonDocumentSerializer";

      // Act and Assert
      Assert.Throws<ArgumentNullException>(() => _serializerFactory.GetDocumentSerializer(serializerTypeName, null));
    }
  }
}
