using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminVeranstaltungsSpeisenModel
    {
        public List<KategorienFuerModel> AlleKategorien { get; set; }
        public KategorienFuerModel SelectedKategorie { get; set; }
        public List<VeranstaltungsSpeisen> AlleGetränke { get; set; }
        public VeranstaltungsSpeisen GewähltesGetränk { get; set; }

        public Dictionary<int, List<Allergene>> AllergeneSpeise = new Dictionary<int, List<Allergene>>();
        public List<Allergene> AlleAllergene { get; set; }

        public AdminVeranstaltungsSpeisenModel()
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(4);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from VeranstaltungsSpeisen mt in _db.VeranstaltungsSpeisen where mt.KategorieSpeise == kat.id select mt).Any();
                    AlleKategorien.Add(kfm);
                }
            }
        }

        public AdminVeranstaltungsSpeisenModel(int id, int level)
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(4);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from VeranstaltungsSpeisen mt in _db.VeranstaltungsSpeisen where mt.KategorieSpeise == kat.id select mt).Any();
                    if(kfm.Kategorie.id == id){
                        SelectedKategorie = kfm;
                        SelectedKategorie.Level = level;
                    }
                    AlleKategorien.Add(kfm);
                }
                AlleGetränke = (from VeranstaltungsSpeisen mt in _db.VeranstaltungsSpeisen where mt.KategorieSpeise == id select mt).ToList();
                GewähltesGetränk = new VeranstaltungsSpeisen();
                GewähltesGetränk.id = 0;
            }
        }

        public AdminVeranstaltungsSpeisenModel(int id, int level, int speiseId)
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(4);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from VeranstaltungsSpeisen mt in _db.VeranstaltungsSpeisen where mt.KategorieSpeise == kat.id select mt).Any();
                    if (kfm.Kategorie.id == id)
                    {
                        SelectedKategorie = kfm;
                        SelectedKategorie.Level = level;
                    }
                    AlleKategorien.Add(kfm);
                }
                AlleGetränke = (from VeranstaltungsSpeisen mt in _db.VeranstaltungsSpeisen where mt.KategorieSpeise == id select mt).ToList();
                foreach (VeranstaltungsSpeisen mt in AlleGetränke)
                {
                    List<int> list = (from AllergeneVeranstaltungsSpeisenIdSpeiseId amsid in _db.AllergeneVeranstaltungsSpeisenIdSpeiseId where amsid.sid == mt.id select amsid.aid).ToList();

                    var listAl = (from amsid in _db.AllergeneVeranstaltungsSpeisenIdSpeiseId
                                  join al in _db.Allergene on amsid.aid equals al.id
                                  where amsid.sid == mt.id
                                  select al).ToList();
                    AllergeneSpeise.Add(mt.id, listAl);
                }
                GewähltesGetränk = AlleGetränke.Where(x => x.id == speiseId).FirstOrDefault();
            }
        }
    }
}