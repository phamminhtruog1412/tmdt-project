namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Display(Name ="ID")]
        public long ProductID { get; set; }
        [Display(Name = "Thể loại")]
        public long CategoryID { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập tên sản phẩm")]
        [StringLength(250)]
        public string ProductName { get; set; }

        [Display(Name = "Mã sp")]
        [StringLength(50)]
        public string ProductCode { get; set; }

        [Display(Name = "Seo title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập seo title")]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Display(Name = "Miêu tả")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập mô tả")]
        [Column(TypeName = "nvarchar")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(250)]
        public string Image { get; set; }

        [Display(Name = "Giá niêm yết")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập giá")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mại")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Display(Name = "Chi tiết")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập chi tiêt")]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public long ViewCount { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
