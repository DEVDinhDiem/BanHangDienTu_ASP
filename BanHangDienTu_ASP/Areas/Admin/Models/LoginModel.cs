using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHangDienTu_ASP.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Vui lòng nhập User Name")]
        public String UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Pasword")]
        public String Password { get; set; }
        public bool RememberMe { get; set; }

    }
}