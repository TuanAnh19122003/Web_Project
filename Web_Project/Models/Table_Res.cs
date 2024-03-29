﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Table_Res
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Table_Res()
        {
            this.Reservations = new HashSet<Reservation>();
        }
        [DisplayName("Mã bàn")]
        [Required(ErrorMessage = "Vui lòng nhập mã bàn.")]
        public int id { get; set; }
        [DisplayName("Tên bàn")]
        [Required(ErrorMessage = "Vui lòng nhập tên bàn.")]
        public string name { get; set; }
        [DisplayName("Vị trí")]
        [Required(ErrorMessage = "Vui lòng nhập vị trí.")]
        public string location { get; set; }
        [DisplayName("Số lượng")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
        public Nullable<int> quantity { get; set; }
        [DisplayName("Trạng thái")]
        [Required(ErrorMessage = "Vui lòng nhập trạng thái.")]
        public Nullable<bool> status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
