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
            UnityEngine.Debug.Log("[EER] strut count: " + struts.Count);
            DebugStruts(struts);
            return radialDecouplers.All(decoupler => CheckForStrutsRoot(decoupler, struts));
        }

        private static bool IsARadialDecoupler(Part part)
        {
            return part.name == "structuralPylon" || part.FindModuleImplementing<ModuleAnchoredDecoupler>();
        }

        private bool CheckForStrutsRoot(Part decoupler, IEnumerable<CompoundPart> struts)
        {
            return decoupler.children.Any(child => CheckForStruts(child, struts));
        }

        private bool CheckForStruts(Part part, IEnumerable<CompoundPart> struts)
        {
            UnityEngine.Debug.Log("[EER] Current part: " + part.name);
            UnityEngine.Debug.Log("[EER] Any struts attached to part: " + struts.Any(strut => strut.parent == part || strut.target == part));
            if (struts.Where(strut => strut.parent == part).Any(strut => StrutsGoHigher(strut, part.inverseStage, (strut2 => strut2.target))))
                return true;
            if (struts.Where(strut => strut.target == part).Any(strut => StrutsGoHigher(strut, part.inverseStage, (strut2 => strut2.parent))))
                return true;
            UnityEngine.Debug.Log("[EER] Checking Children for struts");
            return part.children.Where(child => !IsARadialDecoupler(child)).Any(child => CheckForStruts(child, struts));
        }

        private void DebugStruts(IEnumerable<CompoundPart> struts)
        {
            UnityEngine.Debug.Log("[EER] strut debug start");
            foreach (var strut in struts)
            {
                UnityEngine.Debug.Log("[EER] strut parent: " + (strut.parent != null ? strut.parent.name : "null"));
                UnityEngine.Debug.Log("[EER] strut potential parent: " + (strut.potentialParent != null ? strut.potentialParent.name : "null"));
                UnityEngine.Debug.Log("[EER] strut target: " + (strut.target != null ? strut.target.name : "null"));
                CompoundParts.CModuleStrut module = strut.FindModuleImplementing<CompoundParts.CModuleStrut>();
                if (module == null)
                {
                    UnityEngine.Debug.Log("[EER] strut module null");
                    continue;
                }
                UnityEngine.Debug.Log("[EER] strut module root: " + (module.jointRoot != null ? module.jointRoot.name : "null"));
                UnityEngine.Debug.Log("[EER] strut module target: " + (module.jointTarget != null ? module.jointTarget.name : "null"));
            }
            UnityEngine.Debug.Log("[EER] strut debug end");
        }

        private bool StrutsGoHigher(CompoundPart strut, int startingStage, Func<CompoundPart, Part> partSelector)
        {
            return partSelector(strut).inverseStage < startingStage;
        }
    }
}
