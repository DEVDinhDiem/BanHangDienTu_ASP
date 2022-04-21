﻿namespace ModelDb.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name="Tài Khoản")]
        public string UserName { get; set; }
        [Display(Name ="Mật Khẩu")]
        [StringLength(32)]
        public string Password { get; set; }

        [StringLength(20)]
        public string GroupID { get; set; }
        [Display(Name = "Họ Và Tên")]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Địa Chỉ")]
        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "Số Điện Thoại")]
        [StringLength(50)]
        public string Phone { get; set; }
        public int? ProvinceID { get; set; }

        public int? DistrictID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }
    }
}
