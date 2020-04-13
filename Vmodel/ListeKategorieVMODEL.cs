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
    class ListeKategorieVMODEL : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<KATEGORIE> AlleKategorien
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    var erg = (from kat in db.KATEGORIEs.Include("PRODUKTs")
                               orderby kat.K_NAME
                               select kat).ToList();
                    return erg;
                }

            }
           
        }

        private KATEGORIE selectedKategorie;

        public KATEGORIE SelectedKategorie
        {
            get
            {
                return selectedKategorie;
            }
            set
            {
                selectedKategorie = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedKategorie"));
                PropertyChanged(this, new PropertyChangedEventArgs("AlleProdukte"));
            }
        }

        public IEnumerable<PRODUKT> AlleProdukte
        {
            get
            {
                if (SelectedKategorie != null)
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {
                        var erg = (from prod in db.PRODUKTs
                                   where prod.P_K_KATEGORIE == SelectedKategorie.K_ID
                                   select prod).ToList();

                        SelectedProdukt =
                            (from prod in erg
                             where prod.P_K_KATEGORIE == SelectedKategorie?.K_ID
                             && prod.P_ID == SelectedProdukt?.P_ID
                             select prod).FirstOrDefault();

                        return erg;
                    }
                }
                return null;
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
            if (SelectedKategorie != null)
            {
                var v = new BearbeiteKategorieCTL();
                var vm = new BearbeiteAddVMODELKategorie();
                vm.Kat = new KATEGORIE();
                vm.IsInEditMode = false;
                v.DataContext = vm;             
                v.ShowDialog();
                if (v.DialogResult == true)  
                {
                    using (SnackEmDBEntities db = new SnackEmDBEntities())
                    {        
                        db.KATEGORIEs.Add(vm.Kat);
                        db.SaveChanges();
                        PropertyChanged(this, new PropertyChangedEventArgs("AlleKategorien"));
                    }
                }

            }
        }

        public void EditExecute(object param)
        {
          
            var v = new BearbeiteKategorieCTL();
            var vm = new BearbeiteAddVMODELKategorie();
            vm.Kat = SelectedKategorie;
            vm.IsInEditMode = true;
            v.DataContext = vm;
            v.ShowDialog();
            if (v.DialogResult == true)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(vm.Kat).State = EntityState.Modified;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleKategorien"));
                }
            }

        }

        public void DeleteExecute(object param)
        {
           
            if (SelectedKategorie != null)
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities())
                {
                    db.Entry(SelectedKategorie).State = EntityState.Deleted;
                    db.SaveChanges();
                    PropertyChanged(this, new PropertyChangedEventArgs("AlleKategorien"));
                }
            }
        }

        public bool CanExecute(object param)
        {
           
            if (SelectedKategorie != null) return true;
            return false;
        }
        public bool CanExecutenew(object param)
        {
            
            if (SelectedKategorie != null) return true;
            return false;
        }




        private ICommand saveCommandProd;

        public ICommand SaveCommandProd
        {
            get
            {
                if (saveCommandProd == null)
                    saveCommandProd = new DelegateCommand(EditExecuteProd, CanExecuteProd);
                return saveCommandProd;
            }
        }
      
        public ICommand DeleteCommandProd { get { return new DelegateCommand(DeleteExecuteProd, CanExecuteProd); } }
        public ICommand NewCommandProd { get { return new DelegateCommand(NewExecuteProd, CanExecutenewProd); } }


        public void NewExecuteProd(object param)
        {
         
            if (SelectedKategorie != null)
            {
                var v = new BearbeiteProduktCTL();
                var vm = new BearbeiteAddVMODELProdukt();
                vm.Prod = new PRODUKT { P_K_KATEGORIE = SelectedKategorie.K_ID };
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

        public void EditExecuteProd(object param)
        {
          
            if (SelectedKategorie != null)
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

        }

        public void DeleteExecuteProd(object param)
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

        public bool CanExecuteProd(object param)
        {
           
            if (SelectedProdukt != null) return true;
            return false;
        }
        public bool CanExecutenewProd(object param)
        {
           
            if (SelectedKategorie != null) return true;
            return false;

        }
    }
}
