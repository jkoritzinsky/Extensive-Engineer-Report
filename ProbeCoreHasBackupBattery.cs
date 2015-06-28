using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class ProbeCoreHasBackupBattery : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "This probe lacks a backup battery with ElectricCharge flow disabled.  Without careful attention, it might run out of power. Note: This warning will not disappear after fixing until a part is added or removed.";
        }

        public override string GetConcernTitle()
        {
            return "Probe Lacks Backup Battery";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        public override bool TestCondition()
        {
            var unmannedProbe = EditorLogic.SortedShipList.Any(part =>
            {
                var commandModule = part.FindModuleImplementing<ModuleCommand>();
                return commandModule != null && (commandModule.minimumCrew == 0 || !ShipConstruction.ShipManifest.HasAnyCrew());
            });
            var backupBattery = EditorLogic.SortedShipList.Any(part =>
            {
                var electricCharge = part.Resources["ElectricCharge"];
                var isBattery = electricCharge != null && electricCharge.amount > 0;
                var isNotCommandModule = part.FindModuleImplementing<ModuleCommand>() == null;
                var flowDisabled = electricCharge.flowState == false;
                return isBattery && isNotCommandModule && flowDisabled;
            });
            return !unmannedProbe || backupBattery;
        }
    }
}
