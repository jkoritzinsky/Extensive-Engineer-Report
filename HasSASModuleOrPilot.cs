using PreFlightTests;
using System.Collections.Generic;
using System.Linq;
using static JKorTech.Extensive_Engineer_Report.KSPExtensions;

namespace JKorTech.Extensive_Engineer_Report
{
    public class HasSASModuleOrPilot : SectionDesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Vessel lacks pilots and SAS modules or reaction wheels. Control might be difficult.";
        }

        public override string GetConcernTitle()
        {
            return "No SAS or Reaction Wheels";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.NOTICE;
        }

        protected internal override bool IsApplicable(IEnumerable<Part> currentVesselParts) => currentVesselParts.AnyHasModule<ModuleCommand>();

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            var hasPilots = CrewInSection(sectionParts).Any(pair => pair.Key.experienceTrait.TypeName == "Pilot"
                                                                        && pair.Value.HasModule<ModuleCommand>());
            var hasSas = sectionParts.AnyHasModule<ModuleSAS>();
            var hasReactionWheels = sectionParts.AnyHasModule<ModuleReactionWheel>();
            return (hasPilots || hasSas) && hasReactionWheels;
        }
    }
}
