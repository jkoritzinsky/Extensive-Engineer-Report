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
            var thrust = EditorLogic.SortedShipList.Where(part => part.inverseStage == firstStage)
                                                    .SelectMany(part => part.FindModulesImplementing<ModuleEngines>())
                                                    .Sum(engine => engine.GetMaxThrust());
            return thrust > mass;
        }

        public override EditorFacilities GetEditorFacilities()
        {
            return EditorFacilities.VAB;
        }
    }
}
