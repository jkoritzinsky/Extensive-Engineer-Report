using PreFlightTests;
using System.Collections.Generic;

namespace JKorTech.Extensive_Engineer_Report
{
    public class HasControlSurfaces : SectionDesignConcernBase
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

        public override EditorFacilities GetEditorFacilities()
        {
            return EditorFacilities.SPH;
        }

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            return sectionParts.AnyHasModule<ModuleControlSurface>();
        }
    }
}
