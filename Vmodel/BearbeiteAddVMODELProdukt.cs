using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snackautomaten_Verwaltungsapp.Vmodel
{
    class BearbeiteAddVMODELProdukt : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public PRODUKT Prod
        {
            get;
            set;
        }
        public bool IsInEditMode
        {
            get;
            set;
        }

        public IEnumerable<KATEGORIE> AlleKategorien
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    return db.KATEGORIEs.ToList();
                }
            }
        }
    }
}
