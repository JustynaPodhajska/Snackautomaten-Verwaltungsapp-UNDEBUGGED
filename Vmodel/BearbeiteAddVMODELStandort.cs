using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Snackautomaten_Verwaltungsapp.Vmodel
{
    class BearbeiteAddVMODELStandort : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public STANDORT St
        {
            get;
            set;
        }
        public bool IsInEditMode
        {
            get;
            set;
        }
    }
}
