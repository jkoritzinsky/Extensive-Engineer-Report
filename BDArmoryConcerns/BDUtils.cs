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
        public static bool HasWeapons(this IEnumerable<Part> sectionParts) => sectionParts.Any(part => part.IsWeapon());

        public static bool IsWeapon(this Part part) => part.HasModule(nameof(MissileLauncher)) || part.HasModule(nameof(BahaTurret))
                                || part.HasModule(nameof(ClusterBomb)) || part.HasModule(nameof(BDMMLauncher));
    }
}
