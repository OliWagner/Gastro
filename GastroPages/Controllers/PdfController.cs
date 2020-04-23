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
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                
                PdfHelper.MakePdfSpeisen(new Models.HomeSpeisenModel());
                PdfHelper.MakePdfGetränke(new Models.HomeGetränkeModel());
                PdfHelper.MakePdfMittagstisch(new Models.HomeMittagstischModel());
                PdfHelper.MakePdfAllergene(new Models.HomeAllergeneModel());
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Planer()
        {
            PdfHelper.MakePdfPlaner(Request);
            return View();
        }

        public ActionResult PdfSpeisen()
        {
            PdfHelper.MakePdfPlaner(Request);
            //FileResult fr = DownloadPdfPlaner();
            System.Diagnostics.Process.Start("C:\\copy\\_Speisen.pdf");
            return RedirectToAction("Index", "Pdf");
        }

        public ActionResult PdfGetränke()
        {
            PdfHelper.MakePdfPlaner(Request);
            //FileResult fr = DownloadPdfPlaner();
            System.Diagnostics.Process.Start("C:\\copy\\_Getränke.pdf");
            return RedirectToAction("Index", "Pdf");
        }

        public ActionResult PdfMittagstisch()
        {
            PdfHelper.MakePdfPlaner(Request);
            //FileResult fr = DownloadPdfPlaner();
            System.Diagnostics.Process.Start("C:\\copy\\_Mittagtisch.pdf");
            return RedirectToAction("Index", "Pdf");
        }

        public ActionResult PdfAllergene()
        {
            PdfHelper.MakePdfPlaner(Request);
            //FileResult fr = DownloadPdfPlaner();
            System.Diagnostics.Process.Start("C:\\copy\\_Allergene.pdf");
            return RedirectToAction("Index", "Pdf");
        }
    }
}