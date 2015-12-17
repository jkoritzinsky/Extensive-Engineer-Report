using PreFlightTests;
using System.Collections.Generic;
using System.Linq;

namespace JKorTech.Extensive_Engineer_Report
{
    public class VabTwrAbove1 : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "You won't be able to lift off the ground!  Either remove weight or add MOAR BOOSTERS!";
        }

        public override string GetConcernTitle()
        {
            return "TWR Below 1!";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        public override bool TestCondition()
        {
            var mass = ShipSections.API.CurrentVesselParts.Where(part => part.FindModuleImplementing<LaunchClamp>() == null).Sum(part => part.mass * 9.81);
            var firstStage = ShipSections.API.CurrentVesselParts.Max(part => part.inverseStage);
            var thrustByStage = from part in ShipSections.API.CurrentVesselParts
                                let partWithThrust = new { part, thrust = part.FindModuleImplementing<ModuleEngines>()?.GetMaxThrust() ?? 0.0f }
                                group partWithThrust by partWithThrust.part.inverseStage into stage
                                orderby stage descending
                                select stage.Sum(val => val.thrust);
            var firstEngineStageThrust = thrustByStage.FirstOrDefault(stageThrust => stageThrust > .001);
            return firstEngineStageThrust > mass;
        }

        public override EditorFacilities GetEditorFacilities()
        {
            return EditorFacilities.VAB;
        }
    }
}
