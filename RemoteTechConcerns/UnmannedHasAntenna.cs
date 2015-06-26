using PreFlightTests;
using RemoteTech;
using RemoteTech.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteTechConcerns
{
    public class UnmannedHasAntenna : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Your probe has no antenna.  It will not be controllable!";
        }

        public override string GetConcernTitle()
        {
            return "No Antenna!";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.CRITICAL;
        }

        public override bool TestCondition()
        {
            var isManned = ShipConstruction.ShipManifest.HasAnyCrew();
            if (isManned) return true;
            foreach (var part in EditorLogic.SortedShipList)
            {
                if (part.FindModuleImplementing<IAntenna>() != null) return true;
            }
            return false;
        }

        public override List<Part> GetAffectedParts()
        {
            return EditorLogic.SortedShipList.Where(part =>
            {
                var commandModule = part.FindModuleImplementing<ModuleCommand>();
                return commandModule != null && (commandModule.minimumCrew == 0 || !ShipConstruction.ShipManifest.HasAnyCrew());
            }).ToList();
        }
    }
}
