using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    public class ConcernData : ShipSections.SectionData<ConcernData>
    {
        internal List<string> disabledConcerns = new List<string>();
        public ConcernData()
        {
        }

        protected override void Merge(ConcernData dataToMerge)
        {
            disabledConcerns.AddUniqueRange(dataToMerge.disabledConcerns);
        }
    }
}
