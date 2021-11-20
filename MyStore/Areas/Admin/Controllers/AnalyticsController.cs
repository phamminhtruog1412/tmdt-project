using Model.DAO;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Areas.Admin.Controllers
{
    public class AnalyticsController : Controller
    {
        /// <summary>
        /// Thống kê sản phẩm bán chạy
        /// </summary>
        public ActionResult ProductQuantitySold(int page = 1, int pageSize = 10)
        {
            return View(new ProductDAO().GetBestSellerProduct().ToPagedList(page, pageSize));
        }

        /// <summary>
        /// Thống kê sản phẩm tồn kho
        /// </summary>
        public ActionResult ProductQuantityInStock(int page = 1, int pageSize = 10)
        {
            return View(new ProductDAO().GetProductInStock().ToPagedList(page, pageSize));
        }

        public ActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Order_Statistics_ByDay(string fromDate, string toDate)
        {
            var fDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var tDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var res = new OrderDAO().GetAll().GroupBy(_ => _.CreatedDate.Date).Select(_ => new OrderCountViewModel { date = _.Key, count = _.Count()  }).ToList();
            var allDate = new List<ChartsDataViewModel>();

            var totalDays = (tDate - fDate).TotalDays;
            for (DateTime i = fDate; i <= tDate; i = i.AddDays(1))
            {
                var item = new ChartsDataViewModel();
                item.value = "0";
                foreach (var jtem in res)
                {
                    if (jtem.date.Date == i.Date.Date)
                        item.value = jtem.count.ToString();
                }
                item.date = i;

                allDate.Add(item);
            }

            return Json(new
            {
                label = "Thống kê đơn hàng",
                data = allDate
            }, JsonRequestBehavior.AllowGet);
        }
    }
}