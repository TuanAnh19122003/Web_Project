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

    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            this.Memberships = new HashSet<Membership>();
        }
        [DisplayName("Mã tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập mã tài khoản.")]
        public int id { get; set; }
        [DisplayName("Ảnh tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập ảnh tài khoản.")]
        public string img { get; set; }
        [DisplayName("Tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản.")]
        public string account1 { get; set; }
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }
        [DisplayName("Quyền")]
        [Required(ErrorMessage = "Vui lòng chọn quyền tài khoản.")]
        public string role_id { get; set; }
        [DisplayName("Ngày tạo")]
        public Nullable<System.DateTime> register_date { get; set; }
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string phone_number { get; set; }
    
        public virtual role role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
