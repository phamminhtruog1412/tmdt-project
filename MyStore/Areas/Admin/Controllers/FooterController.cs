using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.Areas.Admin.Controllers
{
    public class FooterController : Controller
    {
        // GET: Admin/Footer
        public ActionResult Index()
        {
            return View();
        }
    }
}