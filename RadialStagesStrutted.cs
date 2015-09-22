using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class RadialStagesStrutted : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Radial stages are not attached to later stages by struts. You may lose \u0394v in these stages and they might ruin control.";
        }

        public override string GetConcernTitle()
        {
            return "Radial Stages Not Attached With Struts";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.NOTICE;
        }

        public override bool TestCondition()
        {
            var radialDecouplers = EditorLogic.SortedShipList.Where(IsARadialDecoupler);
            var struts = EditorLogic.SortedShipList.Where(part => part.FindModuleImplementing<CompoundParts.CModuleStrut>() != null)
                .Cast<CompoundPart>().Where(part => part.attachState == CompoundPart.AttachState.Attached)
                .ToList();
            return radialDecouplers.All(decoupler => CheckForStrutsRoot(decoupler, struts));
        }

        private static bool IsARadialDecoupler(Part part)
        {
            return part.HasModule<ModuleAnchoredDecoupler>() ||
                part?.FindModuleImplementing<ModuleDecouple>().explosiveNodeID == "srf";
        }

        private bool CheckForStrutsRoot(Part decoupler, IEnumerable<CompoundPart> struts)
        {
            return decoupler.children.Any(child => CheckForStruts(child, struts));
        }

        private bool CheckForStruts(Part part, IEnumerable<CompoundPart> struts)
        {
            if (struts.Where(strut => strut.parent == part).Any(strut => StrutsGoHigher(strut, part.inverseStage, (strut2 => strut2.target))))
                return true;
            if (struts.Where(strut => strut.target == part).Any(strut => StrutsGoHigher(strut, part.inverseStage, (strut2 => strut2.parent))))
                return true;
            return part.children.Where(child => !IsARadialDecoupler(child)).Any(child => CheckForStruts(child, struts));
        }
        private bool StrutsGoHigher(CompoundPart strut, int startingStage, Func<CompoundPart, Part> partSelector)
        {
            return partSelector(strut).inverseStage < startingStage;
        }
    }
}
