using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snackautomaten_Verwaltungsapp.Vmodel
{
    class BearbeiteAddVMODELKontrolle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public KONTROLLE Kont
        {
            get;
            set;
        }
        public bool IsInEditMode
        {
            get;
            set;
        }

        public bool NoDateEditMode
        {
            get
            {
                return !IsInEditMode;

            }
        }

        public IEnumerable<AUTOMATEN> AlleAutomaten
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    return db.AUTOMATENs.Include("STANDORT").ToList();
                }
            }
        }
    }
}
