using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Models
{
  /// <summary>
  /// IDocument interface from which all serializable models inherit
  /// Mainly used as a type check constraint so that our application is not made to serialize just any data, but only data defined by our models
  /// </summary>
  public interface IDocument
  {
  }
}
