using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Areas.Admin.Models
{
    public class LoginModel
    {
        [Display(Name = "Tài khoản")]
        [Required(AllowEmptyStrings = false,ErrorMessage = "Mời nhập tài khoản.")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập mật khẩu.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}