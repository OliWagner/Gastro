using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
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
            return null;
        }

        public ActionResult PdfSpeisen()
        {
            PdfHelper.MakePdfSpeisen(new Models.HomeSpeisenModel());
            var fileStream = new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Speisen.pdf", FileMode.Open);
            var mimeType = "application/pdf";
            var fileDownloadName = "Speisen.pdf";
            return File(fileStream, mimeType, fileDownloadName);
        }

        public ActionResult PdfGetränke()
        {
            PdfHelper.MakePdfGetränke(new Models.HomeGetränkeModel());
            var fileStream = new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Getränke.pdf", FileMode.Open);
            var mimeType = "application/pdf";
            var fileDownloadName = "Getränke.pdf";
            return File(fileStream, mimeType, fileDownloadName);
        }

        public ActionResult PdfMittagstisch()
        {
            PdfHelper.MakePdfAllergene(new Models.HomeAllergeneModel());
            var fileStream = new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Mittagtisch.pdf", FileMode.Open);
            var mimeType = "application/pdf";
            var fileDownloadName = "Mittagtisch.pdf";
            return File(fileStream, mimeType, fileDownloadName);
        }

        public ActionResult PdfAllergene()
        {
            PdfHelper.MakePdfAllergene(new Models.HomeAllergeneModel());
            var fileStream = new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Allergene.pdf", FileMode.Open);
            var mimeType = "application/pdf";
            var fileDownloadName = "Allergene.pdf";
            return File(fileStream, mimeType, fileDownloadName);
        }
    }
}