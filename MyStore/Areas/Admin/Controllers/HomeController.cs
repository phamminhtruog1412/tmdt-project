using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var orderDAO = new OrderDAO();
            ViewBag.CancelOrders = orderDAO.CountCancelOrder();
            ViewBag.CountProducts = new ProductDAO().Count();
            ViewBag.CountUsers = new UserDAO().Count();
            ViewBag.CountTodayOrders = orderDAO.CountTodayOrder();
            return View();
        }
    }
}