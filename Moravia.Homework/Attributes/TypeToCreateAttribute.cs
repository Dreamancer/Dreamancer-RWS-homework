using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Attributes
{
  [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
  public class TypeToCreateAttribute : Attribute
  {
    public Type TypeToCreate { get; set; }

    public TypeToCreateAttribute(Type typeToCreate) 
    {
      TypeToCreate = typeToCreate;
    }
  }
}
