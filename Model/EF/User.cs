namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Display(Name = "ID")]
        public long UserID { get; set; }

        [Display(Name = "Tài khoản")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập tài khoản")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập mật khẩu")]
        [StringLength(32)]
        public string Password { get; set; }

        [Display(Name = "Họ tên")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "SĐT")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(250)]
        public string Address { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Nhóm")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập quyền hạn")]
        public int RoleID { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }
    }
}
