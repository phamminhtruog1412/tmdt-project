using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Model.ViewModel;
using System.Data.Entity;

namespace Model.DAO
{
    public class OrderDAO
    {
        private Dbcontext db;
        public OrderDAO() {
            db = new Dbcontext();
        }

        public bool DeleteOrderDetail(long id) {
            try
            {
                var res = (IList<OrderDetail>)db.OrderDetails.Where(x => x.OrderID == id).ToList();
                foreach (var item in res)
                {
                    res.Remove(item);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public IEnumerable<Order> GetListWithSearchString(string searchString,int page, int pageS, bool isWaiting) {
            IQueryable<Order> model = db.Orders.OrderBy(x => x.OrderID);
            long number;
            DateTime date;
            if (isWaiting)
            {
                model = model.Where(x => x.Status == 0);
            }
            else
            {
                if (searchString != null)
                {
                    bool isconvertToLong = Int64.TryParse(searchString, out number);
                    bool isconvertToDatetime = DateTime.TryParse(searchString, out date);
                    if (isconvertToDatetime)
                    {
                        model = model.Where(x => DbFunctions.TruncateTime(x.CreatedDate) == date);
                    }
                    else
                    {
                        model = model.Where(x => x.shipPhone.Contains(searchString));
                    }
                }
            }

            return model.ToPagedList(page, pageS);
        }

        public IEnumerable<Order> GetListWithSearchString2(string searchString, int page, int pageS, bool isWaiting)
        {
            IQueryable<Order> model = db.Orders.OrderBy(x => x.OrderID);
            if (isWaiting)
            {
                model = model.Where(x => x.Status == 1);
            }
            return model.ToPagedList(page, pageS);
        }

        public long Create(Order order)
        {
            try
            {
                order.Status = 0;
                order.CreatedDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return order.OrderID;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public long Delete(long id) {
            try
            {
                var model = db.Orders.Find(id);
                db.Orders.Remove(model);
                db.SaveChanges();
                return id;
            }
            catch
            {
                return -1;
            }
        }

        public int ChangeStatus(long id) {
            var model = db.Orders.Find(id);
            if(model.Status == 2)
            {
                model.Status = 0;
            }
            else
            {
                if(model.Status == 0)
                {
                    IList<OrderDetailViewModel> listDetail = new OrderDetailDAO().GetList(id);
                    foreach (var item in listDetail)
                    {
                        new ProductDAO().SubtractQuanlity(item.orderDetail.ProductID, item.orderDetail.Quanlity);
                    }
                }
                else if(model.Status == 1)
                {
                    IList<OrderDetailViewModel> listDetail = new OrderDetailDAO().GetList(id);
                    foreach (var item in listDetail)
                    {
                        new ProductDAO().AddQuanlity(item.orderDetail.ProductID, item.orderDetail.Quanlity);
                    }
                }
                model.Status += 1;
            }
            db.SaveChanges();
            return model.Status;
        }

        public OrderViewModel ViewDetail(long id) {
            var model = new OrderViewModel();
            model.order = db.Orders.Find(id);
            model.details = new OrderDetailDAO().GetList(id);
            return model;
        }

        public List<Order> GetAll()
        {
            return db.Orders.ToList();
        }

        public int CountProgressingOrder()
        {
            return db.Orders.Count(x => x.Status == 0);
        }
        public int CountProgressingOrder2()
        {
            return db.Orders.Count(x => x.Status == 1);
        }

        public int CountCancelOrder()
        {
            return db.Orders.Count(x => x.Status == 2);
        }

        public int CountTodayOrder()
        {
            int a = db.Orders.Where(x => DbFunctions.DiffDays(x.CreatedDate,DateTime.Now) == 0).Count();
            return a;
        }

        public List<Order> List_OrderByCustomerID(long id)
        {
            return db.Orders.Where(x => x.CustomerID == id).OrderBy(x => x.CreatedDate).ToList();
        }

        public long Update(string cancelNote,long id)
        {
            var order = db.Orders.Find(id);
            order.cancelNote = cancelNote;
            db.SaveChanges();
            return order.OrderID;
        }

    }
}
