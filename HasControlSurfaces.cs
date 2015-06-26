using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class HasControlSurfaces : DesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Your plane has no control surfaces.  It will be extremely difficult to fly!";
        }

        public override string GetConcernTitle()
        {
            return "No Control Surfaces!";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.CRITICAL;
        }

        public override bool TestCondition()
        {
            return EditorLogic.SortedShipList.Any(part => part.FindModuleImplementing<ModuleControlSurface>() != null);
        }

        public override EditorFacilities GetEditorFacilities()
        {
            return EditorFacilities.SPH;
        }
    }
}
