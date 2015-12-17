using JKorTech.Extensive_Engineer_Report.TagModules;
using System.Collections.Generic;
using System.Linq;

namespace JKorTech.Extensive_Engineer_Report
{
    public class WeaponsHaveManager : SectionDesignConcernBase
    {
        protected internal override bool IsApplicable(IEnumerable<Part> sectionParts)
        {
            return sectionParts.AnyHasModule<TagWeapons>();
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part => part.HasModule<TagWeapons>()).ToList();
        }

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            return sectionParts.AnyHasModule<TagWeaponsManager>();
        }

        protected internal override string Category => "Weaponry";

        public override string GetConcernDescription()
        {
            return "This ship has weapons but no weapon manager.";
        }

        public override string GetConcernTitle()
        {
            return "Weapons but no weapon manager";
        }

        public override PreFlightTests.DesignConcernSeverity GetSeverity()
        {
            return PreFlightTests.DesignConcernSeverity.NOTICE;
        }
    }
}
