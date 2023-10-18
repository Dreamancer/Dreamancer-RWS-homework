using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Homework.Attributes
{
  public static class AttributeExtensions
  {
    public static TypeToCreateAttribute? GetTypeToCreateAttribute(this Enum value)
    {
      var enumType = value.GetType();
      var name = Enum.GetName(enumType, value);
      return enumType.GetField(name).GetCustomAttributes(false).OfType<TypeToCreateAttribute>().SingleOrDefault();
    }
  }
}
