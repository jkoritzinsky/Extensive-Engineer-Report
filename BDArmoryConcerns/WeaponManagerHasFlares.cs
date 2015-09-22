using BahaTurret;
using JKorTech.Extensive_Engineer_Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDArmoryConcerns
{
    public class WeaponManagerHasFlares : SectionDesignConcernBase
    {
        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            return !sectionParts.AnyHasModule(nameof(MissileFire)) || sectionParts.AnyHasModule(nameof(CMDropper));
        }

        public override string GetConcernDescription()
        {
            return "The ship has a weapon manager but no flares";
        }

        public override string GetConcernTitle()
        {
            return "Weapon Manager but no flares";
        }

        public override PreFlightTests.DesignConcernSeverity GetSeverity()
        {
            return PreFlightTests.DesignConcernSeverity.NOTICE;
        }
    }
}
