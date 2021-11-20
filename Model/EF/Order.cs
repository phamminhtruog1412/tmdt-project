namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Display(Name = "ID")]
        public long OrderID { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        public long? CustomerID { get; set; }

        [Display(Name = "Họ tên")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập họ tên")]
        [StringLength(50)]
        public string shipName { get; set; }

        [Display(Name = "SDT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập số điện thoại")]
        [StringLength(50)]
        public string shipPhone { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập địa chỉ nhận hàng")]
        [StringLength(50)]
        public string shipAddress { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập email liên hệ")]
        [StringLength(50)]
        public string shipEmail { get; set; }

        [Display(Name = "Tình trạng")]
        public int Status { get; set; }

        [Display(Name = "Ghi chú")]
        [StringLength(500)]
        public string note { get; set; }

        [Display(Name = "Lý do")]
        [StringLength(500)]
        public string cancelNote { get; set; }
    }
}
