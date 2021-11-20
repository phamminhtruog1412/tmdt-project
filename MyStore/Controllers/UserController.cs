using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyStore.Assets.Constant;

namespace MyStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        [HttpGet]
        public ActionResult viewProfile() {
            var session = Session[Constant.CUSTOMER_SESSION];
            if (session != null) {
                var model = new UserDAO().GetByUsername((string)session);
                return View(model);
            }
            else {
                return View();
            }
        }
        //public ActionResult ViewUser(string searchString, int page = 1, int pageS = 10, bool isWaiting = false)
        //{
        //    TempData["UserAlert"] = new UserDAO().CountProgressingUser();
        //    ViewBag.searchString = searchString;
        //    return View(new UserDAO().(searchString, page, pageS, isWaiting));
        //}
        //Edit profile
        [HttpPost, ValidateInput(false)]
        public ActionResult viewProfile(User model) {
            var dao = new UserDAO();
            var user = dao.GetByUsername((string)Session[Constant.CUSTOMER_SESSION]);
            model.UserName = user.UserName;
            model.UserID = user.UserID;
            model.Password = user.Password;
            model.Status = user.Status;
            model.CreatedDate = user.CreatedDate;
            model.RoleID = user.RoleID;
            var res = dao.Edit(model);
            if (res) {
                Response.Write("<script>alert('Thông tin của bạn đã được cập nhật')</script>");
            }
            else
            {
                Response.Write("<script>alert('Cập nhật thất bại, đã có lỗi xảy ra')</script>");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            var session = Session[Constant.CUSTOMER_SESSION];
            if(session != null)
            {
                var encryptOldPass = Encryptor.MD5Hash(oldPassword);
                var encryptNewPass = Encryptor.MD5Hash(newPassword);
                var res = new UserDAO().ChangePassword(session.ToString(), encryptOldPass, encryptNewPass);
                if(res)
                {
                    Response.Write("<script>alert('Mật khẩu đã được thay đổi')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Mật khẩu hiện tại không chính xác')</script>");
                }
            }
            return View("viewProfile");
        }
    }
}