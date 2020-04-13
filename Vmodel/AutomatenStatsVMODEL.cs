using System;
using System.Collections.Generic;
using System.Linq;


namespace Snackautomaten_Verwaltungsapp.Vmodel
{
    class AutomatenStatsVMODEL
    {

        //Liste von Autostat Objects
        public IEnumerable<Autostat> autostatsList
        {
            get
            {
                using (SnackEmDBEntities db = new SnackEmDBEntities()) { 
                var erg = (from a in db.AUTOMATENs
                           orderby a.A_ID
                           select new Autostat
                           {
                               ID = a.A_ID,
                               Name = a.STANDORT.S_NAME,
                               PosZahl = a.POSITIONs.Count,
                               BreitePos = a.POSITIONs.Count() * 46,
                               NotEmpty = a.POSITIONs.Count(x => x.POS_LEER == false),
                               BreiteNotEmpty = a.POSITIONs.Count(x => x.POS_LEER == false) * 48
                           }).ToList();
                    System.Console.WriteLine(erg);
                return erg;
                }
            }
        }
    }
    public class Autostat
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int PosZahl { get; set; }
        public int BreitePos { get; set; }
        public int NotEmpty { get; set; }
        public int BreiteNotEmpty { get; set; }
    }
}
