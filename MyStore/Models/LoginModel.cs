using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public class LoginModel
    {
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản",AllowEmptyStrings = false)]
        public string userName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu", AllowEmptyStrings = false)]
        public string passWord { get; set; } 
    }
}