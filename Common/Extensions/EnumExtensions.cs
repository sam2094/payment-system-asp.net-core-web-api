using System;
using System.Linq;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static T GetAttribute<T>(this Enum value)
          where T : Attribute
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<T>()
                .SingleOrDefault();
        }
    }
}
