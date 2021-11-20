namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public long OrderDetailID { get; set; }

        public long ProductID { get; set; }

        public long? OrderID { get; set; }

        public int Quanlity { get; set; }

        public decimal? Price { get; set; }
    }
}
