using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public abstract class SectionDesignConcernBase : DesignConcernBase
    {
        public sealed override bool TestCondition()
        {
            return TestCondition(EditorLogic.SortedShipList);
        }

        public abstract bool TestCondition(IEnumerable<Part> sectionParts);

        public sealed override List<Part> GetAffectedParts()
        {
            return GetAffectedParts(EditorLogic.SortedShipList);
        }

        private static List<Part> emptyPartList = new List<Part>();

        public virtual List<Part> GetAffectedParts(IEnumerable<Part> sectionParts)
        {
            return emptyPartList;
        }

        protected static IDictionary<ProtoCrewMember, Part> CrewInSection(IEnumerable<Part> sectionParts)
        {
            return ShipConstruction.ShipManifest.GetAllCrew(false)
                .Select(crew => new KeyValuePair<ProtoCrewMember, Part>(crew, ShipConstruction.ShipManifest.GetPartForCrew(crew).PartInfo.partPrefab))
                .Where(pair => true /*TODO: Figure out how to check if crew is in section*/).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
