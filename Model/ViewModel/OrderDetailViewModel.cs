using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OrderDetailViewModel
    {
        public OrderDetail orderDetail { get; set; }
        public string productImage { get; set; }
        public string productName { get; set; }
    }
}
