using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartMacros.DrawingsMacros
{
    class DimensionsAdditionalRebarGroup
    {
        IEnumerable<RebarGroup> RebarGroups;

        public DimensionsAdditionalRebarGroup(IEnumerator rebarGroups)
        {
            RebarGroups = rebarGroups.ToIEnumerable<RebarGroup>();
        }

        public void Run()
        {

        }

        private void InsertRechange()
        {

        }
    }
}
