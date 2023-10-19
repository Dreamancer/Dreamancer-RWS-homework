using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Moravia.Homework.Corrected
{
  public class Document
  {
    public string Title { get; set; }
    public string Text { get; set; }
  }
  /// <summary>
  /// My basic 'corrected' implementation of the problematic code. It isn't supposed to be the final implementation, just an example of AT LEAST runnable code.
  /// </summary>
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("Loading path arguments...");

        var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\SourceFiles\\Document1.xml");
        var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\TargetFiles\\Document1.json");

        Console.WriteLine($"Source file path: '{sourceFileName}'");
        Console.WriteLine($"Target file path: '{targetFileName}'");

        string? input = null;
        using (var reader = new StreamReader(sourceFileName, new FileStreamOptions { Mode = FileMode.Open }))
        {
          input = reader.ReadToEnd();
        }

        if (input == null)
        {
          throw new ArgumentNullException("Input is null");
        }

        XDocument? xdoc = XDocument.Parse(input) ?? throw new Exception("Parsed xdoc is null");

        Console.WriteLine($"Parsed input XML doc: {xdoc}");

        //if xdoc doesn't contain either of 'title' nor 'text' the following code will throw an exception
        //but IMO there is no need to check for that anyway because in such an event we'll have logged xdoc and will know what is missing either way.
        //and I don't suppose a correctly parsed XDocument instance won't have a Root element despite the property itself being defined as nullable
        var doc = new Document
        {
          Title = xdoc.Root.Element("title").Value,
          Text = xdoc.Root.Element("text").Value
        };

        var serializedDoc = JsonConvert.SerializeObject(doc);

        Console.WriteLine($"Serialized output json document:\n{serializedDoc}");

        using (var sw = new StreamWriter(targetFileName, new FileStreamOptions { Mode = FileMode.Create, Access = FileAccess.Write }))
        {
          sw.Write(serializedDoc);
        }

        Console.WriteLine($"Sucessfully converted XML document from '{sourceFileName}' to a JSON file at '{targetFileName}'");
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine($"Error converting document: {ex}");
        throw new Exception(ex.Message);
      }
    }
  }
}
