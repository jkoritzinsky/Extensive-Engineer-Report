using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JKorTech.Extensive_Engineer_Report
{
    public class ScienceButNoComms : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "The ship is unmanned and has science experiments, but has no way to transmit collected science.";
        }

        public override string GetConcernTitle()
        {
            return "Unmanned With Science And No Transmitters";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        public override bool TestCondition()
        {
            var anyCrew = ShipConstruction.ShipManifest.HasAnyCrew();
            var parts = EditorLogic.SortedShipList;
            bool hasAnyComms = false;
            bool hasScienceModules = false;
            foreach (var part in parts)
            {
                hasAnyComms |= part.FindModulesImplementing<ModuleDataTransmitter>().Count != 0;
                hasScienceModules |= part.FindModulesImplementing<ModuleScienceExperiment>().Count != 0;
            }
            return !hasScienceModules || anyCrew || hasAnyComms;
        }
    }
}
