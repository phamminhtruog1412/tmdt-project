namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        [Key]
        [Display(Name = "ID")]
        public long CategoryID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập tên danh mục")]
        [StringLength(250)]
        [Display(Name = "Tên danh mục")]
        public string CategoryName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập title seo")]
        [StringLength(250)]
        [Display(Name = "Seo title")]
        public string MetaTitle { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }
    }
}
