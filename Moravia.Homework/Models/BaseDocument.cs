using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Models
{
  public class BaseDocument : IDocument
  {
    public string? Title { get; set; }
    public string? Text { get; set; }
  }
}
