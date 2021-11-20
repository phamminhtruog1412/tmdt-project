using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDetailDAO
    {
        private Dbcontext db = null;
        public OrderDetailDAO() {
            db = new Dbcontext();
        }


        public IList<OrderDetailViewModel> GetList(long id) {
            var model = from a in db.OrderDetails
                        join b in db.Products
                        on a.ProductID equals b.ProductID
                        select new OrderDetailViewModel()
                        {
                            orderDetail = a,
                            productImage = b.Image,
                            productName = b.ProductName
                        };
            return model.Where(x => x.orderDetail.OrderID == id).ToList();
        }

        public bool Create(OrderDetail orderDetail) {
            try
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
