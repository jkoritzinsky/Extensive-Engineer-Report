using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    internal static class ConcernUtils
    {
        public static bool InCorrectFacility(PreFlightTests.IDesignConcern concern)
        {
            switch (EditorDriver.editorFacility)
            {
                case EditorFacility.VAB:
                    return (concern.GetEditorFacilities() & EditorFacilities.VAB) != 0;
                case EditorFacility.SPH:
                    return (concern.GetEditorFacilities() & EditorFacilities.SPH) != 0;
            }
            return false;
        }
    }
}
