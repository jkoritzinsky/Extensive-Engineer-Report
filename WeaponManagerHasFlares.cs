using JKorTech.Extensive_Engineer_Report.TagModules;
using System.Collections.Generic;

namespace JKorTech.Extensive_Engineer_Report
{
    public class WeaponManagerHasFlares : SectionDesignConcernBase
    {
        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            return !sectionParts.AnyHasModule<TagWeaponsManager>() || sectionParts.AnyHasModule<TagFlares>();
        }

        protected internal override string Category => "Weaponry";

        public override string GetConcernDescription()
        {
            return "The ship has a weapon manager but no flares";
        }

        public override string GetConcernTitle()
        {
            return "Weapon Manager but no flares";
        }

        public override PreFlightTests.DesignConcernSeverity GetSeverity()
        {
            return PreFlightTests.DesignConcernSeverity.NOTICE;
        }
    }
}
