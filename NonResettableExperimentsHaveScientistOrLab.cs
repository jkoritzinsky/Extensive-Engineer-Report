using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class NonResettableExperimentsHaveScientistOrLab : DesignConcernBase
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

        public override bool TestCondition()
        {
            var hasNonResettableExperiments = GetAffectedParts().Count != 0;
            var lab = EditorLogic.SortedShipList.FirstOrDefault(part => part.FindModuleImplementing<ModuleScienceLab>() != null);
            var hasLab = lab != null ? lab.protoModuleCrew.Count > 0 : false;
            var hasScientist = ShipConstruction.ShipManifest.GetAllCrew(false).Any(crew => crew.experienceTrait.TypeName == "Scientist");
            return !hasNonResettableExperiments || hasLab || hasScientist;
        }

        public override List<Part> GetAffectedParts()
        {
            return EditorLogic.SortedShipList.Where(part => part.FindModulesImplementing<ModuleScienceExperiment>().Any(exp => !exp.rerunnable)).ToList();
        }
    }
}
