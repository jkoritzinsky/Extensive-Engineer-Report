using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    internal static class TypeExtensions
    {
        public static IEnumerable<Type> BaseTypes(this Type type)
        {
            while (type.BaseType != typeof(object))
            {
                yield return type.BaseType;
                type = type.BaseType;
            }
            yield return typeof(object);
        }
    }
}
