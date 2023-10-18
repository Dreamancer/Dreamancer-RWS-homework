using Moravia.Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Serialization
{
  public interface IDocumentSerializer
  {
    string SerializeDocument(IDocument obj);

    IDocument DeserializeDocument(string obj);
  }
}
