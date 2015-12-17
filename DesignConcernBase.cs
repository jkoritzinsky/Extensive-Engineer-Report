using System.Collections.Generic;
using System.Linq;
using static JKorTech.Extensive_Engineer_Report.KSPExtensions;

namespace JKorTech.Extensive_Engineer_Report
{
    public abstract class DesignConcernBase : PreFlightTests.DesignConcernBase
    {
        protected internal virtual string Category { get { return ""; } }

        protected internal virtual bool IsApplicable() => true;

        protected static IDictionary<ProtoCrewMember, Part> CrewInVessel()
        {
            return CrewInSection(ShipSections.API.CurrentVesselParts);
        }
    }
}
