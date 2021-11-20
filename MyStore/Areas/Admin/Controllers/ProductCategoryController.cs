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
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString,int page=1,int pageS = 10)
        {
            ViewBag.searchString = searchString;
            return View(new ProductCategoryDAO().GetList(searchString).ToPagedList(page, pageS));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var dao = new ProductCategoryDAO();
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductCategory pc)
        {
            if(ModelState.IsValid)
            {
                pc.CreatedDate = DateTime.Now;
                var res = new ProductCategoryDAO().Create(pc);
                if (res)
                {
                    SetAlert("Thêm danh mục thành công", "success");
                    return RedirectToAction("Index");
                } else
                {
                    SetAlert("Thêm danh mục thất bại", "error");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(pc);
            }
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View(new ProductCategoryDAO().GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory pc)
        {
            if(ModelState.IsValid)
            {
                var res = new ProductCategoryDAO().Edit(pc);
                if(res)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Cập nhật thất bại", "error");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(pc);
            }
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var dao = new ProductCategoryDAO();
            var res = dao.IsForeignKey(id);
            if (res)
            {
                return Json(new
                {
                    Status = false
                });
            }
            else {
                dao.Delete(id);
                return Json(new
                {
                    Status = true
                });
            }
        }
    }
}