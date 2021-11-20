using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ChartsDataViewModel
    {
        public DateTime date { get; set; }
        public string value { get; set; }
    }

    public class OrderCountViewModel
    {
        public DateTime date { get; set; }
        public int count { get; set; }
    }
}
