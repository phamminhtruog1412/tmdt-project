using Model.DAO;
using Model.EF;
using MyStore.Assets.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout() {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index","Home");
        }

        // POST: Login
        [HttpPost]
        public ActionResult Index(User model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                int res = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), 2);
                if (res == 0)
                {
                    Response.Write("<script>alert('Tài khoản không tồn tại.')</script>");
                }
                else if (res == 1)
                {
                    Response.Write("<script>alert('Tài khoản đang bị khóa.')</script>");
                }
                else if (res == 2)
                {
                    Response.Write("<script>alert('Mật khẩu không chính xác.')</script>");
                }
                else if (res == 3)
                {
                    TempData["LoginMessage"] = "success";
                    Session.Add(Constant.CUSTOMER_SESSION, model.UserName);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Index");

        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            var dao = new UserDAO();
            var checkDuplicate = dao.IsExists(model.UserName);
            if (checkDuplicate == false)
            {
                var encryptPass = Encryptor.MD5Hash(model.Password);
                model.Password = encryptPass;
                model.CreatedDate = DateTime.Now;
                model.Status = true;
                model.RoleID = 2;
                bool res = dao.Create(model);
                if (res)
                {
                    TempData["RegisterMessage"] = "success";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    Response.Write("<script>alert('Có lỗi xảy ra')</script>");
                }
            }
            else

            {
                Response.Write("<script>alert('UserName đã tồn tại')</script>");
            }
            return View(model);

        }
    }
}