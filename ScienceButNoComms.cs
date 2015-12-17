using PreFlightTests;
using System.Collections.Generic;
using System.Linq;
using static JKorTech.Extensive_Engineer_Report.KSPExtensions;

namespace JKorTech.Extensive_Engineer_Report
{
    public class ScienceButNoComms : SectionDesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "The ship is unmanned and has science experiments, but has no way to transmit collected science.";
        }

        public override string GetConcernTitle()
        {
            return "Unmanned With Science And No Transmitters";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        protected internal override bool IsApplicable(IEnumerable<Part> sectionParts) =>
            sectionParts.AnyHasModule<ModuleScienceExperiment>() && sectionParts.IsProbeControlled();

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            var hasAnyComms = sectionParts.AnyHasModule<ModuleDataTransmitter>();
            return hasAnyComms;
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part => part.HasModule<ModuleScienceExperiment>()).ToList();
        }
    }
}
