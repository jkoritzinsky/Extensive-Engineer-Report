using BahaTurret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JKorTech.Extensive_Engineer_Report;

namespace BDArmoryConcerns
{
    internal static class BDUtils
    {
        public static bool HasWeapons(this IEnumerable<Part> sectionParts)
        {
            return sectionParts.Any(part => part.IsWeapon());
        }

        public static bool IsWeapon(this Part part)
        {
            return part.HasModule("MissileLauncher") || part.HasModule("BahaTurret")
                                || part.HasModule("ClusterBomb") || part.HasModule("BDMMLauncher");
        }
    }
}
