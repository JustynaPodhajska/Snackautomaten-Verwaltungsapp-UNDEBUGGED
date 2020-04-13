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
    class ListeStandortVMODEL : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        public IEnumerable<STANDORT> AlleStandorts
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    var erg = (from st in db.STANDORTs.Include("AUTOMATENs")
                               orderby st.S_ID
                               select st).ToList();
                    return erg;
                }

            }
        }

        private STANDORT selectedStandort;

        public STANDORT SelectedStandort
        {
            get
            {
                return selectedStandort;
            }
            set
            {
                selectedStandort = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedStandort"));
                PropertyChanged(this, new PropertyChangedEventArgs("AlleAutomaten"));
            }
        }


        public IEnumerable<AUTOMATEN> AlleAutomaten
        {
            get
            {
                if (SelectedStandort != null)
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {
                        var erg = (from a in db.AUTOMATENs.Include("POSITIONs")
                                   where a.A_S_ID == SelectedStandort.S_ID
                                   select a).ToList();

                        SelectedAutomat =
                            (from a in erg
                             where a.A_S_ID == SelectedStandort?.S_ID
                             && a.A_ID == SelectedAutomat?.A_ID
                             select a).FirstOrDefault();

                        return erg;
                    }
                }
                return null;

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
                var v = new BearbeiteStandortCTL();
                var vm = new BearbeiteAddVMODELStandort();
                vm.St = new STANDORT();
                vm.IsInEditMode = false;
                v.DataContext = vm;            
                v.ShowDialog();
                if (v.DialogResult == true) 
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {     
                        db.STANDORTs.Add(vm.St);
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AlleStandorts"));
                    }
                }
            
        }

        public void EditExecute(object param)
        {
            
          
                var v = new BearbeiteStandortCTL();
                var vm = new BearbeiteAddVMODELStandort();
                vm.St = SelectedStandort;
                vm.IsInEditMode = true;
                v.DataContext = vm;
                v.ShowDialog();
                if (v.DialogResult == true)
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {
                        db.Entry(vm.St).State = EntityState.Modified;
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AlleStandorts"));
                    }
                }

            
        }

        public void DeleteExecute(object param)
        {
           
            if (SelectedStandort != null)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(SelectedStandort).State = EntityState.Deleted;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleStandorts"));
                }
            }
        }

        public bool CanExecute(object param)
        {
            if (SelectedStandort != null) return true;
            return false;
        }
        public bool CanExecutenew(object param)
        {
            if (SelectedStandort != null) return true;
            return false;

        }




        private ICommand saveCommandAuto;

        public ICommand SaveCommandAuto
        {
            get
            {
                if (saveCommandAuto == null)
                    saveCommandAuto = new DelegateCommand(EditExecuteAuto, CanExecuteAuto);
                return saveCommandAuto;
            }
        }
        //  Command für Delete
        public ICommand DeleteCommandAuto { get { return new DelegateCommand(DeleteExecuteAuto, CanExecuteAuto); } }
        public ICommand NewCommandAuto { get { return new DelegateCommand(NewExecuteAuto, CanExecutenewAuto); } }


        public void NewExecuteAuto(object param)
        {
            // New Button was pressed
            if (SelectedStandort != null)
            {
                var v = new BearbeiteAutomatenCTL();
                var vm = new BearbeiteAddVMODELAutomat();
                vm.Auto = new AUTOMATEN { A_S_ID = SelectedStandort.S_ID};
                vm.IsInEditMode = false;
                v.DataContext = vm;             //  view.DataContext = ViewModel
                v.ShowDialog();
                if (v.DialogResult == true)  // speichern
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {        // die  stunden Instanz in den OR Mapper
                        db.AUTOMATENs.Add(vm.Auto);
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AlleAutomaten"));
                    }
                }

            }
        }

        public void EditExecuteAuto(object param)
        {
            //Edit Button was pressed
            if (SelectedAutomat != null)
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

        }

        public void DeleteExecuteAuto(object param)
        {
            // Delete Button was pressed
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

        public bool CanExecuteAuto(object param)
        {
            //  Achtung !!!  hier nur sehr perfomanten Code schreiben
            if (SelectedAutomat != null) return true;
            return false;
        }
        public bool CanExecutenewAuto(object param)
        {
            //  Achtung !!!  hier nur sehr perfomanten Code schreiben
            if (SelectedStandort != null) return true;
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
        //  Command für Delete
        public ICommand DeleteCommandPos { get { return new DelegateCommand(DeleteExecutePos, CanExecutePos); } }
        public ICommand NewCommandPos { get { return new DelegateCommand(NewExecutePos, CanExecutenewPos); } }


        public void NewExecutePos(object param)
        {
            // New Button was pressed
            if (SelectedAutomat != null)
            {
                var v = new BearbeitePositionCTL();
                var vm = new BearbeiteAddVMODELPosition();
                vm.Pos = new POSITION { POS_A_ID = SelectedAutomat.A_ID };
                vm.IsInEditMode = false;
                v.DataContext = vm;             //  view.DataContext = ViewModel
                v.ShowDialog();
                if (v.DialogResult == true)  // speichern
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {        // die  stunden Instanz in den OR Mapper
                        db.POSITIONs.Add(vm.Pos);
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AutomatPositions"));
                    }
                }

            }
        }

        public void EditExecutePos(object param)
        {
            //Edit Button was pressed
            if (SelectedPosition != null)
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
                        PropertyChanged(this, new PropertyChangedEventArgs("AutomatPositions"));
                    }
                }
            }

        }

        public void DeleteExecutePos(object param)
        {
            // Delete Button was pressed
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
            //  Achtung !!!  hier nur sehr perfomanten Code schreiben
            if (SelectedPosition != null) return true;
            return false;
        }
        public bool CanExecutenewPos(object param)
        {
            //  Achtung !!!  hier nur sehr perfomanten Code schreiben
            if (SelectedAutomat != null) return true;
            return false;

        }

    }
}
