using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snackautomaten_Verwaltungsapp.Vmodel
{
    class BearbeiteAddVMODELKategorie : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public KATEGORIE Kat
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
