using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminSpeisenModel
    {
        public List<KategorienFuerModel> AlleKategorien { get; set; }
        public KategorienFuerModel SelectedKategorie { get; set; }
        public List<Speisen> AlleSpeisen { get; set; }
        public Speisen GewählteSpeise { get; set; }

        public Dictionary<int, List<Allergene>> AllergeneSpeise = new Dictionary<int, List<Allergene>>();
        public List<Allergene> AlleAllergene { get; set; }

        public AdminSpeisenModel()
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(1);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Speisen mt in _db.Speisen where mt.KategorieId == kat.id select mt).Any();
                    AlleKategorien.Add(kfm);
                }
            }
        }

        public AdminSpeisenModel(int id, int level)
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(1);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Speisen mt in _db.Speisen where mt.KategorieId == kat.id select mt).Any();
                    if(kfm.Kategorie.id == id){
                        SelectedKategorie = kfm;
                        SelectedKategorie.Level = level;
                    }
                    AlleKategorien.Add(kfm);
                }
                AlleSpeisen = (from Speisen mt in _db.Speisen where mt.KategorieId == id select mt).ToList();

                foreach (Speisen mt in AlleSpeisen)
                {
                    List<int> list = (from AllergeneSpeiseIdSpeiseId amsid in _db.AllergeneSpeiseIdSpeiseId where amsid.sid == mt.id select amsid.aid).ToList();

                    var listAl = (from amsid in _db.AllergeneSpeiseIdSpeiseId
                                  join al in _db.Allergene on amsid.aid equals al.id
                                  where amsid.sid == mt.id
                                  select al).ToList();
                    AllergeneSpeise.Add(mt.id, listAl);
                }

                GewählteSpeise = new Speisen();
                GewählteSpeise.id = 0;
            }
        }

        public AdminSpeisenModel(int id, int level, int speiseId)
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(1);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Speisen mt in _db.Speisen where mt.KategorieId == kat.id select mt).Any();
                    if (kfm.Kategorie.id == id)
                    {
                        SelectedKategorie = kfm;
                        SelectedKategorie.Level = level;
                    }
                    AlleKategorien.Add(kfm);
                }
                AlleSpeisen = (from Speisen mt in _db.Speisen where mt.KategorieId == id select mt).ToList();
                foreach (Speisen mt in AlleSpeisen)
                {
                    List<int> list = (from AllergeneSpeiseIdSpeiseId amsid in _db.AllergeneSpeiseIdSpeiseId where amsid.sid == mt.id select amsid.aid).ToList();

                    var listAl = (from amsid in _db.AllergeneSpeiseIdSpeiseId
                                  join al in _db.Allergene on amsid.aid equals al.id
                                  where amsid.sid == mt.id
                                  select al).ToList();
                    AllergeneSpeise.Add(mt.id, listAl);
                }
                GewählteSpeise = AlleSpeisen.Where(x => x.id == speiseId).FirstOrDefault();
            }
        }
    }
}