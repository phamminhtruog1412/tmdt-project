using Model.DAO;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString,int page = 1,int pageSize = 10)
        {
            return View(new ProductDAO().GetListProductViewModel(searchString).ToPagedList(page, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListOfCate = new ProductDAO().GetListAll();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Product model)
        {
            var dao = new ProductDAO();
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("/Assets/Client/images/"), fileName);
                        file.SaveAs(path);
                        model.Image = "/Assets/Client/images/"+fileName;
                        model.CreatedDate = DateTime.Now;
                        var res = dao.Create(model);
                        if (res)
                        {
                            SetAlert("Thêm sản phẩm thành công", "success");
                            return RedirectToAction("Index", "Product");
                        }
                        else
                        {
                            SetAlert("Thêm sản phẩm thất bại", "error");
                            return RedirectToAction("Index", "Product");
                        }
                    }
                    else
                    {
                        SetAlert("Thêm sản phẩm thất bại", "error");
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {
                    SetAlert("Thêm sản phẩm thất bại", "error");
                    return RedirectToAction("Index", "Product");
                }
            }
            else
            {
                ViewBag.ListOfCate = dao.GetListAll();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ProductDAO();
            var product = dao.GetByID(id);
            ViewBag.Categorys_Id = new SelectList(dao.GetListAll(), "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            var dao = new ProductDAO();
            if (ModelState.IsValid)
            {
                bool isImage;
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("/Assets/Client/images/"), fileName);
                        file.SaveAs(path);
                        product.Image = "/Assets/Client/images/"+fileName;
                        isImage = true;
                    }
                    else
                    {
                        isImage = false;
                    }
                }
                else
                {
                    isImage = false;
                }
                var res = dao.Edit(product, isImage);
                if (res)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Cập nhật thất bại", "error");
                    return RedirectToAction("Index", "Product");
                }
            }
            else
            {
                ViewBag.Categorys_Id = new SelectList(dao.GetListAll(), "CategoryID", "CategoryName", product.CategoryID);
                return View(product);
            }
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var dao = new ProductDAO();
            dao.Delete(id);
            return Json(new
            {
                Status = true
            });
        }

    }
}