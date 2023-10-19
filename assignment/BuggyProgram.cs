using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Moravia.Homework.Buggy
{
  class Program
  {
    //some basic logging would improve debugging in case of any problems during running the code
    static void Main(string[] args)
    {
      //Not sure if the path should have been '...\\SourceFiles\\...' or '...\\Source Files\\...' or if the incorrect path was just a result of a faulty copy paste
      //but regardless a line break is not allowed in paths,
      //plus putting new line in a string like that obviously screws up the whole code. 
      //Same for TargetFiles.
      var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source
      Files\\Document1.xml");
      var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target
      Files\\Document1.json");
      try
      {
        FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
        //StreamReader code should be within a 'using' block to ensure correct object disposal
        //or alternatively reader.Close() should be called at the end of it's work cycle.
        //Moreover StreamReader initialization can be simplified as we already know the source path and there already is a constructor that takes a string path as a parameter
        var reader = new StreamReader(sourceStream);
        string input = reader.ReadToEnd();
      }
      catch (Exception ex)
      {
        //the exception is caught but never really processed. Considering there is no logging in this application, it should at least be outputed into Console.Writeline(..)
        throw new Exception(ex.Message);
      }
      //The input variable is initialized and assigned within a try code block that is out of scope from here
      //the variable should have been initialized before the try block.
      //
      //Moreover, considering that document parsing and IO operations can still throw exceptions,
      //the rest of the code should ideally be moved inside the same try block (and the variable initialization can stay where it is)
      var xdoc = XDocument.Parse(input);
      var doc = new Document
      {
        Title = xdoc.Root.Element("title").Value,
        Text = xdoc.Root.Element("text").Value
      };
      var serializedDoc = JsonConvert.SerializeObject(doc);
      //because of the messed up targetFileName initialization code, it isn't recognized as a parameter
      var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
      //Similarly as StreamReader before, the whole work cycle of StreamWriter should be in a using code block to ensure the disposal or sw.Close() should be manually called
      var sw = new StreamWriter(targetStream);
      sw.Write(serializedDoc);
    }
  }
}
