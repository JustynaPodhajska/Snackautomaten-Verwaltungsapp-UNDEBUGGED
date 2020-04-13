using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snackautomaten_Verwaltungsapp.Vmodel
{
    class BearbeiteAddVMODELAutomat : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public AUTOMATEN Auto 
        { 
            get;
            set;
        }
        public bool IsInEditMode
        {
            get;
            set;
        }

        public IEnumerable<STANDORT> AlleStandorts
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    return db.STANDORTs.ToList();
                }
            }
        }

        public IEnumerable<string> InBetriebOptions
        {
            get
            {
               return new List<string>(){ "JA", "NEIN" };
            }
        }
    }
}
