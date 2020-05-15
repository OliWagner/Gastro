using GastroPages.Helpers;
using GastroPages.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public ActionResult Umfrage()
        {
            return View();
        }

        public ActionResult Contact()
        {
                return View(new HomeKontakteModel());
        }

        public ActionResult News(int? id)
        {
            if (id == null) {
                return View(new HomeNewsModel());
            }
            return View(new HomeNewsModel((int)id));
        }

        public ActionResult KontaktEintragen()
        {
            Session["kontakt"] = "";
            string nachricht = Request["txtNachricht"];
            string name = Request["name"];
            string telefon = Request["telefon"];
            string email = Request["email"];
            if (nachricht != null && !nachricht.Equals("") && name != null && !name.Equals("")) {
                using (GastroEntities db = new GastroEntities()) {
                    Kontakte kon = new Kontakte();
                    kon.Nachricht = nachricht;
                    kon.Name = name;
                    kon.Telefon = telefon;
                    kon.Email = email;
                    kon.Datum = DateTime.Now;
                    db.Kontakte.Add(kon);
                    db.SaveChanges();
                    Session["kontakt"] = ResourcesGastro.Home.Logon.KontaktBestätigung;
                }
            }
            return RedirectToAction("Contact", "Home");
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
            if (Session["pdfguid"] != null && !Session["pdfguid"].Equals(""))
            {
                using (GastroEntities db = new GastroEntities()) {
                    var dsvgoName = Request["dsgvoName"];
                    var dsvgoDatum = Request["dsgvoDatum"];
                    string anzahlPersonenInsgesamt = Request["anzahlPersonenInsgesamt"];
                    var SummarySummeSpeisen = Request["summarySummeSpeisenValue"];
                    var SummarySummeGetränke = Request["summarySummeGetränkeValue"];
                    var SummarySummeGesamt = Request["summarySummeGesamtValue"];

                    Veranstaltungen ver = new Veranstaltungen();
                    ver.Name = dsvgoName;
                    ver.VeranstaltungsDatum = dsvgoDatum;
                    ver.Guid = Session["pdfguid"].ToString();
                    ver.Personenzahl = anzahlPersonenInsgesamt;
                    ver.EingabeDatum = DateTime.Now;
                    ver.SummeSpeisen = SummarySummeSpeisen;
                    ver.SummeGetränke = SummarySummeGetränke;
                    ver.SUmmeGesamt = SummarySummeGesamt;
                    db.Veranstaltungen.Add(ver);
                    db.SaveChanges();
                }
                var fileStream = new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Planer_" + Session["pdfguid"] + ".pdf", FileMode.Open);
                var mimeType = "application/pdf";
                var fileDownloadName = "_Planer_" + Session["pdfguid"] + ".pdf";
                Session["pdfguid"] = "";
                return File(fileStream, mimeType, fileDownloadName);
            }
            else
            {
                //System.Diagnostics.Process.Start("C:\\copy\\_Planer.pdf"); 
                var fileStream = new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Planer.pdf", FileMode.Open);
                var mimeType = "application/pdf";
                var fileDownloadName = "_Planer.pdf";
                return File(fileStream, mimeType, fileDownloadName);
            }
            
            return RedirectToAction("Planer", "Home", new HomeVeranstaltungsModel());
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
                reservierung.Name = model.Name;
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
            return View(new HomeKontakteModel());
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