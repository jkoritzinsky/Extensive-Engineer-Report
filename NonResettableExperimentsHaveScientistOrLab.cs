using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class NonResettableExperimentsHaveScientistOrLab : SectionDesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "You have single-use experiments on board, but you do not have a scientist or a manned science lab.";
        }

        public override string GetConcernTitle()
        {
            return "Single-use experiments; No Scientist or Manned Lab";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.WARNING;
        }

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            var hasNonResettableExperiments = GetAffectedParts(sectionParts).Count != 0;
            var hasCrewedLab = CrewInSection(sectionParts).Values.AnyHasModule<ModuleScienceLab>();
            var hasScientist = CrewInSection(sectionParts).Any(pair => pair.Key.experienceTrait.TypeName == "Scientist");
            return !hasNonResettableExperiments || hasCrewedLab || hasScientist;
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return sectionParts.Where(part => part.FindModulesImplementing<ModuleScienceExperiment>().Any(exp => !exp.rerunnable)).ToList();
        }
    }
}
