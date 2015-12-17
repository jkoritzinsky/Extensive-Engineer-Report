using System;
using System.Collections.Generic;
using System.Linq;

namespace JKorTech.Extensive_Engineer_Report
{
    public abstract class SectionDesignConcernBase : DesignConcernBase
    {
        public sealed override bool TestCondition()
        {
            return TestCondition(ShipSections.API.CurrentVesselParts);
        }

        public abstract bool TestCondition(IEnumerable<Part> sectionParts);

        public sealed override List<Part> GetAffectedParts()
        {
            return GetAffectedParts(EditorLogic.SortedShipList);
        }

        protected internal override sealed bool IsApplicable()
        {
            return IsApplicable(ShipSections.API.CurrentVesselParts);
        }

        protected internal virtual bool IsApplicable(IEnumerable<Part> sectionParts) => true;

        private static List<Part> emptyPartList = new List<Part>();

        public virtual List<Part> GetAffectedParts(IEnumerable<Part> sectionParts) => emptyPartList;
    }
}
