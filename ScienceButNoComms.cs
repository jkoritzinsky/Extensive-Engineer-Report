using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JKorTech.Extensive_Engineer_Report
{
    public class ScienceButNoComms : SectionDesignConcernBase
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

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            var anyCrew = CrewInSection(sectionParts).Any();
            var hasAnyComms = sectionParts.AnyHasModule<ModuleDataTransmitter>();
            var hasScienceModules = sectionParts.AnyHasModule<ModuleScienceExperiment>();
            return !hasScienceModules || anyCrew || hasAnyComms;
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part => part.HasModule<ModuleScienceExperiment>()).ToList();
        }
    }
}
