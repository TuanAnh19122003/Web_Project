//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Table_Res
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Table_Res()
        {
            this.Reservations = new HashSet<Reservation>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<bool> status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
