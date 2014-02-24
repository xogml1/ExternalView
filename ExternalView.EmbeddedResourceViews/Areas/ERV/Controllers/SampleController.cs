using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExternalView.EmbeddedResourceViews.Areas.ERV.Controllers
{
    public class SampleController : Controller
    {
        //
        // GET: /ERV/Sample/

        public ActionResult Index()
        {
            return View();
        }

    }
}
