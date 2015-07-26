using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class VabTwrAbove1 : SectionDesignConcernBase
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

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            var mass = sectionParts.Where(part => part.FindModuleImplementing<LaunchClamp>() == null).Sum(part => part.mass * 9.81);
            var firstStage = sectionParts.Max(part => part.inverseStage);
            var thrustByStage = sectionParts.GroupBy(part => part.inverseStage)
                                                    .OrderByDescending(group => group.Key)
                                                    .Select(group => group.SelectMany(part => part.FindModulesImplementing<ModuleEngines>())
                                                                           .Select(engine => engine.GetMaxThrust()).Sum());
            var firstEngineStageThrust = thrustByStage.FirstOrDefault(stageThrust => stageThrust > .001);
            return firstEngineStageThrust > mass;
        }

        public override EditorFacilities GetEditorFacilities()
        {
            return EditorFacilities.VAB;
        }
    }
}
