namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role
    {
        [Display(Name = "ID")]
        public int RoleID { get; set; }

        [Display(Name = "Tên nhóm")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập tên nhóm")]
        [StringLength(30)]
        public string RoleName { get; set; }
    }
}
