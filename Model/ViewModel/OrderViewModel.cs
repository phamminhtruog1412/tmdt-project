using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OrderViewModel
    {
        public Order order { get; set; }
        public IList<OrderDetailViewModel> details { get; set; }
    }
}
