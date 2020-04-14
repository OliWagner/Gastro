using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GastroPages.Controllers
{
    public class PdfController : BaseController
    {
        // GET: Pdf
        public ActionResult Index()
        {
            PdfHelper.MakePdfSpeisen(new Models.HomeSpeisenModel());
            return View();
        }
    }
}