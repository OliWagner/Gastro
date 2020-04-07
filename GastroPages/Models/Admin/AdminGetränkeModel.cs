using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminGetränkeModel
    {
        public List<KategorienFuerModel> AlleKategorien { get; set; }
        public KategorienFuerModel SelectedKategorie { get; set; }
        public List<Getränke> AlleGetränke { get; set; }
        public Getränke GewähltesGetränk { get; set; }

        public Dictionary<int, List<Allergene>> AllergeneSpeise = new Dictionary<int, List<Allergene>>();
        public List<Allergene> AlleAllergene { get; set; }

        public AdminGetränkeModel()
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(2);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Getränke mt in _db.Getränke where mt.KategorieId == kat.id select mt).Any();
                    AlleKategorien.Add(kfm);
                }
            }
        }

        public AdminGetränkeModel(int id, int level)
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(2);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Getränke mt in _db.Getränke where mt.KategorieId == kat.id select mt).Any();
                    if(kfm.Kategorie.id == id){
                        SelectedKategorie = kfm;
                        SelectedKategorie.Level = level;
                    }
                    AlleKategorien.Add(kfm);
                }
                AlleGetränke = (from Getränke mt in _db.Getränke where mt.KategorieId == id select mt).ToList();
                GewähltesGetränk = new Getränke();
                GewähltesGetränk.id = 0;
            }
        }

        public AdminGetränkeModel(int id, int level, int speiseId)
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(2);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Getränke mt in _db.Getränke where mt.KategorieId == kat.id select mt).Any();
                    if (kfm.Kategorie.id == id)
                    {
                        SelectedKategorie = kfm;
                        SelectedKategorie.Level = level;
                    }
                    AlleKategorien.Add(kfm);
                }
                AlleGetränke = (from Getränke mt in _db.Getränke where mt.KategorieId == id select mt).ToList();
                foreach (Getränke mt in AlleGetränke)
                {
                    List<int> list = (from AllergeneGetränkeIdSpeiseId amsid in _db.AllergeneGetränkeIdSpeiseId where amsid.sid == mt.id select amsid.aid).ToList();

                    var listAl = (from amsid in _db.AllergeneGetränkeIdSpeiseId
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