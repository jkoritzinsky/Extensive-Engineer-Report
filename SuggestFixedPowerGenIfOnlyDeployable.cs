using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class SuggestFixedPowerGenIfOnlyDeployable : SectionDesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Only power generation is deployable.  Add some static generation (static solar panels or a generator) to ensure you don't run out of power before deploying.";
        }

        public override string GetConcernTitle()
        {
            return "Only power generation is deployable";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.NOTICE;
        }

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            if (sectionParts.AnyHasModule<ModuleGenerator>()) return true;
            var solarPanels = sectionParts.SelectMany(part => part.FindModulesImplementing<ModuleDeployableSolarPanel>());
            if (solarPanels.Any(panel => !panel.sunTracking)) return true;
            return solarPanels.Any();
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part => part.FindModulesImplementing<ModuleDeployableSolarPanel>().Any(panel => panel.sunTracking)).ToList();
        }
    }
}
