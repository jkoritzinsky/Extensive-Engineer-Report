using BahaTurret;
using JKorTech.Extensive_Engineer_Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDArmoryConcerns
{
    public class AutoPilotHasWeaponManager : SectionDesignConcernBase
    {
        public override bool TestCondition(IEnumerable<Part> sectionParts) =>
            !sectionParts.AnyHasModule(nameof(BDModulePilotAI)) || sectionParts.AnyHasModule(nameof(MissileFire));

        public override string GetConcernDescription()
        {
            return "The ship has an autopilot module but no weapon manager";
        }

        public override string GetConcernTitle()
        {
            return "Autopilot but no weapon manager";
        }

        public override PreFlightTests.DesignConcernSeverity GetSeverity()
        {
            return PreFlightTests.DesignConcernSeverity.NOTICE;
        }
    }
}
