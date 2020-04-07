using GastroPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GastroPages.Controllers
{
    /// <summary>
    /// 1 - Allergene
    /// 2 - Getränke
    /// 3 - Kategorien
    /// 4 - Mittagstisch
    /// 5 - Speisen
    /// 6 - Veranstaltungsgetränke
    /// 7 - Veranstaltungsspeisen
    /// 
    /// 1 - Deutsch
    /// 2 - Italienisch
    /// 3 - Spanisch
    /// 4 - Russisch
    /// 5 - Englisch
    /// </summary>

    public class I18nController : Controller
    {
        // GET: I18n
        public ActionResult Allergene(int id)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nAllergeneModel model = new AdminI18nAllergeneModel(1, id);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AllergeneEintragen(AdminI18nAllergeneModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                I18n eintragEnglisch = new I18n();
                eintragEnglisch.Typ = 1;
                eintragEnglisch.SprachId = 5;
                eintragEnglisch.Bezeichnung = model.Englisch_Bezeichnung;
                eintragEnglisch.Beschreibung = model.Englisch_Beschreibung;
                eintragEnglisch.AllergenId = model.AllergenId;

                I18n eintragSpanisch = new I18n();
                eintragSpanisch.Typ = 1;
                eintragSpanisch.SprachId = 3;
                eintragSpanisch.Bezeichnung = model.Spanisch_Bezeichnung;
                eintragSpanisch.Beschreibung = model.Spanisch_Beschreibung;
                eintragSpanisch.AllergenId = model.AllergenId;

                I18n eintragItalienisch = new I18n();
                eintragItalienisch.Typ = 1;
                eintragItalienisch.SprachId = 2;
                eintragItalienisch.Bezeichnung = model.Italienisch_Bezeichnung;
                eintragItalienisch.Beschreibung = model.Italienisch_Beschreibung;
                eintragItalienisch.AllergenId = model.AllergenId;

                I18n eintragRussisch = new I18n();
                eintragRussisch.Typ = 1;
                eintragRussisch.SprachId = 4;
                eintragRussisch.Bezeichnung = model.Russisch_Bezeichnung;
                eintragRussisch.Beschreibung = model.Russisch_Beschreibung;
                eintragRussisch.AllergenId = model.AllergenId;

                using (GastroEntities _db = new GastroEntities()) {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.AllergenId && i18n.Typ == 1 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(eintragEnglisch);
                    _db.I18n.Add(eintragItalienisch);
                    _db.I18n.Add(eintragSpanisch);
                    _db.I18n.Add(eintragRussisch);
                    _db.SaveChanges();
                }

                return RedirectToAction("Allergene", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Mittagstisch(int id)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nMittagstischModel model = new AdminI18nMittagstischModel(4, id);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult MittagstischEintragen(AdminI18nMittagstischModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                I18n eintragEnglisch = new I18n();
                eintragEnglisch.Typ = 4;
                eintragEnglisch.SprachId = 5;
                eintragEnglisch.Bezeichnung = model.Englisch_Bezeichnung;
                eintragEnglisch.Beschreibung = model.Englisch_Beschreibung;
                eintragEnglisch.AllergenId = model.MittagstischId;

                I18n eintragSpanisch = new I18n();
                eintragSpanisch.Typ = 4;
                eintragSpanisch.SprachId = 3;
                eintragSpanisch.Bezeichnung = model.Spanisch_Bezeichnung;
                eintragSpanisch.Beschreibung = model.Spanisch_Beschreibung;
                eintragSpanisch.AllergenId = model.MittagstischId;

                I18n eintragItalienisch = new I18n();
                eintragItalienisch.Typ = 4;
                eintragItalienisch.SprachId = 2;
                eintragItalienisch.Bezeichnung = model.Italienisch_Bezeichnung;
                eintragItalienisch.Beschreibung = model.Italienisch_Beschreibung;
                eintragItalienisch.AllergenId = model.MittagstischId;

                I18n eintragRussisch = new I18n();
                eintragRussisch.Typ = 4;
                eintragRussisch.SprachId = 4;
                eintragRussisch.Bezeichnung = model.Russisch_Bezeichnung;
                eintragRussisch.Beschreibung = model.Russisch_Beschreibung;
                eintragRussisch.AllergenId = model.MittagstischId;

                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.MittagstischId && i18n.Typ == 4 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(eintragEnglisch);
                    _db.I18n.Add(eintragItalienisch);
                    _db.I18n.Add(eintragSpanisch);
                    _db.I18n.Add(eintragRussisch);
                    _db.SaveChanges();
                }

                return RedirectToAction("Mittagstisch", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Speisen(int id)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nSpeisenModel model = new AdminI18nSpeisenModel(5, id);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SpeisenEintragen(AdminI18nSpeisenModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                I18n eintragEnglisch = new I18n();
                eintragEnglisch.Typ = 5;
                eintragEnglisch.SprachId = 5;
                eintragEnglisch.Bezeichnung = model.Englisch_Bezeichnung;
                eintragEnglisch.Beschreibung = model.Englisch_Beschreibung;
                eintragEnglisch.AllergenId = model.SpeisenId;

                I18n eintragSpanisch = new I18n();
                eintragSpanisch.Typ = 5;
                eintragSpanisch.SprachId = 3;
                eintragSpanisch.Bezeichnung = model.Spanisch_Bezeichnung;
                eintragSpanisch.Beschreibung = model.Spanisch_Beschreibung;
                eintragSpanisch.AllergenId = model.SpeisenId;

                I18n eintragItalienisch = new I18n();
                eintragItalienisch.Typ = 5;
                eintragItalienisch.SprachId = 2;
                eintragItalienisch.Bezeichnung = model.Italienisch_Bezeichnung;
                eintragItalienisch.Beschreibung = model.Italienisch_Beschreibung;
                eintragItalienisch.AllergenId = model.SpeisenId;

                I18n eintragRussisch = new I18n();
                eintragRussisch.Typ = 5;
                eintragRussisch.SprachId = 4;
                eintragRussisch.Bezeichnung = model.Russisch_Bezeichnung;
                eintragRussisch.Beschreibung = model.Russisch_Beschreibung;
                eintragRussisch.AllergenId = model.SpeisenId;

                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.SpeisenId && i18n.Typ == 5 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(eintragEnglisch);
                    _db.I18n.Add(eintragItalienisch);
                    _db.I18n.Add(eintragSpanisch);
                    _db.I18n.Add(eintragRussisch);
                    _db.SaveChanges();
                }

                return RedirectToAction("Speisen", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Getränke(int id)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nGetränkeModel model = new AdminI18nGetränkeModel(2, id);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult GetränkeEintragen(AdminI18nGetränkeModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                I18n eintragEnglisch = new I18n();
                eintragEnglisch.Typ = 2;
                eintragEnglisch.SprachId = 5;
                eintragEnglisch.Bezeichnung = model.Englisch_Bezeichnung;
                eintragEnglisch.Ergänzung1 = model.Englisch_Ergänzung1;
                eintragEnglisch.Ergänzung2 = model.Englisch_Ergänzung2;
                eintragEnglisch.AllergenId = model.GetränkId;

                I18n eintragSpanisch = new I18n();
                eintragSpanisch.Typ = 2;
                eintragSpanisch.SprachId = 3;
                eintragSpanisch.Bezeichnung = model.Spanisch_Bezeichnung;
                eintragSpanisch.Ergänzung1 = model.Spanisch_Ergänzung1;
                eintragSpanisch.Ergänzung2 = model.Spanisch_Ergänzung2;
                eintragSpanisch.AllergenId = model.GetränkId;

                I18n eintragItalienisch = new I18n();
                eintragItalienisch.Typ = 2;
                eintragItalienisch.SprachId = 2;
                eintragItalienisch.Bezeichnung = model.Italienisch_Bezeichnung;
                eintragItalienisch.Ergänzung1 = model.Italienisch_Ergänzung1;
                eintragItalienisch.Ergänzung2 = model.Italienisch_Ergänzung2;
                eintragItalienisch.AllergenId = model.GetränkId;

                I18n eintragRussisch = new I18n();
                eintragRussisch.Typ = 2;
                eintragRussisch.SprachId = 4;
                eintragRussisch.Bezeichnung = model.Russisch_Bezeichnung;
                eintragRussisch.Ergänzung1 = model.Russisch_Ergänzung1;
                eintragRussisch.Ergänzung2 = model.Russisch_Ergänzung2;
                eintragRussisch.AllergenId = model.GetränkId;

                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.GetränkId && i18n.Typ == 2 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(eintragEnglisch);
                    _db.I18n.Add(eintragItalienisch);
                    _db.I18n.Add(eintragSpanisch);
                    _db.I18n.Add(eintragRussisch);
                    _db.SaveChanges();
                }

                return RedirectToAction("Getränke", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Kategorien(int id, int art)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nKategorienModel model = new AdminI18nKategorienModel(3, id, art);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult KategorienEintragen(AdminI18nKategorienModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                I18n eintragEnglisch = new I18n();
                eintragEnglisch.Typ = 3;
                eintragEnglisch.SprachId = 5;
                eintragEnglisch.Bezeichnung = model.Englisch_Bezeichnung;
                eintragEnglisch.Header = model.Englisch_Header;
                eintragEnglisch.Footer = model.Englisch_Footer;
                eintragEnglisch.AllergenId = model.KategorieId;

                I18n eintragSpanisch = new I18n();
                eintragSpanisch.Typ = 3;
                eintragSpanisch.SprachId = 3;
                eintragSpanisch.Bezeichnung = model.Spanisch_Bezeichnung;
                eintragSpanisch.Header = model.Spanisch_Header;
                eintragSpanisch.Footer = model.Spanisch_Footer;
                eintragSpanisch.AllergenId = model.KategorieId;

                I18n eintragItalienisch = new I18n();
                eintragItalienisch.Typ = 3;
                eintragItalienisch.SprachId = 2;
                eintragItalienisch.Bezeichnung = model.Italienisch_Bezeichnung;
                eintragItalienisch.Header = model.Italienisch_Header;
                eintragItalienisch.Footer = model.Italienisch_Footer;
                eintragItalienisch.AllergenId = model.KategorieId;

                I18n eintragRussisch = new I18n();
                eintragRussisch.Typ = 3;
                eintragRussisch.SprachId = 4;
                eintragRussisch.Bezeichnung = model.Russisch_Bezeichnung;
                eintragRussisch.Header = model.Russisch_Header;
                eintragRussisch.Footer = model.Russisch_Footer;
                eintragRussisch.AllergenId = model.KategorieId;

                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.KategorieId && i18n.Typ == 3 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(eintragEnglisch);
                    _db.I18n.Add(eintragItalienisch);
                    _db.I18n.Add(eintragSpanisch);
                    _db.I18n.Add(eintragRussisch);
                    _db.SaveChanges();
                }

                switch (model.KategorieArt) {
                    case 1:
                        return RedirectToAction("Speisen", "Admin");
                        
                    case 2:
                        return RedirectToAction("Getränke", "Admin");

                    case 3:
                        return RedirectToAction("Veranstaltungsgetränke", "Admin");
                    case 4:
                        return RedirectToAction("Veranstaltungsspeisen", "Admin");
                    case 5:
                        return RedirectToAction("Mittagstisch", "Admin");
                }
                
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult VeranstaltungsGetränke(int id)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nVeranstaltungsGetränkeModel model = new AdminI18nVeranstaltungsGetränkeModel(6, id);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult VeranstaltungsGetränkeEintragen(AdminI18nVeranstaltungsGetränkeModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                I18n eintragEnglisch = new I18n();
                eintragEnglisch.Typ = 6;
                eintragEnglisch.SprachId = 5;
                eintragEnglisch.Bezeichnung = model.Englisch_Bezeichnung;
                eintragEnglisch.Beschreibung = model.Englisch_Beschreibung;
                eintragEnglisch.Einheit = model.Englisch_Einheit;
                eintragEnglisch.AllergenId = model.GetränkId;

                I18n eintragSpanisch = new I18n();
                eintragSpanisch.Typ = 6;
                eintragSpanisch.SprachId = 3;
                eintragSpanisch.Bezeichnung = model.Spanisch_Bezeichnung;
                eintragSpanisch.Beschreibung = model.Spanisch_Beschreibung;
                eintragSpanisch.Einheit = model.Spanisch_Einheit;
                eintragSpanisch.AllergenId = model.GetränkId;

                I18n eintragItalienisch = new I18n();
                eintragItalienisch.Typ = 6;
                eintragItalienisch.SprachId = 2;
                eintragItalienisch.Bezeichnung = model.Italienisch_Bezeichnung;
                eintragItalienisch.Beschreibung = model.Italienisch_Beschreibung;
                eintragItalienisch.Einheit = model.Italienisch_Einheit;
                eintragItalienisch.AllergenId = model.GetränkId;

                I18n eintragRussisch = new I18n();
                eintragRussisch.Typ = 6;
                eintragRussisch.SprachId = 4;
                eintragRussisch.Bezeichnung = model.Russisch_Bezeichnung;
                eintragRussisch.Beschreibung = model.Russisch_Beschreibung;
                eintragRussisch.Einheit = model.Russisch_Einheit;
                eintragRussisch.AllergenId = model.GetränkId;

                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.GetränkId && i18n.Typ == 6 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(eintragEnglisch);
                    _db.I18n.Add(eintragItalienisch);
                    _db.I18n.Add(eintragSpanisch);
                    _db.I18n.Add(eintragRussisch);
                    _db.SaveChanges();
                }

                return RedirectToAction("VeranstaltungsGetränke", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult VeranstaltungsSpeisen(int id)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nVeranstaltungsSpeisenModel model = new AdminI18nVeranstaltungsSpeisenModel(7, id);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult VeranstaltungsSpeisenEintragen(AdminI18nVeranstaltungsGetränkeModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                I18n eintragEnglisch = new I18n();
                eintragEnglisch.Typ = 7;
                eintragEnglisch.SprachId = 5;
                eintragEnglisch.Bezeichnung = model.Englisch_Bezeichnung;
                eintragEnglisch.Beschreibung = model.Englisch_Beschreibung;
                eintragEnglisch.Einheit = model.Englisch_Einheit;
                eintragEnglisch.AllergenId = model.GetränkId;

                I18n eintragSpanisch = new I18n();
                eintragSpanisch.Typ = 7;
                eintragSpanisch.SprachId = 3;
                eintragSpanisch.Bezeichnung = model.Spanisch_Bezeichnung;
                eintragSpanisch.Beschreibung = model.Spanisch_Beschreibung;
                eintragSpanisch.Einheit = model.Spanisch_Einheit;
                eintragSpanisch.AllergenId = model.GetränkId;

                I18n eintragItalienisch = new I18n();
                eintragItalienisch.Typ = 7;
                eintragItalienisch.SprachId = 2;
                eintragItalienisch.Bezeichnung = model.Italienisch_Bezeichnung;
                eintragItalienisch.Beschreibung = model.Italienisch_Beschreibung;
                eintragItalienisch.Einheit = model.Italienisch_Einheit;
                eintragItalienisch.AllergenId = model.GetränkId;

                I18n eintragRussisch = new I18n();
                eintragRussisch.Typ = 7;
                eintragRussisch.SprachId = 4;
                eintragRussisch.Bezeichnung = model.Russisch_Bezeichnung;
                eintragRussisch.Beschreibung = model.Russisch_Beschreibung;
                eintragRussisch.Einheit = model.Russisch_Einheit;
                eintragRussisch.AllergenId = model.GetränkId;

                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.GetränkId && i18n.Typ == 6 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(eintragEnglisch);
                    _db.I18n.Add(eintragItalienisch);
                    _db.I18n.Add(eintragSpanisch);
                    _db.I18n.Add(eintragRussisch);
                    _db.SaveChanges();
                }

                return RedirectToAction("VeranstaltungsSpeisen", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}