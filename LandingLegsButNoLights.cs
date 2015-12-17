using PreFlightTests;
using System.Collections.Generic;

namespace JKorTech.Extensive_Engineer_Report
{
    public class LandingLegsButNoLights : SectionDesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Your vessel has landing legs or wheels but no lights.  Add lights to make night landings possible.";
        }

        public override string GetConcernTitle()
        {
            return "Missing lights for night landing";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.NOTICE;
        }

        protected internal override bool IsApplicable(IEnumerable<Part> sectionParts) =>
            sectionParts.AnyHasModule<ModuleLandingLeg>() || sectionParts.AnyHasModule<ModuleLandingGear>()
                                 || sectionParts.AnyHasModule<ModuleLandingGearFixed>();

        public override bool TestCondition(IEnumerable<Part> sectionParts) => sectionParts.AnyHasModule<ModuleLight>();
    }
}
