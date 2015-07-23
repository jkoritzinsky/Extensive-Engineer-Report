using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var mass = EditorLogic.SortedShipList.Where(part => part.FindModuleImplementing<LaunchClamp>() == null).Sum(part => part.mass * 9.81);
            var firstStage = EditorLogic.SortedShipList.Max(part => part.inverseStage);
            var thrustByStage = EditorLogic.SortedShipList.GroupBy(part => part.inverseStage)
                                                    .OrderByDescending(group => group.Key)
                                                    .Select(group => group.SelectMany(part => part.FindModulesImplementing<ModuleEngines>())
                                                                           .Select(engine => engine.GetMaxThrust()).Sum());
            var firstEngineStageThrust = thrustByStage.FirstOrDefault(stageThrust => stageThrust > .001);
            UnityEngine.Debug.Log("[EER] " + firstEngineStageThrust * 1000 + "<>" + mass);
            return firstEngineStageThrust > mass;
        }

        public override EditorFacilities GetEditorFacilities()
        {
            return EditorFacilities.VAB;
        }
    }
}
