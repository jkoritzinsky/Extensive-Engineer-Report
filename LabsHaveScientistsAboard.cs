using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class LabsHaveScientistsAboard : SectionDesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "There is a lab on this ship, but no scientists aboard.  Add a scientist to the crew to increase processing.";
        }

        public override string GetConcernTitle()
        {
            return "Science Lab; No Scientists";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part => part.HasModule<ModuleScienceConverter>() && part.HasModule<ModuleScienceLab>()).ToList();
        }

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            var hasLab = sectionParts.AnyHasModule<ModuleScienceConverter>() && sectionParts.AnyHasModule<ModuleScienceLab>();
            var hasScientist = CrewInSection(sectionParts).Keys.Any(crew => crew.experienceTrait.TypeName == "Scientist");
            return !hasLab || hasScientist;
        }
    }
}
