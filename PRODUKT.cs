//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Snackautomaten_Verwaltungsapp
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUKT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUKT()
        {
            this.POSITIONs = new HashSet<POSITION>();
        }
    
        public int P_ID { get; set; }
        public string P_NAME { get; set; }
        public string P_BEZ { get; set; }
        public int P_K_KATEGORIE { get; set; }
        public string P_URI_PHOTO { get; set; }
    
        public virtual KATEGORIE KATEGORIE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POSITION> POSITIONs { get; set; }
    }
}
