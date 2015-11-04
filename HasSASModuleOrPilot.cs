using PreFlightTests;
using System.Collections.Generic;
using System.Linq;

namespace JKorTech.Extensive_Engineer_Report
{
    public class HasSASModuleOrPilot : SectionDesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Vessel lacks pilots and SAS modules. Control might be difficult.";
        }

        public override string GetConcernTitle()
        {
            return "No SAS";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.NOTICE;
        }

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            var hasPilots = CrewInSection(sectionParts).Any(pair => pair.Key.experienceTrait.TypeName == "Pilot"
                                                                        && pair.Value.HasModule<ModuleCommand>());
            var hasSas = sectionParts.AnyHasModule<ModuleSAS>();
            return hasPilots || hasSas;
        }
    }
}
