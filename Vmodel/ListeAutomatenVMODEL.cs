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
    class ListeAutomatenVMODEL : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<AUTOMATEN> AlleAutomaten
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    var erg = (from a in db.AUTOMATENs.Include("STANDORT")
                               orderby a.A_ID
                               select a).ToList();
                    return erg;
                }

            }
        }

        private AUTOMATEN selectedAutomat;

        public AUTOMATEN SelectedAutomat
        {
            get
            {
                return selectedAutomat;
            }
            set
            {
                selectedAutomat = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedAutomat"));
                PropertyChanged(this, new PropertyChangedEventArgs("AutomatPositions"));
            }
        }

        public IEnumerable<POSITION> AutomatPositions
        {
            get
            {
                if (SelectedAutomat != null)
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {
                        var erg = (from pos in db.POSITIONs.Include("PRODUKT")
                                   where pos.POS_A_ID == SelectedAutomat.A_ID
                                   select pos).ToList();

                        SelectedPosition =
                            (from pos in erg
                             where pos.POS_A_ID == SelectedPosition?.POS_A_ID
                             && pos.POS_ID == SelectedPosition?.POS_ID
                             select pos).FirstOrDefault();

                        return erg;
                    }
                }
                return null;
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
          
            if (SelectedAutomat != null)
            {
                var v = new BearbeiteAutomatenCTL();
                var vm = new BearbeiteAddVMODELAutomat();
                vm.Auto = new AUTOMATEN();
                vm.IsInEditMode = false;
                v.DataContext = vm;            
                v.ShowDialog();
                if (v.DialogResult == true) 
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {       
                        db.AUTOMATENs.Add(vm.Auto);
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AlleAutomaten"));
                    }
                }

            }
        }

        public void EditExecute(object param)
        {
          
            var v = new BearbeiteAutomatenCTL();
            var vm = new BearbeiteAddVMODELAutomat();
            vm.Auto = SelectedAutomat;
            vm.IsInEditMode = true;
            v.DataContext = vm;
            v.ShowDialog();
            if (v.DialogResult == true)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(vm.Auto).State = EntityState.Modified;
                    db.SaveChanges();
                       PropertyChanged(this, new PropertyChangedEventArgs("AlleAutomaten"));
                }
            }
          
        }

        public void DeleteExecute(object param)
        {
           
            if (SelectedAutomat != null)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(SelectedAutomat).State = EntityState.Deleted;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleAutomaten"));
                }
            }
        }

        public bool CanExecute(object param)
        {
           
            if (SelectedAutomat != null) return true;
            return false;
        }
        public bool CanExecutenew(object param)
        {
         
            if (SelectedAutomat != null) return true;
            return false;
        }



     
        private ICommand saveCommandPos;

        public ICommand SaveCommandPos
        {
            get
            {
                if (saveCommandPos == null)
                    saveCommandPos = new DelegateCommand(EditExecutePos, CanExecutePos);
                return saveCommandPos;
            }
        }
      
        public ICommand DeleteCommandPos { get { return new DelegateCommand(DeleteExecutePos, CanExecutePos); } }
        public ICommand NewCommandPos { get { return new DelegateCommand(NewExecutePos, CanExecutenewPos); } }


        public void NewExecutePos(object param)
        {
           
            if (SelectedAutomat != null)
            {
                var v = new BearbeitePositionCTL();
                var vm = new BearbeiteAddVMODELPosition();
                vm.Pos = new POSITION { POS_A_ID = SelectedAutomat.A_ID};
                vm.IsInEditMode = false;
                v.DataContext = vm;             
                v.ShowDialog();
                if (v.DialogResult == true)  
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {       
                        db.POSITIONs.Add(vm.Pos);
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AutomatPositions"));
                    }
                }

            }
        }

        public void EditExecutePos(object param)
        {
           
            if(SelectedPosition != null) { 
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
                        PropertyChanged(this, new PropertyChangedEventArgs("AutomatPositions"));
                    }
                }
            }

        }

        public void DeleteExecutePos(object param)
        {
           
            if (SelectedPosition != null)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(SelectedPosition).State = EntityState.Deleted;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AutomatPositions"));
                }
            }
        }

        public bool CanExecutePos(object param)
        {
           
            if (SelectedPosition != null) return true;
            return false;
        }
        public bool CanExecutenewPos(object param)
        {
          
            if (SelectedAutomat != null) return true;
            return false;

        }
    }
}
