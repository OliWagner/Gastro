using GastroPages.Helpers;
using GastroPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GastroPages.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin")) {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Benutzer()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminBenutzerModel model = new AdminBenutzerModel();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult BenutzerEintragen(Benutzer benutzer)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                if (benutzer.id == 0 ) {
                    using (GastroEntities _db = new GastroEntities()) {
                        _db.Benutzer.Add(benutzer);
                        _db.SaveChanges();
                    }
                } else {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Benutzer _benutzer = (from Benutzer b in _db.Benutzer where b.id == benutzer.id select b).FirstOrDefault();
                        _benutzer.Nachname = benutzer.Nachname;
                        _benutzer.Vorname = benutzer.Vorname;
                        _benutzer.Email = benutzer.Email;
                        _benutzer.Telefon = benutzer.Telefon;
                        _benutzer.Passwort = benutzer.Passwort;
                        _benutzer.IstAdmin = benutzer.IstAdmin;
                        _benutzer.IstAktiv = benutzer.IstAktiv;
                        _db.SaveChanges();
                    }
                }

                AdminBenutzerModel model = new AdminBenutzerModel();
                return RedirectToAction("Benutzer", "Admin", model);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Reservierung()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminReservierungModel model = new AdminReservierungModel();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ReservierungEintragen(AdminReservierungModel arm)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                //EIntragen
                using (GastroEntities _db = new GastroEntities()) {
                    Öffnungszeiten ö1 = (from Öffnungszeiten oez in _db.Öffnungszeiten where oez.Wochentag == 10 select oez).FirstOrDefault();
                    Öffnungszeiten ö2 = (from Öffnungszeiten oez in _db.Öffnungszeiten where oez.Wochentag == 11 select oez).FirstOrDefault();
                    ö1.Ergänzung1 = arm.Ansprache;
                    ö2.Ergänzung1 = arm.WichtigerHinweis;
                    _db.SaveChanges();
                }
                AdminReservierungModel model = new AdminReservierungModel();
                return RedirectToAction("Reservierung", "Admin", model);
            }
            return RedirectToAction("Index", "Home");
        }



        public ActionResult BenutzerBearbeiten(int id)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminBenutzerModel model = new AdminBenutzerModel(id);
                return View("Benutzer", model);
            }
            return RedirectToAction("Index", "Home");
        }

        

        [HttpPost]
        public ActionResult MittagstischKategorieEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                int SelectedKategoriId = Int32.Parse(Request["SelectedKategoriId"].ToString());
                string KategorieBezeichnung = Request["KategorieBezeichnung"] != null ? Request["KategorieBezeichnung"].ToString() : "";
                string KategorieSortierung = Request["KategorieSortierung"] != null ? Request["KategorieSortierung"].ToString() : "";
                var KategorieHeader = Request["KategorieHeader"];
                var KategorieFooter = Request["KategorieFooter"];

                var NeueUnterKategorie = Request["NeueUnterKategorie"];

                string NeueUnterKategorieSortierung = "1000";
                if (Request["NeueUnterKategorieSortierung"] != null) {
                    NeueUnterKategorieSortierung = Request["NeueUnterKategorieSortierung"].ToString();
                } 

                var NeueUnterKategorieHeader = Request["NeueKategorieHeader"];
                var NeueUnterKategorieFooter = Request["NeueKategorieFooter"];

                var KategorieLoeschen = Request["KategorieLoeschen"];

                var NeueHauptKategorieBezeichnung = Request["NeueHauptKategorieBezeichnung"];

                if (SelectedKategoriId == 0) {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 5;
                    kat.Bezeichnung = NeueHauptKategorieBezeichnung.ToString();
                    kat.Header = "";
                    kat.Footer = "";
                    kat.Sortierung = 1000;
                    kat.Oberkategorie = 0;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                    AdminMittagstischModel model3 = new AdminMittagstischModel();
                    return View("Mittagstisch", model3);
                }

                if (KategorieLoeschen != null && KategorieLoeschen.ToString().Equals("x")) {
                    using (GastroEntities _db = new GastroEntities()) {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        _db.Kategorien.Remove(kat);
                        _db.SaveChanges();

                        List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 3 && i18n.AllergenId == SelectedKategoriId select i18n).ToList();
                        _db.I18n.RemoveRange(list);
                    }
                    AdminMittagstischModel model2 = new AdminMittagstischModel();
                    return View("Mittagstisch", model2);
                }

                if (NeueUnterKategorie != null && !NeueUnterKategorie.ToString().Equals("")) {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 5;
                    kat.Bezeichnung = NeueUnterKategorie.ToString();
                    kat.Header = NeueUnterKategorieHeader.ToString();
                    kat.Footer = NeueUnterKategorieFooter.ToString();
                    kat.Sortierung = Int32.Parse(NeueUnterKategorieSortierung);
                    kat.Oberkategorie = SelectedKategoriId;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                }

                if (SelectedKategoriId != 0)
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        kat.Bezeichnung = KategorieBezeichnung;
                        kat.Sortierung = Int32.Parse(KategorieSortierung);
                        kat.Header = KategorieHeader.ToString();
                        kat.Footer = KategorieFooter.ToString();
                        _db.SaveChanges();
                    }
                }

                AdminMittagstischModel model = new AdminMittagstischModel();
                return View("Mittagstisch", model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult MittagstischSpeisenEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                
                var Todo = Request["Todo"];
                if (Todo != null) {
                    if (Todo.Equals("Delete")) {
                        var katId = int.Parse(Request["KategorieId"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        using (GastroEntities _db = new GastroEntities()) {
                            Mittagstisch mt = (from Mittagstisch m in _db.Mittagstisch where m.id == speiseId select m).FirstOrDefault();
                            _db.Mittagstisch.Remove(mt);
                            List<AllergeneMittagstischIdSpeiseId> li = (from AllergeneMittagstischIdSpeiseId aid in _db.AllergeneMittagstischIdSpeiseId where aid.sid == mt.id select aid).ToList();
                            _db.AllergeneMittagstischIdSpeiseId.RemoveRange(li);
                            _db.SaveChanges();
                            List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 4 && i18n.AllergenId == speiseId select i18n).ToList();
                            _db.I18n.RemoveRange(list);
                        }

                        AdminMittagstischModel model = new AdminMittagstischModel(katId, level);
                        return View("Mittagstisch", model);
                    }

                    if (Todo.Equals("Edit")) {
                        var katId = int.Parse(Request["KategorieId"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        AdminMittagstischModel model = new AdminMittagstischModel(katId, level, speiseId);
                        return View("Mittagstisch", model);
                    }
                }

                var allergenIds = Request["aids[]"];
                int KategorieId = int.Parse(Request["KategorieId"].ToString());
                var Bezeichnung = Request["Bezeichnung"];
                var IstAktiv = Request["IstAktiv"];
                var Beschreibung = Request["Beschreibung"];
                var Preis = Request["Preis"];
                int Level = int.Parse(Request["Level"].ToString());
                int GewählteSpeiseId = int.Parse(Request["GewählteSpeiseId"].ToString());
                if (GewählteSpeiseId == 0)
                {

                    Mittagstisch mt = new Mittagstisch();
                    mt.KategorieId = KategorieId;
                    mt.Bezeichnung = Bezeichnung.ToString();
                    mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                    mt.Beschreibung = Beschreibung.ToString();
                    mt.Preis = decimal.Parse(Preis.ToString());
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Mittagstisch.Add(mt);
                        _db.SaveChanges();
                        int number = (from Mittagstisch mtt in _db.Mittagstisch orderby mtt.id descending select mtt.id).FirstOrDefault();
                        if (allergenIds != null) {
                            foreach (string id in allergenIds.Split(','))
                            {
                                AllergeneMittagstischIdSpeiseId eintrag = new AllergeneMittagstischIdSpeiseId();
                                eintrag.aid = int.Parse(id);
                                eintrag.sid = number;
                                _db.AllergeneMittagstischIdSpeiseId.Add(eintrag);
                            }
                            _db.SaveChanges();
                        }
                    }
                    AdminMittagstischModel model = new AdminMittagstischModel(KategorieId, Level);
                    return View("Mittagstisch", model);
                }
                else {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Mittagstisch mt = (from Mittagstisch m in _db.Mittagstisch where m.id == GewählteSpeiseId select m).FirstOrDefault();
                        mt.KategorieId = KategorieId;
                        mt.Bezeichnung = Bezeichnung.ToString();
                        mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                        mt.Beschreibung = Beschreibung.ToString();
                        mt.Preis = decimal.Parse(Preis.ToString());

                        if (allergenIds != null)
                        {
                            //erst die alten löschen
                            List<AllergeneMittagstischIdSpeiseId> li = (from AllergeneMittagstischIdSpeiseId a in _db.AllergeneMittagstischIdSpeiseId where a.sid == mt.id select a).ToList();
                            _db.AllergeneMittagstischIdSpeiseId.RemoveRange(li);
                            foreach (Allergene alg in _db.Allergene)
                            {
                                if (allergenIds.Split(',').Contains(alg.id.ToString())) {
                                    AllergeneMittagstischIdSpeiseId ss = new AllergeneMittagstischIdSpeiseId();
                                    ss.aid = alg.id;
                                    ss.sid = mt.id;
                                    _db.AllergeneMittagstischIdSpeiseId.Add(ss);
                                } 
                            }
                        }
                        _db.SaveChanges();
                    }
                    AdminMittagstischModel model = new AdminMittagstischModel(KategorieId, Level);
                    return View("Mittagstisch", model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Mittagstisch(int? katid, int? level)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminMittagstischModel model;
                if (katid == null)
                {
                    model = new AdminMittagstischModel();
                }
                else {
                    model = new AdminMittagstischModel((int)katid, (int)level);
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }




        public ActionResult Speisen(int? katid, int? level)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminSpeisenModel model;
                if (katid == null)
                {
                    model = new AdminSpeisenModel();
                }
                else
                {
                    model = new AdminSpeisenModel((int)katid, (int)level);
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SpeisenKategorieEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                int SelectedKategoriId = Int32.Parse(Request["SelectedKategoriId"].ToString());
                string KategorieBezeichnung = Request["KategorieBezeichnung"] != null ? Request["KategorieBezeichnung"].ToString() : "";
                string KategorieSortierung = Request["KategorieSortierung"] != null ? Request["KategorieSortierung"].ToString() : "";
                var KategorieHeader = Request["KategorieHeader"];
                var KategorieFooter = Request["KategorieFooter"];

                var NeueUnterKategorie = Request["NeueUnterKategorie"];

                string NeueUnterKategorieSortierung = "1000";
                if (Request["NeueUnterKategorieSortierung"] != null)
                {
                    NeueUnterKategorieSortierung = Request["NeueUnterKategorieSortierung"].ToString();
                }

                var NeueUnterKategorieHeader = Request["NeueKategorieHeader"];
                var NeueUnterKategorieFooter = Request["NeueKategorieFooter"];

                var KategorieLoeschen = Request["KategorieLoeschen"];

                var NeueHauptKategorieBezeichnung = Request["NeueHauptKategorieBezeichnung"];

                if (SelectedKategoriId == 0)
                {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 1;
                    kat.Bezeichnung = NeueHauptKategorieBezeichnung.ToString();
                    kat.Header = "";
                    kat.Footer = "";
                    kat.Sortierung = 1000;
                    kat.Oberkategorie = 0;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                    AdminSpeisenModel model3 = new AdminSpeisenModel();
                    return View("Speisen", model3);
                }

                if (KategorieLoeschen != null && KategorieLoeschen.ToString().Equals("x"))
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        _db.Kategorien.Remove(kat);
                        _db.SaveChanges();
                        List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 3 && i18n.AllergenId == SelectedKategoriId select i18n).ToList();
                        _db.I18n.RemoveRange(list);
                    }
                    AdminSpeisenModel model2 = new AdminSpeisenModel();
                    return View("Speisen", model2);
                }

                if (NeueUnterKategorie != null && !NeueUnterKategorie.ToString().Equals(""))
                {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 1;
                    kat.Bezeichnung = NeueUnterKategorie.ToString();
                    kat.Header = NeueUnterKategorieHeader.ToString();
                    kat.Footer = NeueUnterKategorieFooter.ToString();
                    kat.Sortierung = Int32.Parse(NeueUnterKategorieSortierung);
                    kat.Oberkategorie = SelectedKategoriId;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                }

                if (SelectedKategoriId != 0)
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        kat.Bezeichnung = KategorieBezeichnung;
                        kat.Sortierung = Int32.Parse(KategorieSortierung);
                        kat.Header = KategorieHeader.ToString();
                        kat.Footer = KategorieFooter.ToString();
                        _db.SaveChanges();
                    }
                }

                AdminSpeisenModel model = new AdminSpeisenModel();
                return View("Speisen", model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SpeisenEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                var Todo = Request["Todo"];
                if (Todo != null)
                {
                    if (Todo.Equals("Delete"))
                    {
                        var katId = int.Parse(Request["KategorieId"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        using (GastroEntities _db = new GastroEntities())
                        {
                            Speisen mt = (from Speisen m in _db.Speisen where m.id == speiseId select m).FirstOrDefault();
                            _db.Speisen.Remove(mt);
                            List<AllergeneSpeiseIdSpeiseId> li = (from AllergeneSpeiseIdSpeiseId aid in _db.AllergeneSpeiseIdSpeiseId where aid.sid == mt.id select aid).ToList();
                            _db.AllergeneSpeiseIdSpeiseId.RemoveRange(li);
                            
                            List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 5 && i18n.AllergenId == speiseId select i18n).ToList();
                            _db.I18n.RemoveRange(list);
                            _db.SaveChanges();
                        }

                        AdminSpeisenModel model = new AdminSpeisenModel(katId, level);
                        return View("Speisen", model);
                    }

                    if (Todo.Equals("Edit"))
                    {
                        var katId = int.Parse(Request["KategorieId"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        AdminSpeisenModel model = new AdminSpeisenModel(katId, level, speiseId);
                        return View("Speisen", model);
                    }
                }

                var allergenIds = Request["aids[]"];
                int KategorieId = int.Parse(Request["KategorieId"].ToString());
                var Bezeichnung = Request["Bezeichnung"];
                var IstAktiv = Request["IstAktiv"];
                var Beschreibung = Request["Beschreibung"];
                var Preis = Request["Preis"];
                int Level = int.Parse(Request["Level"].ToString());
                int GewählteSpeiseId = int.Parse(Request["GewählteSpeiseId"].ToString());
                var Sortierung = Request["Sortierung"];
                var Kartennummer = Request["Kartennummer"];

                if (GewählteSpeiseId == 0)
                {

                    Speisen mt = new Speisen();
                    mt.KategorieId = KategorieId;
                    mt.Bezeichnung = Bezeichnung.ToString();
                    mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                    mt.Beschreibung = Beschreibung.ToString();
                    mt.Preis = decimal.Parse(Preis.ToString());

                    mt.Kartennummer = Kartennummer.ToString();
                    mt.Sortierung = int.Parse(Sortierung.ToString());
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Speisen.Add(mt);
                        _db.SaveChanges();
                        int number = (from Speisen mtt in _db.Speisen orderby mtt.id descending select mtt.id).FirstOrDefault();
                        if (allergenIds != null)
                        {
                            foreach (string id in allergenIds.Split(','))
                            {
                                AllergeneSpeiseIdSpeiseId eintrag = new AllergeneSpeiseIdSpeiseId();
                                eintrag.aid = int.Parse(id);
                                eintrag.sid = number;
                                _db.AllergeneSpeiseIdSpeiseId.Add(eintrag);
                            }
                            _db.SaveChanges();
                        }
                    }
                    AdminSpeisenModel model = new AdminSpeisenModel(KategorieId, Level);
                    return View("Speisen", model);
                }
                else
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Speisen mt = (from Speisen m in _db.Speisen where m.id == GewählteSpeiseId select m).FirstOrDefault();
                        mt.KategorieId = KategorieId;
                        mt.Bezeichnung = Bezeichnung.ToString();
                        mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                        mt.Beschreibung = Beschreibung.ToString();
                        mt.Preis = decimal.Parse(Preis.ToString());
                        mt.Kartennummer = Kartennummer.ToString();
                        mt.Sortierung = int.Parse(Sortierung.ToString());
                        if (allergenIds != null)
                        {
                            //erst die alten löschen
                            List<AllergeneSpeiseIdSpeiseId> li = (from AllergeneSpeiseIdSpeiseId a in _db.AllergeneSpeiseIdSpeiseId where a.sid == mt.id select a).ToList();
                            _db.AllergeneSpeiseIdSpeiseId.RemoveRange(li);
                            foreach (Allergene alg in _db.Allergene)
                            {
                                if (allergenIds.Split(',').Contains(alg.id.ToString()))
                                {
                                    AllergeneSpeiseIdSpeiseId ss = new AllergeneSpeiseIdSpeiseId();
                                    ss.aid = alg.id;
                                    ss.sid = mt.id;
                                    _db.AllergeneSpeiseIdSpeiseId.Add(ss);
                                }
                            }
                        }
                        _db.SaveChanges();
                    }
                    AdminSpeisenModel model = new AdminSpeisenModel(KategorieId, Level);
                    return View("Speisen", model);
                }
            }
            return RedirectToAction("Index", "Home");
        }





        public ActionResult Getränke(int? katid, int? level)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminGetränkeModel model;
                if (katid == null)
                {
                    model = new AdminGetränkeModel();
                }
                else
                {
                    model = new AdminGetränkeModel((int)katid, (int)level);
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult GetränkeKategorieEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                int SelectedKategoriId = Int32.Parse(Request["SelectedKategoriId"].ToString());
                string KategorieBezeichnung = Request["KategorieBezeichnung"] != null ? Request["KategorieBezeichnung"].ToString() : "";
                string KategorieSortierung = Request["KategorieSortierung"] != null ? Request["KategorieSortierung"].ToString() : "";
                var KategorieHeader = Request["KategorieHeader"];
                var KategorieFooter = Request["KategorieFooter"];

                var NeueUnterKategorie = Request["NeueUnterKategorie"];

                string NeueUnterKategorieSortierung = "1000";
                if (Request["NeueUnterKategorieSortierung"] != null)
                {
                    NeueUnterKategorieSortierung = Request["NeueUnterKategorieSortierung"].ToString();
                }

                var NeueUnterKategorieHeader = Request["NeueKategorieHeader"];
                var NeueUnterKategorieFooter = Request["NeueKategorieFooter"];

                var KategorieLoeschen = Request["KategorieLoeschen"];

                var NeueHauptKategorieBezeichnung = Request["NeueHauptKategorieBezeichnung"];

                if (SelectedKategoriId == 0)
                {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 2;
                    kat.Bezeichnung = NeueHauptKategorieBezeichnung.ToString();
                    kat.Header = "";
                    kat.Footer = "";
                    kat.Sortierung = 1000;
                    kat.Oberkategorie = 0;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                    AdminGetränkeModel model3 = new AdminGetränkeModel();
                    return View("Getränke", model3);
                }

                if (KategorieLoeschen != null && KategorieLoeschen.ToString().Equals("x"))
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        _db.Kategorien.Remove(kat);
                        _db.SaveChanges();
                        List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 3 && i18n.AllergenId == SelectedKategoriId select i18n).ToList();
                        _db.I18n.RemoveRange(list);
                    }
                    AdminGetränkeModel model2 = new AdminGetränkeModel();
                    return View("Getränke", model2);
                }

                if (NeueUnterKategorie != null && !NeueUnterKategorie.ToString().Equals(""))
                {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 2;
                    kat.Bezeichnung = NeueUnterKategorie.ToString();
                    kat.Header = NeueUnterKategorieHeader.ToString();
                    kat.Footer = NeueUnterKategorieFooter.ToString();
                    kat.Sortierung = Int32.Parse(NeueUnterKategorieSortierung);
                    kat.Oberkategorie = SelectedKategoriId;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                }

                if (SelectedKategoriId != 0)
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        kat.Bezeichnung = KategorieBezeichnung;
                        kat.Sortierung = Int32.Parse(KategorieSortierung);
                        kat.Header = KategorieHeader.ToString();
                        kat.Footer = KategorieFooter.ToString();
                        _db.SaveChanges();
                    }
                }

                AdminGetränkeModel model = new AdminGetränkeModel();
                return View("Getränke", model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult GetränkeEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                var Todo = Request["Todo"];
                if (Todo != null)
                {
                    if (Todo.Equals("Delete"))
                    {
                        var katId = int.Parse(Request["KategorieId"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        using (GastroEntities _db = new GastroEntities())
                        {
                            Getränke mt = (from Getränke m in _db.Getränke where m.id == speiseId select m).FirstOrDefault();
                            _db.Getränke.Remove(mt);
                            _db.SaveChanges();

                            List<AllergeneGetränkeIdSpeiseId> li = (from AllergeneGetränkeIdSpeiseId aid in _db.AllergeneGetränkeIdSpeiseId where aid.sid == mt.id select aid).ToList();
                            _db.AllergeneGetränkeIdSpeiseId.RemoveRange(li);

                            List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 2 && i18n.AllergenId == speiseId select i18n).ToList();
                            _db.I18n.RemoveRange(list);
                        }

                        AdminGetränkeModel model = new AdminGetränkeModel(katId, level);
                        return View("Getränke", model);
                    }

                    if (Todo.Equals("Edit"))
                    {
                        var katId = int.Parse(Request["KategorieId"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        AdminGetränkeModel model = new AdminGetränkeModel(katId, level, speiseId);
                        return View("Getränke", model);
                    }
                }

                var allergenIds = Request["aids[]"];
                int KategorieId = int.Parse(Request["KategorieId"].ToString());
                var Bezeichnung = Request["Bezeichnung"];
                var IstAktiv = Request["IstAktiv"];
                var Preis = Request["Preis"];
                var Ergänzung1 = Request["Ergänzung1"];
                var Ergänzung2 = Request["Ergänzung2"];
                int Level = int.Parse(Request["Level"].ToString());
                int GewählteSpeiseId = int.Parse(Request["SpeiseId"].ToString());
                var Sortierung = Request["Sortierung"];

                if (GewählteSpeiseId == 0)
                {

                    Getränke mt = new Getränke();
                    mt.KategorieId = KategorieId;
                    mt.Bezeichnung = Bezeichnung.ToString();
                    mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                    mt.Preis = decimal.Parse(Preis.ToString());
                    mt.Ergänzung1 = Ergänzung1.ToString();
                    mt.Ergänzung2 = Ergänzung2.ToString();
                    mt.Kartennummer = "";
                    mt.Sortierung = int.Parse(Sortierung.ToString());
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Getränke.Add(mt);
                        _db.SaveChanges();
                        int number = (from Getränke mtt in _db.Getränke orderby mtt.id descending select mtt.id).FirstOrDefault();
                        if (allergenIds != null)
                        {
                            foreach (string id in allergenIds.Split(','))
                            {
                                AllergeneVeranstaltungsGetränkeIdSpeiseId eintrag = new AllergeneVeranstaltungsGetränkeIdSpeiseId();
                                eintrag.aid = int.Parse(id);
                                eintrag.sid = number;
                                _db.AllergeneVeranstaltungsGetränkeIdSpeiseId.Add(eintrag);
                            }
                            _db.SaveChanges();
                        }
                    }
                    AdminGetränkeModel model = new AdminGetränkeModel(KategorieId, Level);
                    return View("Getränke", model);
                }
                else
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Getränke mt = (from Getränke m in _db.Getränke where m.id == GewählteSpeiseId select m).FirstOrDefault();
                        mt.KategorieId = KategorieId;
                        mt.Bezeichnung = Bezeichnung.ToString();
                        mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                        mt.Preis = decimal.Parse(Preis.ToString());
                        mt.Ergänzung1 = Ergänzung1.ToString();
                        mt.Ergänzung2 = Ergänzung2.ToString();
                        mt.Sortierung = int.Parse(Sortierung.ToString());
                        if (allergenIds != null)
                        {
                            //erst die alten löschen
                            List<AllergeneGetränkeIdSpeiseId> li = (from AllergeneGetränkeIdSpeiseId a in _db.AllergeneGetränkeIdSpeiseId where a.sid == mt.id select a).ToList();
                            _db.AllergeneGetränkeIdSpeiseId.RemoveRange(li);
                            foreach (Allergene alg in _db.Allergene)
                            {
                                if (allergenIds.Split(',').Contains(alg.id.ToString()))
                                {
                                    AllergeneVeranstaltungsGetränkeIdSpeiseId ss = new AllergeneVeranstaltungsGetränkeIdSpeiseId();
                                    ss.aid = alg.id;
                                    ss.sid = mt.id;
                                    _db.AllergeneVeranstaltungsGetränkeIdSpeiseId.Add(ss);
                                }
                            }
                        }
                        _db.SaveChanges();
                    }
                    AdminGetränkeModel model = new AdminGetränkeModel(KategorieId, Level);
                    return View("Getränke", model);
                }
            }
            return RedirectToAction("Index", "Home");
        }



        public ActionResult Räume()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }




        public ActionResult Öffnungszeiten()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminÖffnungszeitenModel model = new AdminÖffnungszeitenModel();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ÖffnungszeitenEintragen(AdminÖffnungszeitenModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                TrageVorwortNachwortEin(model.Vorwort, model.Nachwort);
                    for (int i = 1; i < 8; i++) {
                        switch (i) {
                            case 1: TrageÖzEin(i, model.Montag); break;
                            case 2: TrageÖzEin(i, model.Dienstag); break;
                            case 3: TrageÖzEin(i, model.Mittwoch); break;
                            case 4: TrageÖzEin(i, model.Donnerstag); break;
                            case 5: TrageÖzEin(i, model.Freitag); break;
                            case 6: TrageÖzEin(i, model.Samstag); break;
                            case 7: TrageÖzEin(i, model.Sonntag); break;
                        }
                    }
                return RedirectToAction("Öffnungszeiten", "Admin");
            }
            return RedirectToAction("Index", "Home");
        }

        private void TrageÖzEin(int id, Öffnungszeiten öz) {
            using (GastroEntities _db = new GastroEntities())
            {
                Öffnungszeiten ö = _db.Öffnungszeiten.Where(x => x.id == id).FirstOrDefault();
                ö.Bis1 = öz.Bis1;
                ö.Bis2 = öz.Bis2;
                ö.Ergänzung1 = öz.Ergänzung1;
                ö.Ergänzung2 = öz.Ergänzung2;
                ö.Ergänzung3 = öz.Ergänzung3;
                ö.IstRuhetag = öz.IstRuhetag;
                ö.Von1 = öz.Von1;
                ö.Von2 = öz.Von2;

                _db.SaveChanges();
            }
        }

        private void TrageVorwortNachwortEin(string vorwort, string nachwort)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                Öffnungszeiten özVw = _db.Öffnungszeiten.Where(x => x.id == 8).FirstOrDefault();
                Öffnungszeiten özNw = _db.Öffnungszeiten.Where(x => x.id == 9).FirstOrDefault();
                özVw.Ergänzung1 = vorwort;
                özNw.Ergänzung1 = nachwort;
                _db.SaveChanges();
            }
        }


        public ActionResult Allergene(int? id, int? todo)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                if (id != null && todo != null) {
                    if ((int)todo == 0)
                    {
                        using (GastroEntities _db = new GastroEntities()) {
                            Allergene a = (from Allergene al in _db.Allergene where al.id == id select al).FirstOrDefault();
                            _db.Allergene.Remove(a);
                            _db.SaveChanges();
                            AdminAllergeneModel model = new AdminAllergeneModel();
                            return View(model);
                        }
                    }
                    else {
                        AdminAllergeneModel model = new AdminAllergeneModel((int)id);
                        return View(model);
                    }
                    
                } else {
                    AdminAllergeneModel model = new AdminAllergeneModel();
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AllergeneEintragen(AdminAllergeneModel model)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                if (ModelState.IsValid)
                {
                    using (GastroEntities _db = new GastroEntities()) {
                        if (model.AllergenItem.id == 0)
                        {
                            //Neu
                            _db.Allergene.Add(model.AllergenItem);
                            _db.SaveChanges();
                        }
                        else
                        {
                            //Bearbeiten
                            Allergene al = (from Allergene a in _db.Allergene where a.id == model.AllergenItem.id select a).FirstOrDefault();
                            al.Bezeichnung = model.AllergenItem.Bezeichnung;
                            al.Beschreibung = model.AllergenItem.Beschreibung;
                            al.Nummer = model.AllergenItem.Nummer;
                            _db.SaveChanges();
                        }
                    }
                    AdminAllergeneModel m = new AdminAllergeneModel();
                    return View("Allergene", m);
                }
            }
            return RedirectToAction("Index", "Home");
        }




        public ActionResult VeranstaltungsGetränke(int? katid, int? level, int? SpeiseId)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminVeranstaltungsGetränkeModel model;
                if (katid == null)
                {
                    model = new AdminVeranstaltungsGetränkeModel();
                }
                else
                {if (SpeiseId == null) {
                        model = new AdminVeranstaltungsGetränkeModel((int)katid, (int)level);
                    } else {
                        model = new AdminVeranstaltungsGetränkeModel((int)katid, (int)level, (int)SpeiseId);
                    }
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult VeranstaltungsGetränkeKategorieEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                int SelectedKategoriId = Int32.Parse(Request["SelectedKategoriId"].ToString());
                string KategorieBezeichnung = Request["KategorieBezeichnung"] != null ? Request["KategorieBezeichnung"].ToString() : "";
                string KategorieSortierung = Request["KategorieSortierung"] != null ? Request["KategorieSortierung"].ToString() : "";
                var KategorieHeader = Request["KategorieHeader"];
                var KategorieFooter = Request["KategorieFooter"];

                var NeueUnterKategorie = Request["NeueUnterKategorie"];

                string NeueUnterKategorieSortierung = "1000";
                if (Request["NeueUnterKategorieSortierung"] != null)
                {
                    NeueUnterKategorieSortierung = Request["NeueUnterKategorieSortierung"].ToString();
                }

                var NeueUnterKategorieHeader = Request["NeueKategorieHeader"];
                var NeueUnterKategorieFooter = Request["NeueKategorieFooter"];

                var KategorieLoeschen = Request["KategorieLoeschen"];

                var NeueHauptKategorieBezeichnung = Request["NeueHauptKategorieBezeichnung"];

                if (SelectedKategoriId == 0)
                {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 3;
                    kat.Bezeichnung = NeueHauptKategorieBezeichnung.ToString();
                    kat.Header = "";
                    kat.Footer = "";
                    kat.Sortierung = 1000;
                    kat.Oberkategorie = 0;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                    AdminVeranstaltungsGetränkeModel model3 = new AdminVeranstaltungsGetränkeModel();
                    return View("VeranstaltungsGetränke", model3);
                }

                if (KategorieLoeschen != null && KategorieLoeschen.ToString().Equals("x"))
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        _db.Kategorien.Remove(kat);
                        _db.SaveChanges();
                        List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 3 && i18n.AllergenId == SelectedKategoriId select i18n).ToList();
                        _db.I18n.RemoveRange(list);
                    }
                    AdminVeranstaltungsGetränkeModel model2 = new AdminVeranstaltungsGetränkeModel();
                    return View("VeranstaltungsGetränke", model2);
                }

                if (NeueUnterKategorie != null && !NeueUnterKategorie.ToString().Equals(""))
                {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 3;
                    kat.Bezeichnung = NeueUnterKategorie.ToString();
                    kat.Header = NeueUnterKategorieHeader.ToString();
                    kat.Footer = NeueUnterKategorieFooter.ToString();
                    kat.Sortierung = Int32.Parse(NeueUnterKategorieSortierung);
                    kat.Oberkategorie = SelectedKategoriId;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                }

                if (SelectedKategoriId != 0)
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        kat.Bezeichnung = KategorieBezeichnung;
                        kat.Sortierung = Int32.Parse(KategorieSortierung);
                        kat.Header = KategorieHeader.ToString();
                        kat.Footer = KategorieFooter.ToString();
                        _db.SaveChanges();
                    }
                }

                AdminVeranstaltungsGetränkeModel model = new AdminVeranstaltungsGetränkeModel();
                return View("VeranstaltungsGetränke", model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult VeranstaltungsGetränkeEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                var Todo = Request["Todo"];
                if (Todo != null)
                {
                    if (Todo.Equals("Delete"))
                    {
                        var katId = int.Parse(Request["GetränkeKategorie"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        using (GastroEntities _db = new GastroEntities())
                        {
                            VeranstaltungsGetränke mt = (from VeranstaltungsGetränke m in _db.VeranstaltungsGetränke where m.id == speiseId select m).FirstOrDefault();
                            _db.VeranstaltungsGetränke.Remove(mt);

                            List<AllergeneVeranstaltungsGetränkeIdSpeiseId> li = (from AllergeneVeranstaltungsGetränkeIdSpeiseId aid in _db.AllergeneVeranstaltungsGetränkeIdSpeiseId where aid.sid == mt.id select aid).ToList();
                            _db.AllergeneVeranstaltungsGetränkeIdSpeiseId.RemoveRange(li);

                            List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 6 && i18n.AllergenId == speiseId select i18n).ToList();
                            _db.I18n.RemoveRange(list);

                            _db.SaveChanges();
                        }

                        AdminVeranstaltungsGetränkeModel model = new AdminVeranstaltungsGetränkeModel(katId, level);
                        return View("VeranstaltungsGetränke", model);
                    }

                    if (Todo.Equals("Edit"))
                    {
                        var katId = int.Parse(Request["GetränkeKategorie"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        AdminVeranstaltungsGetränkeModel model = new AdminVeranstaltungsGetränkeModel(katId, level, speiseId);
                        return View("VeranstaltungsGetränke", model);
                    }
                }

                var allergenIds = Request["aids[]"];
                int KategorieId = int.Parse(Request["GetränkeKategorie"].ToString());
                var Bezeichnung = Request["Bezeichnung"];
                var IstAktiv = Request["IstAktiv"];
                var Preis = Request["PreisProEinheit"];
                var Beschreibung = Request["Beschreibung"];
                var Einheit = Request["Einheit"];
                int Level = int.Parse(Request["Level"].ToString());
                int GewählteSpeiseId = int.Parse(Request["SpeiseId"].ToString());
                var Sortierung = Request["Sortierung"];

                if (GewählteSpeiseId == 0)
                {

                    VeranstaltungsGetränke mt = new VeranstaltungsGetränke();
                    mt.KategorieGetränke = KategorieId;
                    mt.Bezeichnung = Bezeichnung.ToString();
                    mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                    mt.PreisProEinheit = decimal.Parse(Preis.ToString());
                    mt.Beschreibung = Beschreibung.ToString();
                    mt.Einheit = Einheit.ToString();
                    mt.Sortierung = int.Parse(Sortierung.ToString());
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.VeranstaltungsGetränke.Add(mt);
                        _db.SaveChanges();
                        int number = (from VeranstaltungsGetränke mtt in _db.VeranstaltungsGetränke orderby mtt.id descending select mtt.id).FirstOrDefault();
                        if (allergenIds != null)
                        {
                            foreach (string id in allergenIds.Split(','))
                            {
                                AllergeneVeranstaltungsGetränkeIdSpeiseId eintrag = new AllergeneVeranstaltungsGetränkeIdSpeiseId();
                                eintrag.aid = int.Parse(id);
                                eintrag.sid = number;
                                _db.AllergeneVeranstaltungsGetränkeIdSpeiseId.Add(eintrag);
                            }
                            _db.SaveChanges();
                        }
                    }
                    AdminVeranstaltungsGetränkeModel model = new AdminVeranstaltungsGetränkeModel(KategorieId, Level);
                    return View("VeranstaltungsGetränke", model);
                }
                else
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        VeranstaltungsGetränke mt = (from VeranstaltungsGetränke m in _db.VeranstaltungsGetränke where m.id == GewählteSpeiseId select m).FirstOrDefault();
                        mt.KategorieGetränke = KategorieId;
                        mt.Bezeichnung = Bezeichnung.ToString();
                        mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                        mt.PreisProEinheit = decimal.Parse(Preis.ToString());
                        mt.Einheit = Einheit.ToString();
                        mt.Beschreibung = Beschreibung.ToString();
                        mt.Sortierung = int.Parse(Sortierung.ToString());
                        if (allergenIds != null)
                        {
                            //erst die alten löschen
                            List<AllergeneVeranstaltungsGetränkeIdSpeiseId> li = (from AllergeneVeranstaltungsGetränkeIdSpeiseId a in _db.AllergeneVeranstaltungsGetränkeIdSpeiseId where a.sid == mt.id select a).ToList();
                            _db.AllergeneVeranstaltungsGetränkeIdSpeiseId.RemoveRange(li);
                            foreach (Allergene alg in _db.Allergene)
                            {
                                if (allergenIds.Split(',').Contains(alg.id.ToString()))
                                {
                                    AllergeneVeranstaltungsGetränkeIdSpeiseId ss = new AllergeneVeranstaltungsGetränkeIdSpeiseId();
                                    ss.aid = alg.id;
                                    ss.sid = mt.id;
                                    _db.AllergeneVeranstaltungsGetränkeIdSpeiseId.Add(ss);
                                }
                            }
                        }
                        _db.SaveChanges();
                    }
                    AdminVeranstaltungsGetränkeModel model = new AdminVeranstaltungsGetränkeModel(KategorieId, Level);
                    return View("VeranstaltungsGetränke", model);
                }
            }
            return RedirectToAction("Index", "Home");
        }






        public ActionResult VeranstaltungsSpeisen(int? katid, int? level, int? SpeiseId)
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                AdminVeranstaltungsSpeisenModel model;
                if (katid == null)
                {
                    model = new AdminVeranstaltungsSpeisenModel();
                }
                else
                {
                    if (SpeiseId == null) {
                        model = new AdminVeranstaltungsSpeisenModel((int)katid, (int)level);
                    } else {
                        model = new AdminVeranstaltungsSpeisenModel((int)katid, (int)level, (int)SpeiseId);
                    }
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult VeranstaltungsSpeisenKategorieEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                int SelectedKategoriId = Int32.Parse(Request["SelectedKategoriId"].ToString());
                string KategorieBezeichnung = Request["KategorieBezeichnung"] != null ? Request["KategorieBezeichnung"].ToString() : "";
                string KategorieSortierung = Request["KategorieSortierung"] != null ? Request["KategorieSortierung"].ToString() : "";
                var KategorieHeader = Request["KategorieHeader"];
                var KategorieFooter = Request["KategorieFooter"];

                var NeueUnterKategorie = Request["NeueUnterKategorie"];

                string NeueUnterKategorieSortierung = "1000";
                if (Request["NeueUnterKategorieSortierung"] != null)
                {
                    NeueUnterKategorieSortierung = Request["NeueUnterKategorieSortierung"].ToString();
                }

                var NeueUnterKategorieHeader = Request["NeueKategorieHeader"];
                var NeueUnterKategorieFooter = Request["NeueKategorieFooter"];

                var KategorieLoeschen = Request["KategorieLoeschen"];

                var NeueHauptKategorieBezeichnung = Request["NeueHauptKategorieBezeichnung"];

                if (SelectedKategoriId == 0)
                {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 4;
                    kat.Bezeichnung = NeueHauptKategorieBezeichnung.ToString();
                    kat.Header = "";
                    kat.Footer = "";
                    kat.Sortierung = 1000;
                    kat.Oberkategorie = 0;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                    AdminVeranstaltungsSpeisenModel model3 = new AdminVeranstaltungsSpeisenModel();
                    return View("VeranstaltungsSpeisen", model3);
                }

                if (KategorieLoeschen != null && KategorieLoeschen.ToString().Equals("x"))
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        _db.Kategorien.Remove(kat);
                        _db.SaveChanges();

                        List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 3 && i18n.AllergenId == SelectedKategoriId select i18n).ToList();
                        _db.I18n.RemoveRange(list);
                    }
                    AdminVeranstaltungsSpeisenModel model2 = new AdminVeranstaltungsSpeisenModel();
                    return View("VeranstaltungsSpeisen", model2);
                }

                if (NeueUnterKategorie != null && !NeueUnterKategorie.ToString().Equals(""))
                {
                    Kategorien kat = new Kategorien();
                    kat.Kategorieart = 4;
                    kat.Bezeichnung = NeueUnterKategorie.ToString();
                    kat.Header = NeueUnterKategorieHeader.ToString();
                    kat.Footer = NeueUnterKategorieFooter.ToString();
                    kat.Sortierung = Int32.Parse(NeueUnterKategorieSortierung);
                    kat.Oberkategorie = SelectedKategoriId;
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.Kategorien.Add(kat);
                        _db.SaveChanges();
                    }
                }

                if (SelectedKategoriId != 0)
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        Kategorien kat = (from Kategorien k in _db.Kategorien where k.id == SelectedKategoriId select k).FirstOrDefault();
                        kat.Bezeichnung = KategorieBezeichnung;
                        kat.Sortierung = Int32.Parse(KategorieSortierung);
                        kat.Header = KategorieHeader.ToString();
                        kat.Footer = KategorieFooter.ToString();
                        _db.SaveChanges();
                    }
                }

                AdminVeranstaltungsSpeisenModel model = new AdminVeranstaltungsSpeisenModel();
                return View("VeranstaltungsSpeisen", model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult VeranstaltungsSpeisenEintragen()
        {
            if (Session["Rolle"] != null && Session["Rolle"].Equals("Admin"))
            {
                var Todo = Request["Todo"];
                if (Todo != null)
                {
                    if (Todo.Equals("Delete"))
                    {
                        var katId = int.Parse(Request["GetränkeKategorie"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        using (GastroEntities _db = new GastroEntities())
                        {
                            VeranstaltungsSpeisen mt = (from VeranstaltungsSpeisen m in _db.VeranstaltungsSpeisen where m.id == speiseId select m).FirstOrDefault();
                            _db.VeranstaltungsSpeisen.Remove(mt);

                            List<AllergeneVeranstaltungsSpeisenIdSpeiseId> li = (from AllergeneVeranstaltungsSpeisenIdSpeiseId aid in _db.AllergeneVeranstaltungsSpeisenIdSpeiseId where aid.sid == mt.id select aid).ToList();
                            _db.AllergeneVeranstaltungsSpeisenIdSpeiseId.RemoveRange(li);

                            List<I18n> list = (from I18n i18n in _db.I18n where i18n.Typ == 7 && i18n.AllergenId == speiseId select i18n).ToList();
                            _db.I18n.RemoveRange(list);

                            _db.SaveChanges();
                        }

                        AdminVeranstaltungsSpeisenModel model = new AdminVeranstaltungsSpeisenModel(katId, level);
                        return View("VeranstaltungsSpeisen", model);
                    }

                    if (Todo.Equals("Edit"))
                    {
                        var katId = int.Parse(Request["GetränkeKategorie"].ToString());
                        var level = int.Parse(Request["Level"].ToString());
                        int speiseId = int.Parse(Request["SpeiseId"].ToString());
                        AdminVeranstaltungsSpeisenModel model = new AdminVeranstaltungsSpeisenModel(katId, level, speiseId);
                        return View("VeranstaltungsSpeisen", model);
                    }
                }

                var allergenIds = Request["aids[]"];
                int KategorieId = int.Parse(Request["GetränkeKategorie"].ToString());
                var Bezeichnung = Request["Bezeichnung"];
                var IstAktiv = Request["IstAktiv"];
                var Preis = Request["PreisProEinheit"];
                var Beschreibung = Request["Beschreibung"];
                var Einheit = Request["Einheit"];
                int Level = int.Parse(Request["Level"].ToString());
                int GewählteSpeiseId = int.Parse(Request["SpeiseId"].ToString());
                var Sortierung = Request["Sortierung"];

                if (GewählteSpeiseId == 0)
                {

                    VeranstaltungsSpeisen mt = new VeranstaltungsSpeisen();
                    mt.KategorieSpeise = KategorieId;
                    mt.Bezeichnung = Bezeichnung.ToString();
                    mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                    mt.PreisProEinheit = decimal.Parse(Preis.ToString());
                    mt.Beschreibung = Beschreibung.ToString();
                    mt.Einheit = Einheit.ToString();
                    mt.Sortierung = int.Parse(Sortierung.ToString());
                    using (GastroEntities _db = new GastroEntities())
                    {
                        _db.VeranstaltungsSpeisen.Add(mt);
                        _db.SaveChanges();
                        int number = (from VeranstaltungsSpeisen mtt in _db.VeranstaltungsSpeisen orderby mtt.id descending select mtt.id).FirstOrDefault();
                        if (allergenIds != null)
                        {
                            foreach (string id in allergenIds.Split(','))
                            {
                                AllergeneVeranstaltungsSpeisenIdSpeiseId eintrag = new AllergeneVeranstaltungsSpeisenIdSpeiseId();
                                eintrag.aid = int.Parse(id);
                                eintrag.sid = number;
                                _db.AllergeneVeranstaltungsSpeisenIdSpeiseId.Add(eintrag);
                            }
                            _db.SaveChanges();
                        }
                    }
                    AdminVeranstaltungsSpeisenModel model = new AdminVeranstaltungsSpeisenModel(KategorieId, Level);
                    return View("VeranstaltungsSpeisen", model);
                }
                else
                {
                    using (GastroEntities _db = new GastroEntities())
                    {
                        VeranstaltungsSpeisen mt = (from VeranstaltungsSpeisen m in _db.VeranstaltungsSpeisen where m.id == GewählteSpeiseId select m).FirstOrDefault();
                        mt.KategorieSpeise = KategorieId;
                        mt.Bezeichnung = Bezeichnung.ToString();
                        mt.IstAktiv = IstAktiv != null ? (IstAktiv.ToString().Equals("x") ? true : false) : false;
                        mt.PreisProEinheit = decimal.Parse(Preis.ToString());
                        mt.Einheit = Einheit.ToString();
                        mt.Beschreibung = Beschreibung.ToString();
                        mt.Sortierung = int.Parse(Sortierung.ToString());
                        if (allergenIds != null)
                        {
                            //erst die alten löschen
                            List<AllergeneVeranstaltungsSpeisenIdSpeiseId> li = (from AllergeneVeranstaltungsSpeisenIdSpeiseId a in _db.AllergeneVeranstaltungsSpeisenIdSpeiseId where a.sid == mt.id select a).ToList();
                            _db.AllergeneVeranstaltungsSpeisenIdSpeiseId.RemoveRange(li);
                            foreach (Allergene alg in _db.Allergene)
                            {
                                if (allergenIds.Split(',').Contains(alg.id.ToString()))
                                {
                                    AllergeneVeranstaltungsSpeisenIdSpeiseId ss = new AllergeneVeranstaltungsSpeisenIdSpeiseId();
                                    ss.aid = alg.id;
                                    ss.sid = mt.id;
                                    _db.AllergeneVeranstaltungsSpeisenIdSpeiseId.Add(ss);
                                }
                            }
                        }
                        _db.SaveChanges();
                    }
                    AdminVeranstaltungsSpeisenModel model = new AdminVeranstaltungsSpeisenModel(KategorieId, Level);
                    return View("VeranstaltungsSpeisen", model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            LoginHelper.KillSessions();
            return RedirectToAction("Index", "Home");
        }
    }
}