using Model.DAO;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Admin/Role
        public ActionResult Index(string searchString, int page=1, int pageS = 10)
        {
            ViewBag.searchString = searchString;
            return View(new RoleDAO().GetListAll(searchString).ToPagedList(page, pageS));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                var res = new RoleDAO().Create(role);
                if (res)
                {
                    SetAlert("Thêm mới thành công", "success");
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    SetAlert("Thêm mới thất bại", "error");
                    return RedirectToAction("Index", "Role");
                }
            }
            else
            {
                return View(role);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new RoleDAO().Delete(id);
            return RedirectToAction("Index", "Role");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(new RoleDAO().GetByID(id));
        }
        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if(ModelState.IsValid)
            {
                var res = new RoleDAO().Edit(role);
                if (res)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Role");
                } else
                {
                    SetAlert("Cập nhật thất bại", "error");
                    return RedirectToAction("Index", "Role");
                }
            }
            else
            {
                return View(role);
            }

        }
    }
}