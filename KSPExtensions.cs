using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public static class KSPExtensions
    {
        public static bool HasModule<M>(this Part part)
            where M : class
        {
            return part.FindModuleImplementing<M>() != null;
        }

        public static bool AnyHasModule<M>(this IEnumerable<Part> parts)
            where M : class
        {
            return parts.Any(part => part.FindModuleImplementing<M>() != null);
        }
    }
}
