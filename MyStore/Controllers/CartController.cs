using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyStore.Models;
using Model.DAO;
using System.Web.Script.Serialization;
using Model.EF;
using MyStore.Assets.Constant;
using Model.ViewModel;

namespace MyStore.Controllers
{
    public class CartController : Controller
    {
        private const string CARTSESSION = "CARTSESSION";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CARTSESSION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult AddItem(long productID)
        {
            var product = new ProductDAO().GetByID(productID);

                var cart = Session[CARTSESSION];
                if (cart != null)
                {
                    var list = (List<CartItem>)cart;
                    if (list.Exists(x => x.product.ProductID == productID))
                    {
                        foreach (var i in list)
                        {
                            if (i.product.ProductID == productID)
                            {
                                i.quanlity += 1;
                            }
                        }
                    }
                    else
                    {
                        var item = new CartItem();
                        item.product = product;
                        item.quanlity = 1;
                        list.Add(item);
                    }

                    Session[CARTSESSION] = list;
                }
                else
                {
                    var item = new CartItem();
                    item.product = product;
                    item.quanlity = 1;
                    var list = new List<CartItem>();
                    list.Add(item);

                    Session[CARTSESSION] = list;

                }
                return RedirectToAction("Index");

            
        }

        public JsonResult Update(string cartModel) {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CARTSESSION];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.product.ProductID == item.product.ProductID);
                if (jsonItem != null) {
                    item.quanlity = jsonItem.quanlity;
                }
            }
            Session[CARTSESSION] = sessionCart;
            return Json(new {
                Status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll() {
            Session[CARTSESSION] = null;
            return Json(new {
                Status = true
            });
        }

        [HttpPost]
        public JsonResult Delete(long id) {
            var sessionCart = (List<CartItem>)Session[CARTSESSION];
            sessionCart.RemoveAll(x => x.product.ProductID == id);
            Session[CARTSESSION] = sessionCart;
            return Json(new {
                Status = true
            });
        }

        public ActionResult Message(string msg)
        {
            ViewBag.msgStatus = msg;
            return View();
        }

        public ActionResult Payment() {
            return View();
        }

        [HttpPost]
        public ActionResult PaymentWithLogin(string note)
        {
            if (ModelState.IsValid)
            {
                var user = new UserDAO().GetByUsername((string)Session[Constant.CUSTOMER_SESSION]);
                var orderInfo = new Order();
                orderInfo.CustomerID = user.UserID;
                orderInfo.shipAddress = user.Address;
                orderInfo.shipEmail = user.Email;
                orderInfo.shipPhone = user.Phone;
                orderInfo.shipName = user.Name;
                orderInfo.note = note;
                var res = new OrderDAO().Create(orderInfo);
                if (res == -1)
                {
                    return RedirectToAction("Message", new { msg = "error" });
                }
                else {
                    setOrdelDetail(res);
                    return RedirectToAction("Message", new { msg = "success" });
                }
            }
            else{
                return RedirectToAction("Message", new { msg = "error" });
            }
        }

        [HttpPost]
        public ActionResult Payment(Order order) {
            if (ModelState.IsValid)
            {
                var dao = new OrderDAO();
                var res = dao.Create(order);
                if (res == -1)
                {
                    return RedirectToAction("Message", new { msg = "error" });
                }
                else {
                    setOrdelDetail(res);
                    return RedirectToAction("Message", new { msg = "success" });
                }

            }
            else {
                return RedirectToAction("Message", new { msg = "error" });
            }

        }

        [HttpPost]
        public ActionResult CheckProductAvailable()
        {
            var result = new List<Product>();

            var items = (List<CartItem>)Session[CARTSESSION];
            var productDAO = new ProductDAO();
            foreach (var item in items)
            {
                var orderDetail = new OrderDetail();
                var product = productDAO.GetByID(item.product.ProductID);
                if(product.Quantity < item.quanlity)
                {
                    result.Add(product);
                }
            }

            return Json(new {
                Data = result
            });
        }

        void setOrdelDetail(long res) {
            var items = (List<CartItem>)Session[CARTSESSION];
            var orderDetailDAO = new OrderDetailDAO();
            foreach (var item in items)
            {
                var orderDetail = new OrderDetail();
                orderDetail.ProductID = item.product.ProductID;
                orderDetail.OrderID = res;
                orderDetail.Quanlity = item.quanlity;
                if (item.product.PromotionPrice == 0)
                {
                    orderDetail.Price = item.product.Price;
                }
                else {
                    orderDetail.Price = item.product.PromotionPrice;
                }
                orderDetailDAO.Create(orderDetail);
            }
            Session[CARTSESSION] = null;
        }

        public ActionResult ShowTransactionHistory()
        {
            UserDAO userDAO = new UserDAO();
            var session = Session[Constant.CUSTOMER_SESSION];
            if (session != null)
            {
                var customer = userDAO.GetByUsername(session.ToString());
                var user = userDAO.GetByUsername((string)session);
                List<Order> list = new List<Order>();
                list = new OrderDAO().List_OrderByCustomerID(user.UserID);
                return View(list);
            }
            else
            {
                return View();
            }
        }

        public ActionResult ShowDetailTran(long id)
        {
            var list = new OrderDetailDAO().GetList(id);
            return View(list);
        }
    }
}