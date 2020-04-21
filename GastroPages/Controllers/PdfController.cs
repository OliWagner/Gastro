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
            PdfHelper.MakePdfGetränke(new Models.HomeGetränkeModel());
            PdfHelper.MakePdfMittagstisch(new Models.HomeMittagstischModel());
            PdfHelper.MakePdfAllergene(new Models.HomeAllergeneModel());
            return View();
        }

        [HttpPost]
        public ActionResult Planer()
        {

            PdfHelper.MakePdfPlaner(Request);
            return View();
        }
    }
}