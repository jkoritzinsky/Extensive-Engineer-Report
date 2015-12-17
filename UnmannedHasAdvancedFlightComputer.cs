using System.Collections.Generic;
using System.Linq;

namespace JKorTech.Extensive_Engineer_Report
{
    public class UnmannedHasAdvancedFlightComputer : SectionDesignConcernBase
    {
        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            return sectionParts.AnyHasModule<TagModules.TagAdvancedFlightComputer>();
        }

        protected internal override bool IsApplicable(IEnumerable<Part> sectionParts)
        {
            return sectionParts.IsProbeControlled();
        }

        protected internal override string Category => "AdvancedFlightComputer";

        public override string GetConcernDescription()
        {
            return "Your unmanned probe is lacking an advanced flight computer (like MechJeb or kOS).";
        }

        public override string GetConcernTitle()
        {
            return "Unmanned Probe and no Advanced Flight Computer ";
        }

        public override PreFlightTests.DesignConcernSeverity GetSeverity()
        {
            return PreFlightTests.DesignConcernSeverity.NOTICE;
        }
    }
}
