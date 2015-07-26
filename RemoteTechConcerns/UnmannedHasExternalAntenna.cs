using JKorTech.Extensive_Engineer_Report;
using PreFlightTests;
using RemoteTech.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteTechConcerns
{
    public class UnmannedHasExternalAntenna : SectionDesignConcernBase, IPreFlightTest
    {
        public override string GetConcernDescription()
        {
            return "Your probe has no external antenna.  It will not be controllable soon after launch!";
        }

        public override string GetConcernTitle()
        {
            return "No External Antenna!";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.CRITICAL;
        }

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            return CrewInSection(sectionParts).Any() || sectionParts.AnyHasModule<ModuleRTAntenna>();
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part => {
                var commandModule = part.FindModuleImplementing<ModuleCommand>();
                return commandModule != null && (commandModule.minimumCrew == 0 || !ShipConstruction.ShipManifest.HasAnyCrew());
                }).ToList();
        }

        public string GetAbortOption()
        {
            return "Cancel";
        }

        public string GetProceedOption()
        {
            return "Launch anyway";
        }

        public string GetWarningDescription()
        {
            return GetConcernDescription();
        }

        public string GetWarningTitle()
        {
            return GetConcernTitle();
        }
    }
}
