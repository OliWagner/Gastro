using GastroPages.Helpers;
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
    /// 8 - Öffnungszeiten
    ///     Die Tabelle Öffnungszeiten wird ein wenig vergewaltigt:
    ///         (Die Nummern sind als AllergenId in I18n zu hinterlegen)
    ///         Wochentag 1-7 sind die tatsächlichen Einträge der Öffnungszeiten
    ///         Wochentag 8 Öffnungszeiten Einführungstext (Vorwort)
    ///         Wochentag 9 Öffnungszeiten Schlusstext (Nachwort)
    ///         Wochentag 10 Reservierungen Einleitung
    ///         Wochentag 11 Reservierungen Wichtige Mitteilung
    ///         Wochentag 12 Impressum
    /// 9 - Kontakte
    /// 10 - Impressum
    /// 
    /// 1 - Deutsch
    /// 2 - Italienisch
    /// 3 - Spanisch
    /// 4 - Russisch
    /// 5 - Englisch
    /// </summary>

    public class I18nController : Controller
    {

        public ActionResult Kontakt()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nKontaktModel model = new AdminI18nKontaktModel();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult KontaktEintragen(AdminI18nKontaktModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == 1 && i18n.Typ == 9 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(I18nHelper.CreateInstance(9, 5, 1, " ", "", model.Englisch_Einleitung, model.Englisch_Nachrichtentext, "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(9, 2, 1, " ", "", model.Italienisch_Einleitung, model.Italienisch_Nachrichtentext, "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(9, 3, 1, " ", "", model.Spanisch_Einleitung, model.Spanisch_Nachrichtentext, "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(9, 4, 1, " ", "", model.Russisch_Einleitung, model.Russisch_Nachrichtentext, "", "", ""));

                    _db.SaveChanges();
                }

                return RedirectToAction("Kontakt", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

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
                using (GastroEntities _db = new GastroEntities()) {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.AllergenId && i18n.Typ == 1 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(I18nHelper.CreateInstance(1, 5, model.AllergenId, model.Englisch_Bezeichnung, model.Englisch_Beschreibung, "", "", "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(1, 2, model.AllergenId, model.Italienisch_Bezeichnung, model.Italienisch_Beschreibung, "", "", "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(1, 3, model.AllergenId, model.Spanisch_Bezeichnung, model.Spanisch_Beschreibung, "", "", "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(1, 4, model.AllergenId, model.Russisch_Bezeichnung, model.Russisch_Beschreibung, "", "", "", "", ""));
                    _db.SaveChanges();
                }

                return RedirectToAction("Allergene", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Öffnungszeiten()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nÖffnungszeitenModel model = new AdminI18nÖffnungszeitenModel();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ÖffnungszeitenEintragen(AdminI18nÖffnungszeitenModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                try
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        //erst löschen wenn vorhanden
                        List<I18n> liste = (from I18n i18n in _db.I18n where i18n.Typ == 8 select i18n).ToList();
                        _db.I18n.RemoveRange(liste);

                        _db.I18n.Add(I18nHelper.CreateInstance(8, 5, 1, " ", "", model.Englisch_Montag_Ergänzung1, model.Englisch_Montag_Ergänzung2, model.Englisch_Vorwort, "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 2, 1, " ", "", model.Italienisch_Montag_Ergänzung1, model.Italienisch_Montag_Ergänzung2, model.Italienisch_Vorwort, "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 3, 1, " ", "", model.Spanisch_Montag_Ergänzung1, model.Spanisch_Montag_Ergänzung2, model.Spanisch_Vorwort, "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 4, 1, " ", "", model.Russisch_Montag_Ergänzung1, model.Russisch_Montag_Ergänzung2, model.Russisch_Vorwort, "", ""));

                        _db.I18n.Add(I18nHelper.CreateInstance(8, 5, 2, " ", "", model.Englisch_Dienstag_Ergänzung1, model.Englisch_Dienstag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 2, 2, " ", "", model.Italienisch_Dienstag_Ergänzung1, model.Italienisch_Dienstag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 3, 2, " ", "", model.Spanisch_Dienstag_Ergänzung1, model.Spanisch_Dienstag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 4, 2, " ", "", model.Russisch_Dienstag_Ergänzung1, model.Russisch_Dienstag_Ergänzung2, "", "", ""));

                        _db.I18n.Add(I18nHelper.CreateInstance(8, 5, 3, " ", "", model.Englisch_Mittwoch_Ergänzung1, model.Englisch_Mittwoch_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 2, 3, " ", "", model.Italienisch_Mittwoch_Ergänzung1, model.Italienisch_Mittwoch_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 3, 3, " ", "", model.Spanisch_Mittwoch_Ergänzung1, model.Spanisch_Mittwoch_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 4, 3, " ", "", model.Russisch_Mittwoch_Ergänzung1, model.Russisch_Mittwoch_Ergänzung2, "", "", ""));

                        _db.I18n.Add(I18nHelper.CreateInstance(8, 5, 4, " ", "", model.Englisch_Donnerstag_Ergänzung1, model.Englisch_Donnerstag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 2, 4, " ", "", model.Italienisch_Donnerstag_Ergänzung1, model.Italienisch_Donnerstag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 3, 4, " ", "", model.Spanisch_Donnerstag_Ergänzung1, model.Spanisch_Donnerstag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 4, 4, " ", "", model.Russisch_Donnerstag_Ergänzung1, model.Russisch_Donnerstag_Ergänzung2, "", "", ""));

                        _db.I18n.Add(I18nHelper.CreateInstance(8, 5, 5, " ", "", model.Englisch_Freitag_Ergänzung1, model.Englisch_Freitag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 2, 5, " ", "", model.Italienisch_Freitag_Ergänzung1, model.Italienisch_Freitag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 3, 5, " ", "", model.Spanisch_Freitag_Ergänzung1, model.Spanisch_Freitag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 4, 5, " ", "", model.Russisch_Freitag_Ergänzung1, model.Russisch_Freitag_Ergänzung2, "", "", ""));

                        _db.I18n.Add(I18nHelper.CreateInstance(8, 5, 6, " ", "", model.Englisch_Samstag_Ergänzung1, model.Englisch_Samstag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 2, 6, " ", "", model.Italienisch_Samstag_Ergänzung1, model.Italienisch_Samstag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 3, 6, " ", "", model.Spanisch_Samstag_Ergänzung1, model.Spanisch_Samstag_Ergänzung2, "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 4, 6, " ", "", model.Russisch_Samstag_Ergänzung1, model.Russisch_Samstag_Ergänzung2, "", "", ""));

                        _db.I18n.Add(I18nHelper.CreateInstance(8, 5, 7, " ", "", model.Englisch_Sonntag_Ergänzung1, model.Englisch_Sonntag_Ergänzung2, model.Englisch_Nachwort, "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 2, 7, " ", "", model.Italienisch_Sonntag_Ergänzung1, model.Italienisch_Sonntag_Ergänzung2, model.Italienisch_Nachwort, "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 3, 7, " ", "", model.Spanisch_Sonntag_Ergänzung1, model.Spanisch_Sonntag_Ergänzung2, model.Spanisch_Nachwort, "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(8, 4, 7, " ", "", model.Russisch_Sonntag_Ergänzung1, model.Russisch_Sonntag_Ergänzung2, model.Russisch_Nachwort, "", ""));

                        _db.SaveChanges();
                    }
                }
                catch (Exception ex) {
                    var i = 0;
                }
                return RedirectToAction("Öffnungszeiten", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Reservierung()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminI18nReservierungModel model = new AdminI18nReservierungModel();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ReservierungEintragen(AdminI18nReservierungModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                try
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        //erst löschen wenn vorhanden
                        List<I18n> liste = (from I18n i18n in _db.I18n where i18n.Typ == 9 select i18n).ToList();
                        _db.I18n.RemoveRange(liste);

                        _db.I18n.Add(I18nHelper.CreateInstance(9, 5, 10, " ", "", model.Englisch_Ansprache, "", "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(9, 2, 10, " ", "", model.Italienisch_Ansprache, "", "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(9, 3, 10, " ", "", model.Spanisch_Ansprache, "", "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(9, 4, 10, " ", "", model.Russisch_Ansprache, "", "", "", ""));

                        _db.I18n.Add(I18nHelper.CreateInstance(9, 5, 11, " ", "", model.Englisch_WichtigerHinweis, "", "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(9, 2, 11, " ", "", model.Italienisch_WichtigerHinweis, "", "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(9, 3, 11, " ", "", model.Spanisch_WichtigerHinweis, "", "", "", ""));
                        _db.I18n.Add(I18nHelper.CreateInstance(9, 4, 11, " ", "", model.Russisch_WichtigerHinweis, "", "", "", ""));

                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    var i = 0;
                }
                return RedirectToAction("Reservierung", "Admin");
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
                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.MittagstischId && i18n.Typ == 4 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(I18nHelper.CreateInstance(4, 5, model.MittagstischId, model.Englisch_Bezeichnung, model.Englisch_Beschreibung, "", "", "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(4, 2, model.MittagstischId, model.Italienisch_Bezeichnung, model.Italienisch_Beschreibung, "", "", "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(4, 3, model.MittagstischId, model.Spanisch_Bezeichnung, model.Spanisch_Beschreibung, "", "", "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(4, 4, model.MittagstischId, model.Russisch_Bezeichnung, model.Russisch_Beschreibung, "", "", "", "", ""));
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
                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.SpeisenId && i18n.Typ == 5 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(I18nHelper.CreateInstance(5, 5, model.SpeisenId, model.Englisch_Bezeichnung, model.Englisch_Beschreibung, "", "", "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(5, 2, model.SpeisenId, model.Italienisch_Bezeichnung, model.Italienisch_Beschreibung, "", "", "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(5, 3, model.SpeisenId, model.Spanisch_Bezeichnung, model.Spanisch_Beschreibung, "", "", "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(5, 4, model.SpeisenId, model.Russisch_Bezeichnung, model.Russisch_Beschreibung, "", "", "", "", ""));
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
               using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.GetränkId && i18n.Typ == 2 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(I18nHelper.CreateInstance(2, 5, model.GetränkId, model.Englisch_Bezeichnung, "", model.Englisch_Ergänzung1, model.Englisch_Ergänzung2, "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(2, 2, model.GetränkId, model.Italienisch_Bezeichnung, "", model.Italienisch_Ergänzung1, model.Italienisch_Ergänzung2, "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(2, 3, model.GetränkId, model.Spanisch_Bezeichnung, "", model.Spanisch_Ergänzung1, model.Spanisch_Ergänzung2, "", "", ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(2, 4, model.GetränkId, model.Russisch_Bezeichnung, "", model.Russisch_Ergänzung1, model.Russisch_Ergänzung2, "", "", ""));
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
                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.KategorieId && i18n.Typ == 3 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(I18nHelper.CreateInstance(3, 5, model.KategorieId, model.Englisch_Bezeichnung, "", "", "", model.Englisch_Header, model.Englisch_Footer, ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(3, 2, model.KategorieId, model.Italienisch_Bezeichnung, "", "", "", model.Italienisch_Header, model.Italienisch_Footer, ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(3, 3, model.KategorieId, model.Spanisch_Bezeichnung, "", "", "", model.Spanisch_Header, model.Spanisch_Footer, ""));
                    _db.I18n.Add(I18nHelper.CreateInstance(3, 4, model.KategorieId, model.Russisch_Bezeichnung, "", "", "", model.Russisch_Header, model.Russisch_Footer, ""));
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
                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.GetränkId && i18n.Typ == 6 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(I18nHelper.CreateInstance(6, 5, model.GetränkId, model.Englisch_Bezeichnung, model.Englisch_Beschreibung, "", "", "", "", model.Englisch_Einheit));
                    _db.I18n.Add(I18nHelper.CreateInstance(6, 2, model.GetränkId, model.Italienisch_Bezeichnung, model.Italienisch_Beschreibung, "", "", "", "", model.Italienisch_Einheit));
                    _db.I18n.Add(I18nHelper.CreateInstance(6, 3, model.GetränkId, model.Spanisch_Bezeichnung, model.Spanisch_Beschreibung, "", "", "", "", model.Spanisch_Einheit));
                    _db.I18n.Add(I18nHelper.CreateInstance(6, 4, model.GetränkId, model.Russisch_Bezeichnung, model.Russisch_Beschreibung, "", "", "", "", model.Russisch_Einheit));
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
                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.AllergenId == model.GetränkId && i18n.Typ == 6 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(I18nHelper.CreateInstance(7, 5, model.GetränkId, model.Englisch_Bezeichnung, model.Englisch_Beschreibung, "", "", "", "", model.Englisch_Einheit));
                    _db.I18n.Add(I18nHelper.CreateInstance(7, 2, model.GetränkId, model.Italienisch_Bezeichnung, model.Italienisch_Beschreibung, "", "", "", "", model.Italienisch_Einheit));
                    _db.I18n.Add(I18nHelper.CreateInstance(7, 3, model.GetränkId, model.Spanisch_Bezeichnung, model.Spanisch_Beschreibung, "", "", "", "", model.Spanisch_Einheit));
                    _db.I18n.Add(I18nHelper.CreateInstance(7, 4, model.GetränkId, model.Russisch_Bezeichnung, model.Russisch_Beschreibung, "", "", "", "", model.Russisch_Einheit));
                    _db.SaveChanges();
                }

                return RedirectToAction("VeranstaltungsSpeisen", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}