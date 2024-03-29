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

    public partial class Contact
    {
        [DisplayName("Mã liên hệ")]
        [Required(ErrorMessage = "Vui lòng nhập mã liên hệ.")]
        public int id { get; set; }
        [DisplayName("Tên khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng.")]
        public string name { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        public string email { get; set; }
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string phone_number { get; set; }
        [DisplayName("Ghi chú")]
        public string Note { get; set; }
        [DisplayName("Ngày gửi liên hệ")]
        public Nullable<System.DateTime> request_date { get; set; }
    }
}
