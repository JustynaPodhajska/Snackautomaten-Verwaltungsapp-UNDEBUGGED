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
    class ListePositionenVMODEL : INotifyPropertyChanged
    {
        //allle pos incl automat
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<POSITION> AllePositionen
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    var erg = (from pos in db.POSITIONs.Include("AUTOMATEN.STANDORT").Include("PRODUKT")
                               orderby pos.POS_ID
                               select pos).ToList();
                    return erg;
                }

            }
        }
        private POSITION selectedPosition;

        public POSITION SelectedPosition
        {
            get
            {
                return selectedPosition;
            }
            set
            {
                selectedPosition = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedPosition"));
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
           
            if (SelectedPosition != null)
            {
                var v = new BearbeitePositionCTL();
                var vm = new BearbeiteAddVMODELPosition();
                vm.Pos = new POSITION();
                vm.IsInEditMode = false;
                v.DataContext = vm;            
                v.ShowDialog();
                if (v.DialogResult == true) 
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {       
                        db.POSITIONs.Add(vm.Pos);
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AllePositionen"));
                    }
                }

            }
        }

        public void EditExecute(object param)
        {
           
            var v = new BearbeitePositionCTL();
            var vm = new BearbeiteAddVMODELPosition();
            vm.Pos = SelectedPosition;
            vm.IsInEditMode = true;
            v.DataContext = vm;
            v.ShowDialog();
            if (v.DialogResult == true)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(vm.Pos).State = EntityState.Modified;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AllePositionen"));
                }
            }

        }

        public void DeleteExecute(object param)
        {
          
            if (SelectedPosition != null)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(SelectedPosition).State = EntityState.Deleted;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AllePositionen"));
                }
            }
        }

        public bool CanExecute(object param)
        {
          
            if (SelectedPosition != null) return true;
            return false;
        }
        public bool CanExecutenew(object param)
        {
          
            if (SelectedPosition != null) return true;
            return false;
        }

    }
}
