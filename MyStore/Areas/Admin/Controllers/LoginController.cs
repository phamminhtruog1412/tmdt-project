using Model.DAO;
using MyStore.Areas.Admin.Models;
using MyStore.Assets.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyStore.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout() {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return View("Index");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                int res = new UserDAO().Login(model.UserName, Encryptor.MD5Hash(model.Password),1);
                if (res == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                } else if (res == 1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                } else if (res == 2)
                {
                    ModelState.AddModelError("","Mật khẩu không chính xác");
                } else if (res == 3)
                {
                    Session.Add(Constant.USER_SESSION, model.UserName);
                    return RedirectToAction("Index","Home");
                }
            } 
            return View("Index");
        }
    }
}