using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKorTech.Extensive_Engineer_Report
{
    public class LandingLegsButNoLights : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Your vessel has landing legs or wheels but no lights.  Add lights to make night landings possible.";
        }

        public override string GetConcernTitle()
        {
            return "Missing lights for night landing";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        public override bool TestCondition()
        {
            var parts = EditorLogic.SortedShipList;
            bool hasLights = false;
            bool hasLandingLegs = false;
            foreach (var part in parts)
            {
                if (part.FindModuleImplementing<ModuleLight>() != null) hasLights = true;
                else if (part.FindModuleImplementing<ModuleLandingLeg>() != null || part.FindModuleImplementing<ModuleLandingGear>() != null
                         || part.FindModuleImplementing<ModuleLandingGearFixed>() != null)
                {
                    hasLandingLegs = true;
                }
            }
            return !hasLandingLegs || (hasLandingLegs && hasLights);
        }
    }
}
