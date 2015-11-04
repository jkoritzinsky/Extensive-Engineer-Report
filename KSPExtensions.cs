using System.Collections.Generic;
using System.Linq;

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
            return parts.Any(part => part.HasModule<M>());
        }

        public static bool HasModule(this Part part, string moduleName)
        {
            return part.Modules.Contains(moduleName);
        }

        public static bool AnyHasModule(this IEnumerable<Part> parts, string moduleName)
        {
            return parts.Any(part => part.HasModule(moduleName));
        }
    }
}
