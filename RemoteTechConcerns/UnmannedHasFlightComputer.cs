using PreFlightTests;
using RemoteTech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteTechConcerns
{
    public class UnmannedHasFlightComputer : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Your probe has no flight computer. You won't be able to queue commands for your probe!";
        }

        public override string GetConcernTitle()
        {
            return "No flight computer!";
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
                if (part.FindModuleImplementing<ISignalProcessor>() != null) return true;
            }
            return false;
        }
    }
}
