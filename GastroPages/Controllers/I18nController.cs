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
                I18n eintragEnglischMontag = new I18n();
                eintragEnglischMontag.Typ = 8;
                eintragEnglischMontag.SprachId = 5;
                eintragEnglischMontag.Ergänzung1 = model.Englisch_Montag_Ergänzung1;
                eintragEnglischMontag.Ergänzung2 = model.Englisch_Montag_Ergänzung2;
                eintragEnglischMontag.AllergenId = 1;
                eintragEnglischMontag.Header = model.Englisch_Vorwort;

                I18n eintragSpanischMontag = new I18n();
                eintragSpanischMontag.Typ = 8;
                eintragSpanischMontag.SprachId = 3;
                eintragSpanischMontag.Ergänzung1 = model.Spanisch_Montag_Ergänzung1;
                eintragSpanischMontag.Ergänzung2 = model.Spanisch_Montag_Ergänzung2;
                eintragSpanischMontag.AllergenId = 1;
                eintragSpanischMontag.Header = model.Spanisch_Vorwort;

                I18n eintragItalienischMontag = new I18n();
                eintragItalienischMontag.Typ = 8;
                eintragItalienischMontag.SprachId = 2;
                eintragItalienischMontag.Ergänzung1 = model.Italienisch_Montag_Ergänzung1;
                eintragItalienischMontag.Ergänzung2 = model.Italienisch_Montag_Ergänzung2;
                eintragItalienischMontag.AllergenId = 1;
                eintragItalienischMontag.Header = model.Italienisch_Vorwort;

                I18n eintragRussischMontag = new I18n();
                eintragRussischMontag.Typ = 8;
                eintragRussischMontag.SprachId = 4;
                eintragRussischMontag.Ergänzung1 = model.Russisch_Montag_Ergänzung1;
                eintragRussischMontag.Ergänzung2 = model.Russisch_Montag_Ergänzung2;
                eintragRussischMontag.AllergenId = 1;
                eintragRussischMontag.Header = model.Russisch_Vorwort;

                I18n eintragEnglischDienstag = new I18n();
                eintragEnglischDienstag.Typ = 8;
                eintragEnglischDienstag.SprachId = 5;
                eintragEnglischDienstag.Ergänzung1 = model.Englisch_Dienstag_Ergänzung1;
                eintragEnglischDienstag.Ergänzung2 = model.Englisch_Dienstag_Ergänzung2;
                eintragEnglischDienstag.AllergenId = 1;

                I18n eintragSpanischDienstag = new I18n();
                eintragSpanischDienstag.Typ = 8;
                eintragSpanischDienstag.SprachId = 3;
                eintragSpanischDienstag.Ergänzung1 = model.Spanisch_Dienstag_Ergänzung1;
                eintragSpanischDienstag.Ergänzung2 = model.Spanisch_Dienstag_Ergänzung2;
                eintragSpanischDienstag.AllergenId = 1;

                I18n eintragItalienischDienstag = new I18n();
                eintragItalienischDienstag.Typ = 8;
                eintragItalienischDienstag.SprachId = 2;
                eintragItalienischDienstag.Ergänzung1 = model.Italienisch_Dienstag_Ergänzung1;
                eintragItalienischDienstag.Ergänzung2 = model.Italienisch_Dienstag_Ergänzung2;
                eintragItalienischDienstag.AllergenId = 1;

                I18n eintragRussischDienstag = new I18n();
                eintragRussischDienstag.Typ = 8;
                eintragRussischDienstag.SprachId = 4;
                eintragRussischDienstag.Ergänzung1 = model.Russisch_Dienstag_Ergänzung1;
                eintragRussischDienstag.Ergänzung2 = model.Russisch_Dienstag_Ergänzung2;
                eintragRussischDienstag.AllergenId = 1;

                I18n eintragEnglischMittwoch = new I18n();
                eintragEnglischMittwoch.Typ = 8;
                eintragEnglischMittwoch.SprachId = 5;
                eintragEnglischMittwoch.Ergänzung1 = model.Englisch_Mittwoch_Ergänzung1;
                eintragEnglischMittwoch.Ergänzung2 = model.Englisch_Mittwoch_Ergänzung2;
                eintragEnglischMittwoch.AllergenId = 1;

                I18n eintragSpanischMittwoch = new I18n();
                eintragSpanischMittwoch.Typ = 8;
                eintragSpanischMittwoch.SprachId = 3;
                eintragSpanischMittwoch.Ergänzung1 = model.Spanisch_Mittwoch_Ergänzung1;
                eintragSpanischMittwoch.Ergänzung2 = model.Spanisch_Mittwoch_Ergänzung2;
                eintragSpanischMittwoch.AllergenId = 1;

                I18n eintragItalienischMittwoch = new I18n();
                eintragItalienischMittwoch.Typ = 8;
                eintragItalienischMittwoch.SprachId = 2;
                eintragItalienischMittwoch.Ergänzung1 = model.Italienisch_Mittwoch_Ergänzung1;
                eintragItalienischMittwoch.Ergänzung2 = model.Italienisch_Mittwoch_Ergänzung2;
                eintragItalienischMittwoch.AllergenId = 1;

                I18n eintragRussischMittwoch = new I18n();
                eintragRussischMittwoch.Typ = 8;
                eintragRussischMittwoch.SprachId = 4;
                eintragRussischMittwoch.Ergänzung1 = model.Russisch_Mittwoch_Ergänzung1;
                eintragRussischMittwoch.Ergänzung2 = model.Russisch_Mittwoch_Ergänzung2;
                eintragRussischMittwoch.AllergenId = 1;

                I18n eintragEnglischDonnerstag = new I18n();
                eintragEnglischDonnerstag.Typ = 8;
                eintragEnglischDonnerstag.SprachId = 5;
                eintragEnglischDonnerstag.Ergänzung1 = model.Englisch_Donnerstag_Ergänzung1;
                eintragEnglischDonnerstag.Ergänzung2 = model.Englisch_Donnerstag_Ergänzung2;
                eintragEnglischDonnerstag.AllergenId = 1;

                I18n eintragSpanischDonnerstag = new I18n();
                eintragSpanischDonnerstag.Typ = 8;
                eintragSpanischDonnerstag.SprachId = 3;
                eintragSpanischDonnerstag.Ergänzung1 = model.Spanisch_Donnerstag_Ergänzung1;
                eintragSpanischDonnerstag.Ergänzung2 = model.Spanisch_Donnerstag_Ergänzung2;
                eintragSpanischDonnerstag.AllergenId = 1;

                I18n eintragItalienischDonnerstag = new I18n();
                eintragItalienischDonnerstag.Typ = 8;
                eintragItalienischDonnerstag.SprachId = 2;
                eintragItalienischDonnerstag.Ergänzung1 = model.Italienisch_Donnerstag_Ergänzung1;
                eintragItalienischDonnerstag.Ergänzung2 = model.Italienisch_Donnerstag_Ergänzung2;
                eintragItalienischDonnerstag.AllergenId = 1;

                I18n eintragRussischDonnerstag = new I18n();
                eintragRussischDonnerstag.Typ = 8;
                eintragRussischDonnerstag.SprachId = 4;
                eintragRussischDonnerstag.Ergänzung1 = model.Russisch_Donnerstag_Ergänzung1;
                eintragRussischDonnerstag.Ergänzung2 = model.Russisch_Donnerstag_Ergänzung2;
                eintragRussischDonnerstag.AllergenId = 1;

                I18n eintragEnglischFreitag = new I18n();
                eintragEnglischFreitag.Typ = 8;
                eintragEnglischFreitag.SprachId = 5;
                eintragEnglischFreitag.Ergänzung1 = model.Englisch_Freitag_Ergänzung1;
                eintragEnglischFreitag.Ergänzung2 = model.Englisch_Freitag_Ergänzung2;
                eintragEnglischFreitag.AllergenId = 1;

                I18n eintragSpanischFreitag = new I18n();
                eintragSpanischFreitag.Typ = 8;
                eintragSpanischFreitag.SprachId = 3;
                eintragSpanischFreitag.Ergänzung1 = model.Spanisch_Freitag_Ergänzung1;
                eintragSpanischFreitag.Ergänzung2 = model.Spanisch_Freitag_Ergänzung2;
                eintragSpanischFreitag.AllergenId = 1;

                I18n eintragItalienischFreitag = new I18n();
                eintragItalienischFreitag.Typ = 8;
                eintragItalienischFreitag.SprachId = 2;
                eintragItalienischFreitag.Ergänzung1 = model.Italienisch_Freitag_Ergänzung1;
                eintragItalienischFreitag.Ergänzung2 = model.Italienisch_Freitag_Ergänzung2;
                eintragItalienischFreitag.AllergenId = 1;

                I18n eintragRussischFreitag = new I18n();
                eintragRussischFreitag.Typ = 8;
                eintragRussischFreitag.SprachId = 4;
                eintragRussischFreitag.Ergänzung1 = model.Russisch_Freitag_Ergänzung1;
                eintragRussischFreitag.Ergänzung2 = model.Russisch_Freitag_Ergänzung2;
                eintragRussischFreitag.AllergenId = 1;

                I18n eintragEnglischSamstag = new I18n();
                eintragEnglischSamstag.Typ = 8;
                eintragEnglischSamstag.SprachId = 5;
                eintragEnglischSamstag.Ergänzung1 = model.Englisch_Samstag_Ergänzung1;
                eintragEnglischSamstag.Ergänzung2 = model.Englisch_Samstag_Ergänzung2;
                eintragEnglischSamstag.AllergenId = 1;

                I18n eintragSpanischSamstag = new I18n();
                eintragSpanischSamstag.Typ = 8;
                eintragSpanischSamstag.SprachId = 3;
                eintragSpanischSamstag.Ergänzung1 = model.Spanisch_Samstag_Ergänzung1;
                eintragSpanischSamstag.Ergänzung2 = model.Spanisch_Samstag_Ergänzung2;
                eintragSpanischSamstag.AllergenId = 1;

                I18n eintragItalienischSamstag = new I18n();
                eintragItalienischSamstag.Typ = 8;
                eintragItalienischSamstag.SprachId = 2;
                eintragItalienischSamstag.Ergänzung1 = model.Italienisch_Samstag_Ergänzung1;
                eintragItalienischSamstag.Ergänzung2 = model.Italienisch_Samstag_Ergänzung2;
                eintragItalienischSamstag.AllergenId = 1;

                I18n eintragRussischSamstag = new I18n();
                eintragRussischSamstag.Typ = 8;
                eintragRussischSamstag.SprachId = 4;
                eintragRussischSamstag.Ergänzung1 = model.Russisch_Samstag_Ergänzung1;
                eintragRussischSamstag.Ergänzung2 = model.Russisch_Samstag_Ergänzung2;
                eintragRussischSamstag.AllergenId = 1;

                I18n eintragEnglischSonntag = new I18n();
                eintragEnglischSonntag.Typ = 8;
                eintragEnglischSonntag.SprachId = 5;
                eintragEnglischSonntag.Ergänzung1 = model.Englisch_Sonntag_Ergänzung1;
                eintragEnglischSonntag.Ergänzung2 = model.Englisch_Sonntag_Ergänzung2;
                eintragEnglischSonntag.AllergenId = 1;
                eintragEnglischSonntag.Header = model.Englisch_Nachwort;

                I18n eintragSpanischSonntag = new I18n();
                eintragSpanischSonntag.Typ = 8;
                eintragSpanischSonntag.SprachId = 3;
                eintragSpanischSonntag.Ergänzung1 = model.Spanisch_Sonntag_Ergänzung1;
                eintragSpanischSonntag.Ergänzung2 = model.Spanisch_Sonntag_Ergänzung2;
                eintragSpanischSonntag.AllergenId = 1;
                eintragSpanischSonntag.Header = model.Spanisch_Nachwort;

                I18n eintragItalienischSonntag = new I18n();
                eintragItalienischSonntag.Typ = 8;
                eintragItalienischSonntag.SprachId = 2;
                eintragItalienischSonntag.Ergänzung1 = model.Italienisch_Sonntag_Ergänzung1;
                eintragItalienischSonntag.Ergänzung2 = model.Italienisch_Sonntag_Ergänzung2;
                eintragItalienischSonntag.AllergenId = 1;
                eintragItalienischSonntag.Header = model.Italienisch_Nachwort;

                I18n eintragRussischSonntag = new I18n();
                eintragRussischSonntag.Typ = 8;
                eintragRussischSonntag.SprachId = 4;
                eintragRussischSonntag.Ergänzung1 = model.Russisch_Sonntag_Ergänzung1;
                eintragRussischSonntag.Ergänzung2 = model.Russisch_Sonntag_Ergänzung2;
                eintragRussischSonntag.AllergenId = 1;
                eintragRussischSonntag.Header = model.Russisch_Nachwort;


                using (GastroEntities _db = new GastroEntities())
                {
                    //erst löschen wenn vorhanden
                    List<I18n> liste = (from I18n i18n in _db.I18n where i18n.Typ == 8 select i18n).ToList();
                    _db.I18n.RemoveRange(liste);

                    _db.I18n.Add(eintragEnglischMontag);
                    _db.I18n.Add(eintragItalienischMontag);
                    _db.I18n.Add(eintragSpanischMontag);
                    _db.I18n.Add(eintragRussischMontag);

                    _db.I18n.Add(eintragEnglischDienstag);
                    _db.I18n.Add(eintragItalienischDienstag);
                    _db.I18n.Add(eintragSpanischDienstag);
                    _db.I18n.Add(eintragRussischDienstag);

                    _db.I18n.Add(eintragEnglischMittwoch);
                    _db.I18n.Add(eintragItalienischMittwoch);
                    _db.I18n.Add(eintragSpanischMittwoch);
                    _db.I18n.Add(eintragRussischMittwoch);

                    _db.I18n.Add(eintragEnglischDonnerstag);
                    _db.I18n.Add(eintragItalienischDonnerstag);
                    _db.I18n.Add(eintragSpanischDonnerstag);
                    _db.I18n.Add(eintragRussischDonnerstag);

                    _db.I18n.Add(eintragEnglischFreitag);
                    _db.I18n.Add(eintragItalienischFreitag);
                    _db.I18n.Add(eintragSpanischFreitag);
                    _db.I18n.Add(eintragRussischFreitag);

                    _db.I18n.Add(eintragEnglischSamstag);
                    _db.I18n.Add(eintragItalienischSamstag);
                    _db.I18n.Add(eintragSpanischSamstag);
                    _db.I18n.Add(eintragRussischSamstag);

                    _db.I18n.Add(eintragEnglischSonntag);
                    _db.I18n.Add(eintragItalienischSonntag);
                    _db.I18n.Add(eintragSpanischSonntag);
                    _db.I18n.Add(eintragRussischSonntag);

                    _db.SaveChanges();
                }

                return RedirectToAction("Öffnungszeiten", "Admin");
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