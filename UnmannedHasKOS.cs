using System.Collections.Generic;
using System.Linq;

namespace JKorTech.Extensive_Engineer_Report
{
    public class UnmannedHasKOS : SectionDesignConcernBase
    {
        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            if (!AssemblyLoader.loadedAssemblies.Any(assembly => assembly.assembly.GetName().Name == "kOS"))
                return true;
            var manned = CrewInSection(sectionParts).Any(pair => pair.Value.HasModule<ModuleCommand>());
            return !manned || sectionParts.Any(part => part.Modules.Contains("kOSProcessor"));
        }

        public override string GetConcernDescription()
        {
            return "Your unmanned probe is lacking a kOS processor.";
        }

        public override string GetConcernTitle()
        {
            return "Unmanned Probe and no kOS Processor";
        }

        public override PreFlightTests.DesignConcernSeverity GetSeverity()
        {
            return PreFlightTests.DesignConcernSeverity.NOTICE;
        }
    }
}
