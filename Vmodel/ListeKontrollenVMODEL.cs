using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Snackautomaten_Verwaltungsapp.Vmodel
{
    class ListeKontrollenVMODEL : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<KONTROLLE> AlleKontrollen
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    var erg = (from kont in db.KONTROLLEs
                               orderby kont.KON_DATUM
                               select kont).ToList();
                    return erg;
                }

            }
        }

        private KONTROLLE selectedKontrolle;

        public KONTROLLE SelectedKontrolle
        {
            get
            {
                return selectedKontrolle;
            }
            set
            {
                selectedKontrolle = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedKontrolle"));
            }
        }
        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                    saveCommand = new DelegateCommand(EditExecute, CanExecute);
                return saveCommand;
            }
        }
       
        public ICommand DeleteCommand { get { return new DelegateCommand(DeleteExecute, CanExecute); } }
        public ICommand NewCommand { get { return new DelegateCommand(NewExecute, CanExecutenew); } }


        public void NewExecute(object param)
        {
         
            if(SelectedKontrolle != null) {
                var v = new BearbeiteKontrolleCTL();
                var vm = new BearbeiteAddVMODELKontrolle();
                vm.Kont = new KONTROLLE();
                vm.IsInEditMode = false;
                v.DataContext = vm;            
                v.ShowDialog();
                if (v.DialogResult == true)  
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {      
                        db.KONTROLLEs.Add(vm.Kont);
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AlleKontrollen"));
                    }
                }
            }
        }

        public void EditExecute(object param)
        {
           
            var v = new BearbeiteKontrolleCTL();
            var vm = new BearbeiteAddVMODELKontrolle();
            vm.Kont = SelectedKontrolle;
            vm.IsInEditMode = true;
            v.DataContext = vm;
            v.ShowDialog();
            if (v.DialogResult == true)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(vm.Kont).State = EntityState.Modified;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleKontrollen"));
                }
            }

        }

        public void DeleteExecute(object param)
        {
           
            if (SelectedKontrolle != null)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(SelectedKontrolle).State = EntityState.Deleted;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleKontrollen"));
                }
            }
        }

        public bool CanExecute(object param)
        {
          
            if (SelectedKontrolle != null) return true;
            return false;

        }
        public bool CanExecutenew(object param)
        {
          
            if (SelectedKontrolle != null) return true;
            return false;

        }

    }
}
