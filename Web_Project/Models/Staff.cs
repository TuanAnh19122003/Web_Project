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

    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            this.Bills = new HashSet<Bill>();
        }
        [DisplayName("Mã nhân viên")]
        [Required(ErrorMessage = "Vui lòng nhập mã nhân viên.")]
        public string id { get; set; }
        [DisplayName("Ảnh nhân viên")]
        [Required(ErrorMessage = "Vui lòng nhập ảnh nhân viên.")]
        public string img { get; set; }
        [DisplayName("Tên nhân viên")]
        [Required(ErrorMessage = "Vui lòng nhập tên nhân viên.")]
        public string name { get; set; }
        [DisplayName("Chức vụ")]
        [Required(ErrorMessage = "Vui lòng nhập mã chức vụ.")]
        public string position_id { get; set; }
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string phone_number { get; set; }
        [DisplayName("Ngày sinh")]
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh nhân viên.")]
        public Nullable<System.DateTime> date_of_birth { get; set; }
        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhân viên.")]
        public string address { get; set; }
        [DisplayName("Giới tính")]
        [Required(ErrorMessage = "Vui lòng nhập giới tính nhân viên.")]
        public string gender { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual Position Position { get; set; }
    }
}
