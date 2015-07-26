using JKorTech.Extensive_Engineer_Report;
using PreFlightTests;
using RemoteTech;
using RemoteTech.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteTechConcerns
{
    public class UnmannedHasAntenna : SectionDesignConcernBase
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

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            return CrewInSection(sectionParts).Any() || sectionParts.AnyHasModule<IAntenna>();
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part =>
            {
                var commandModule = part.FindModuleImplementing<ModuleCommand>();
                return commandModule != null && (commandModule.minimumCrew == 0 || !ShipConstruction.ShipManifest.HasAnyCrew());
            }).ToList();
        }
    }
}
