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
    class ListeProdukteVMODEL : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<PRODUKT> AlleProdukte
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    var erg = (from p in db.PRODUKTs.Include("KATEGORIE")
                               orderby p.P_ID
                               select p).ToList();
                    return erg;
                }

            }
        }

        private PRODUKT selectedProdukt;

        public PRODUKT SelectedProdukt
        {
            get
            {
                return selectedProdukt;
            }
            set
            {
                selectedProdukt = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedProdukt"));
                PropertyChanged(this, new PropertyChangedEventArgs("AutomatPositions"));
            }
        }

        public IEnumerable<POSITION> AutomatPositions
        {
            get
            {
                if (SelectedProdukt != null)
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {
                        var erg = (from pos in db.POSITIONs.Include("AUTOMATEN")
                                   where pos.POS_P_ID == SelectedProdukt.P_ID
                                   select pos).ToList();

                        SelectedPosition =
                            (from pos in erg
                             where pos.POS_P_ID == SelectedPosition?.POS_P_ID
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
          
            if (SelectedProdukt != null)
            {
                var v = new BearbeiteProduktCTL();
                var vm = new BearbeiteAddVMODELProdukt();
                vm.Prod = new PRODUKT();
                vm.IsInEditMode = false;
                v.DataContext = vm;             
                v.ShowDialog();
                if (v.DialogResult == true)
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {       
                        db.PRODUKTs.Add(vm.Prod);
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AlleProdukte"));
                    
                    }
                }

            }
        }

        public void EditExecute(object param)
        {
     
      
            var v = new BearbeiteProduktCTL();
            var vm = new BearbeiteAddVMODELProdukt();
            vm.Prod = SelectedProdukt;
            vm.IsInEditMode = true;
            v.DataContext = vm;
            v.ShowDialog();
            if (v.DialogResult == true)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(vm.Prod).State = EntityState.Modified;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleProdukte"));
                 
                }
            }


        }

        public void DeleteExecute(object param)
        {
           
            if (SelectedProdukt != null)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(SelectedProdukt).State = EntityState.Deleted;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleProdukte"));
               
                }
            }
        }

        public bool CanExecute(object param)
        {
          
            if (SelectedProdukt != null) return true;
            return false;
        }
        public bool CanExecutenew(object param)
        {
          
            if (SelectedProdukt != null) return true;
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
           
            if (SelectedProdukt != null)
            {
                var v = new BearbeitePositionCTL();
                var vm = new BearbeiteAddVMODELPosition();
                vm.Pos = new POSITION { POS_P_ID = SelectedProdukt.P_ID };
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
           
            if (SelectedProdukt != null) return true;
            return false;

        }
    }
}
