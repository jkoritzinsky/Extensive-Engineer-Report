using PreFlightTests;
using System.Collections.Generic;
using System.Linq;

namespace JKorTech.Extensive_Engineer_Report
{
    public class HighHeatGenNoRadiators : SectionDesignConcernBase
    {
        private const double defaultRadiationVal = .25;
        public override string GetConcernDescription()
        {
            return "This vessel has heat generating parts (such nuclear engines), but it does not have any radiators to dissipate said heat.  Either add radiators or be really careful about overheating.";
        }

        public override string GetConcernTitle()
        {
            return "Heat generating parts; No radiators";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part => part.radiatorMax > defaultRadiationVal).ToList();
        }

        protected internal override bool IsApplicable(IEnumerable<Part> sectionParts) => GetAffectedParts(sectionParts).Any();

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            var activeRadiators = sectionParts.AnyHasModule<ModuleActiveRadiator>() || sectionParts.AnyHasModule<ModuleDeployableRadiator>();
            if (activeRadiators) return true;
            var highHeatParts = sectionParts.Where(part => part.radiatorMax > defaultRadiationVal);
            var anyHighHeatNoRadiators = highHeatParts.Any(part => !part.children.Any(child => child.name.StartsWith("radPanel")));
            return anyHighHeatNoRadiators;
        }
    }
}
