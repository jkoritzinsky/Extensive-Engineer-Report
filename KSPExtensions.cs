using System.Collections.Generic;
using System.Linq;

namespace JKorTech.Extensive_Engineer_Report
{
    public static class KSPExtensions
    {
        public static bool HasModule<M>(this Part part)
            where M : PartModule
        {
            return part.FindModuleImplementing<M>() != null;
        }

        public static bool AnyHasModule<M>(this IEnumerable<Part> parts)
            where M : PartModule
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

        public static IDictionary<ProtoCrewMember, Part> CrewInSection(IEnumerable<Part> sectionParts)
        {
            return ShipConstruction.ShipManifest.GetAllCrew(false)
                .Select(crew => new KeyValuePair<ProtoCrewMember, Part>(crew, ShipConstruction.FindPartWithCraftID(ShipConstruction.ShipManifest.GetPartForCrew(crew).PartID)))
                .Where(pair => sectionParts.Contains(pair.Value)).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static bool IsProbeControlled(this IEnumerable<Part> sectionParts)
        {
            return sectionParts.AnyHasModule<ModuleCommand>() && !CrewInSection(sectionParts).Any(pair => pair.Value.HasModule<ModuleCommand>());
        }

        public static IEnumerable<T> GetScenarioModules<T>()
            where T : ScenarioModule
        {
            return ScenarioRunner.GetLoadedModules().OfType<T>();
        }
    }
}
