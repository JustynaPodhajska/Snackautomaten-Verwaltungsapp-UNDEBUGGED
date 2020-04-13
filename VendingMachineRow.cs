using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snackautomaten_Verwaltungsapp
{
    class VendingMachineRow
    {
        public HashSet<Object> rowPositions;

        public VendingMachineRow()
        {
            rowPositions = null;
        }
        public void addPosition(object o)
        {
            rowPositions.Add(o);
        }

        public HashSet<Object> getRowPositions()
        {
            return this.rowPositions;
        }

    }
}
