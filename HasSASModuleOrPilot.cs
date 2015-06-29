using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class HasSASModuleOrPilot : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Vessel lacks pilots and SAS modules. Control might be difficult.";
        }

        public override string GetConcernTitle()
        {
            return "No SAS";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.NOTICE;
        }

        public override bool TestCondition()
        {
            var hasPilots = ShipConstruction.ShipManifest.GetAllCrew(false).Any(crew => crew.experienceTrait.TypeName == "Pilot");
            var hasSAS = EditorLogic.SortedShipList.Any(part => part.FindModuleImplementing<ModuleSAS>() != null);
            return hasPilots || hasSAS;
        }
    }
}
