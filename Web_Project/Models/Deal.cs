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

    public partial class Deal
    {
        [DisplayName("Mã ưu đãi")]
        [Required(ErrorMessage = "Vui lòng nhập mã ưu đãi.")]
        public int id { get; set; }
        [DisplayName("Tên ưu đãi")]
        [Required(ErrorMessage = "Vui lòng nhập tên ưu đãi.")]
        public string name { get; set; }
        [DisplayName("Ảnh minh họa")]
        [Required(ErrorMessage = "Vui lòng chọn ảnh.")]
        public string img { get; set; }
        [DisplayName("Mô tả")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả.")]
        public string description { get; set; }
    }
}
