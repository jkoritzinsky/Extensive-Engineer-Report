using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class HighHeatGenNoRadiators : DesignConcernBase
    {
        private const double defaultRadiationVal = .25;
        public override string GetConcernDescription()
        {
            return "This vessel has heat generating parts (such nuclear engines), but it does not have any radiators to dissipate said heat.  Either add radiators or be really careful about overheating.";
        }

        public override string GetConcernTitle()
        {
            return "Heat generating parts; No radiators";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        public override bool TestCondition()
        {
            var noHighHeatParts = true;
            foreach (var part in EditorLogic.SortedShipList)
            {
                if (part.name.StartsWith("radPanel")) return true;
                if (part.FindModuleImplementing<ModuleActiveRadiator>() != null || part.FindModuleImplementing<ModuleDeployableRadiator>() != null) return true;
                if (part.radiatorMax > defaultRadiationVal) noHighHeatParts = false;
            }
            return noHighHeatParts;
        }

        public override List<Part> GetAffectedParts()
        {
            return EditorLogic.SortedShipList.Where(part => part.radiatorMax > defaultRadiationVal).ToList();
        }
    }
}
