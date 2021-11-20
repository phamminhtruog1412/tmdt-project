using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.ViewModel;
using Model.EF;
using MyStore.Assets.Constant;

namespace MyStore.Controllers
{
    public class ProductController : Controller
    {
        
        // GET: Product
        public ActionResult Index(long cateID)
        {
            ViewBag.ProductsByCategoryID = new ProductDAO().GetListByCategory(cateID,12);
            ViewBag.CateName = new ProductCategoryDAO().GetByID(cateID).CategoryName;
            return View();
        }

        public ActionResult ViewDetail(long proID)
        {
            var dao = new ProductDAO();
            dao.IncreaseView(proID);
            var model = dao.GetByID(proID);
            var session = Session[Constant.VIEWEDPRODUCT];

            if (session == null)
            {
                var list = new List<Product>();
                list.Add(model);
                Session[Constant.VIEWEDPRODUCT] = list;
            }
            else
            {
                var list = (List<Product>)session;
                if (list.Exists(x => x.ProductID == model.ProductID))
                {

                }
                else {
                    if (list.Count > 4)
                    {
                        list.RemoveAt(0);
                        list.Add(model);
                    }
                    else
                    {
                        list.Add(model);
                    }
                }

                Session[Constant.VIEWEDPRODUCT] = list;
            }

            ViewBag.listOfSuggestions = dao.GetSuggestionList(model.CategoryID, 5,proID);
            return View(model);
        }

        // Get all products
        public ActionResult GetProducts(int page = 1, int pageSize = 8)
        {
            int totalRecord = 0;
            var model = new ProductDAO().listProducts(ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize)); // ép kiểu
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }

        // Seach product by name
        public ActionResult SearchProducts(string name, int page = 1, int pageSize = 8)
        {
            int totalRecord = 0;
            var model = new ProductDAO().listByName(name, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize)); // ép kiểu
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
    }
}