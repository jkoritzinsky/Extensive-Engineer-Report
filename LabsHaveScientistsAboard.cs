using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class LabsHaveScientistsAboard : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "There is a lab on this ship, but no scientists aboard.  Add a scientist to the crew to increase processing.";
        }

        public override string GetConcernTitle()
        {
            return "Science Lab; No Scientists";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        public override bool TestCondition()
        {
            var hasLab = EditorLogic.SortedShipList.Any(part => part.FindModuleImplementing<ModuleScienceLab>() != null);
            var hasScientist = ShipConstruction.ShipManifest.GetAllCrew(false).Any(crew => crew.experienceTrait.TypeName == "Scientist");
            return !hasLab || hasScientist;
        }

        public override List<Part> GetAffectedParts()
        {
            return EditorLogic.SortedShipList.Where(part => part.FindModuleImplementing<ModuleScienceLab>() != null).ToList();
        }
    }
}
