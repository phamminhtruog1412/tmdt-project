using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Model.EF;
using MyStore.Assets.Constant;
using Model.ViewModel;

namespace MyStore.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {

        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            return View(new UserDAO().ListUserViewModel(searchString).ToPagedList(page, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListOfRoles = new RoleDAO().GetListAll(null);
            return View();
        }
        [HttpPost]
        public ActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var checkDuplicate = dao.IsExists(model.UserName);
                if (checkDuplicate == false)
                {
                    var encryptPass = Encryptor.MD5Hash(model.Password);
                    model.Password = encryptPass;
                    model.CreatedDate = DateTime.Now;
                    bool res = dao.Create(model);
                    if (res)
                    {
                        SetAlert("Thêm user thành công.", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        SetAlert("Thêm user thất bại", "error");
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    SetAlert("UserName đã tồn tại.", "warning");
                    ViewBag.ListOfRoles = new RoleDAO().GetListAll(null);
                    return View(model);
                }
            }
            else
            {
                ViewBag.ListOfRoles = new RoleDAO().GetListAll(null);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var user = new UserDAO().GetByID(id);
            ViewBag.Role_Id = new SelectList(new RoleDAO().GetListAll(null), "RoleID", "RoleName", user.RoleID);
            return View(user);
        }
        //[HttpPost]
        //public ActionResult Edit(User user)
        //{
        //    var dao = new UserDAO();
        //    if (!string.IsNullOrEmpty(user.Password))
        //    {
        //        user.Password = Encryptor.MD5Hash(user.Password);
        //    }
        //    bool res = dao.Edit(user);
        //    if (res)
        //    {
        //        SetAlert("Cập nhật thành công", "success");
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        SetAlert("Cập nhật thất bại", "error");
        //        return RedirectToAction("Index");
        //    }
        //}
        [HttpPost]
        public ActionResult Edit(User user)
        {
            var dao = new UserDAO();
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = Encryptor.MD5Hash(user.Password);
            }
            bool res = dao.Edit(user);
            if (res)
            {
                SetAlert("Cập nhật thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Cập nhật thất bại", "error");
                return RedirectToAction("Index");
            }
        }
        //Password
        [HttpGet]
        public ActionResult ChangePass(long id)
        {
            var user = new UserDAO().GetByID(id);
            ViewBag.Role_Id = new SelectList(new RoleDAO().GetListAll(null), "RoleID", "RoleName", user.RoleID);
            return View(user);
        }
        [HttpPost]
        public ActionResult ChangePass(User user)
        {
            var dao = new UserDAO();
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = Encryptor.MD5Hash(user.Password);
            }
            bool res = dao.Edit(user);
            if (res)
            {
                SetAlert("Mật khẩu đã được thay đổi", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Thay đổi mật khẩu thất bại", "error");
                return RedirectToAction("Index");
            }
        }
        //EndPassword
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var res = new UserDAO().Delete(id);
            if (res)
            {
                SetAlert("Xóa user thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa user thất bại", "error");
                return RedirectToAction("Index");
            }

        }
    }
}