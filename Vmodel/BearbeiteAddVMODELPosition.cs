using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snackautomaten_Verwaltungsapp.Vmodel
{
    class BearbeiteAddVMODELPosition : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public POSITION Pos
        {
            get;
            set;
        }
        public bool IsInEditMode
        {
            get;
            set;
        }


        public IEnumerable<AUTOMATEN> AlleAutomaten
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    return db.AUTOMATENs.ToList();
                }
            }
        }

        public IEnumerable<PRODUKT> AlleProdukte
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    return db.PRODUKTs.ToList();
                }
            }
        }

        public IEnumerable<bool> IsLeerOptions
        {
            get
            {
                return new List<bool>() {true, false};
            }
        }
    }
}
