using JKorTech.Extensive_Engineer_Report.TagModules;
using System.Collections.Generic;

namespace JKorTech.Extensive_Engineer_Report
{
    public class AutoPilotHasWeaponManager : SectionDesignConcernBase
    {
        public override bool TestCondition(IEnumerable<Part> sectionParts) =>
            !sectionParts.AnyHasModule<TagAutopilot>() || sectionParts.AnyHasModule<TagWeaponsManager>();

        protected internal override string Category => "Weaponry";

        public override string GetConcernDescription()
        {
            return "The ship has an autopilot module but no weapon manager";
        }

        public override string GetConcernTitle()
        {
            return "Autopilot but no weapon manager";
        }

        public override PreFlightTests.DesignConcernSeverity GetSeverity()
        {
            return PreFlightTests.DesignConcernSeverity.NOTICE;
        }
    }
}
