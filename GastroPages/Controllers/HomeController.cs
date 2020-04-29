using GastroPages.Helpers;
using GastroPages.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GastroPages.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.ToString());
            //return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Planer()
        {
            HomeVeranstaltungsModel model = new HomeVeranstaltungsModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult VeranstaltungSpeichern()
        {
            PdfHelper.MakePdfPlaner(Request);
            //FileResult fr = DownloadPdfPlaner();
            System.Diagnostics.Process.Start("C:\\copy\\_Planer.pdf");
            return View();
        }

        public FileResult DownloadPdfPlaner()
        {
            var filePath = "C:\\copy\\_Planer.pdf";
            var pdfFileBytes = FileHelper.GetBytesFromFile(filePath);
            return File(pdfFileBytes, "application/pdf", "IhreAnfrage.pdf");
        }


        public ActionResult Karte()
        {
            HomeSpeisenModel model = new HomeSpeisenModel();
            return View(model);
        }

        //public ActionResult Reservierung()
        //{
        //    HomeReservierungModel model = new HomeReservierungModel();
        //    return View(model);
        //}


        public ActionResult Reservierung(HomeReservierungModel model)
        {
            if (model == null) {
                HomeReservierungModel m = new HomeReservierungModel();
                return View(m);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ReservierungEintragen(HomeReservierungModel model)
        {
            using (GastroEntities _db = new GastroEntities()) {
                Reservierungen reservierung = new Reservierungen();
                reservierung.Eingabedatum = DateTime.Now;
                reservierung.Nachricht = model.Mitteilung;
                reservierung.Personenzahl = int.Parse(model.Personenzahl.ToString());
                reservierung.Telefonnummer = model.Telefon;
                reservierung.Datum = model.Datum;
                reservierung.Uhrzeit = model.Uhrzeit;
                _db.Reservierungen.Add(reservierung);
                _db.SaveChanges();
            }
            
            return RedirectToAction("Reservierung", "Home", model);
        }

        public ActionResult Mittagstisch()
        {
            HomeMittagstischModel model = new HomeMittagstischModel();
            return View(model);
        }

        public ActionResult Impressum()
        {
            return View();
        }

        public ActionResult Räume()
        {
            return View();
        }

        public ActionResult Öffnungszeiten()
        {
            HomeÖffnungszeitenModel model = new HomeÖffnungszeitenModel();
            return View(model);
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginModel model)
        {
            using (GastroEntities _db = new GastroEntities()) {
                if (ModelState.IsValid)
                {
                    //Admin
                    foreach (var user in _db.Benutzer)
                    {
                        
                        if (user.Email == model.Email && user.Passwort == model.Password)
                        {
                            
                            //Session["UserId"] = model.Email;
                            //LogInLog log = new LogInLog
                            //{
                            //    UserId = model.Email,
                            //    Wann = DateTime.Now
                            //};
                            //_db.LogInLog.Add(log);
                            //_db.SaveChanges();
                            LoginHelper.SetSessions(_db, model.Email);
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                }
                    return View("LogOn");
            }
        }



    }
}