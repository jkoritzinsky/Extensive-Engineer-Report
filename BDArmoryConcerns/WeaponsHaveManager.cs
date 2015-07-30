using JKorTech.Extensive_Engineer_Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BahaTurret;

namespace BDArmoryConcerns
{
    public class WeaponsHaveManager : SectionDesignConcernBase
    {
        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part => part.IsWeapon()).ToList();
        }

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            return !sectionParts.HasWeapons() || sectionParts.AnyHasModule("MissileFire");
        }

        public override string GetConcernDescription()
        {
            return "This ship has weapons but no weapon manager.";
        }

        public override string GetConcernTitle()
        {
            return "Weapons but no weapon manager";
        }

        public override PreFlightTests.DesignConcernSeverity GetSeverity()
        {
            return PreFlightTests.DesignConcernSeverity.NOTICE;
        }
    }
}
