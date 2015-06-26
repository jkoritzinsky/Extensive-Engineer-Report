using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class SuggestFixedPowerGenIfOnlyDeployable : DesignConcernBase
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

        public override bool TestCondition()
        {
            var parts = EditorLogic.SortedShipList;
            var hasNoDeployableGen = true;
            foreach (var part in parts)
            {
                if (part.FindModuleImplementing<ModuleGenerator>()) return true;
                var solarPanel = part.FindModuleImplementing<ModuleDeployableSolarPanel>();
                if (solarPanel != null)
                {
                    if (!solarPanel.sunTracking) return true;
                    else hasNoDeployableGen = false;
                }
            }
            return hasNoDeployableGen;
        }
    }
}
