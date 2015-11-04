using PreFlightTests;
using System.Collections.Generic;
using System.Linq;

namespace JKorTech.Extensive_Engineer_Report
{
    public class StrutsDontGoToLowerStages : SectionDesignConcernBase
    {
        public override string GetConcernDescription()
        {
            return "Struts create a lot of drag on the part they are first attached to.  Switch the direction of these struts to reduce drag";
        }

        public override string GetConcernTitle()
        {
            return "Struts Going To Lower Stages";
        }

        public override DesignConcernSeverity GetSeverity()
        {
            return DesignConcernSeverity.NOTICE;
        }

        public override bool TestCondition(IEnumerable<Part> sectionParts)
        {
            return GetAffectedParts().Count == 0;
        }

        public override List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            var struts = sectionParts.Where(part => part.FindModuleImplementing<CompoundParts.CModuleStrut>() != null)
                        .Cast<CompoundPart>().Where(part => part.attachState == CompoundPart.AttachState.Attached).ToList();
            return struts.Where(strut => strut.parent.inverseStage < strut.target.inverseStage).Cast<Part>().ToList();
        }
    }
}