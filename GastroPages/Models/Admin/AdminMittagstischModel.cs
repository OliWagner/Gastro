using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminMittagstischModel
    {
        public List<KategorienFuerModel> AlleKategorien { get; set; }
        public KategorienFuerModel SelectedKategorie { get; set; }
        public List<Mittagstisch> AlleSpeisen { get; set; }
        public Mittagstisch GewählteSpeise { get; set; }
        public Dictionary<int, List<Allergene>> AllergeneSpeise = new Dictionary<int, List<Allergene>>();
        public List<Allergene> AlleAllergene { get; set; }

        public AdminMittagstischModel()
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(5);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Mittagstisch mt in _db.Mittagstisch where mt.KategorieId == kat.id select mt).Any();
                    AlleKategorien.Add(kfm);
                }
            }
        }

        public AdminMittagstischModel(int id, int level)
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(5);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Mittagstisch mt in _db.Mittagstisch where mt.KategorieId == kat.id select mt).Any();
                    if(kfm.Kategorie.id == id){
                        SelectedKategorie = kfm;
                        SelectedKategorie.Level = level;
                    }
                    AlleKategorien.Add(kfm);
                }
                AlleSpeisen = (from Mittagstisch mt in _db.Mittagstisch where mt.KategorieId == id select mt).ToList();

                foreach (Mittagstisch mt in AlleSpeisen)
                {
                    List<int> list = (from AllergeneMittagstischIdSpeiseId amsid in _db.AllergeneMittagstischIdSpeiseId where amsid.sid == mt.id select amsid.aid).ToList();

                    var listAl = (from amsid in _db.AllergeneMittagstischIdSpeiseId
                                  join al in _db.Allergene on amsid.aid equals al.id
                                  where amsid.sid == mt.id
                                  select al).ToList();
                    AllergeneSpeise.Add(mt.id, listAl);
                }

                GewählteSpeise = new Mittagstisch();
                GewählteSpeise.id = 0;
            }
        }

        public AdminMittagstischModel(int id, int level, int speiseId)
        {
            
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(5);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Mittagstisch mt in _db.Mittagstisch where mt.KategorieId == kat.id select mt).Any();
                    if (kfm.Kategorie.id == id)
                    {
                        SelectedKategorie = kfm;
                        SelectedKategorie.Level = level;
                    }
                    AlleKategorien.Add(kfm);
                }
                AlleSpeisen = (from Mittagstisch mt in _db.Mittagstisch where mt.KategorieId == id select mt).ToList();
                foreach (Mittagstisch mt in AlleSpeisen)
                {
                    List<int> list = (from AllergeneMittagstischIdSpeiseId amsid in _db.AllergeneMittagstischIdSpeiseId where amsid.sid == mt.id select amsid.aid).ToList();

                    var listAl = (from amsid in _db.AllergeneMittagstischIdSpeiseId
                                  join al in _db.Allergene on amsid.aid equals al.id
                                  where amsid.sid == mt.id
                                  select al).ToList();
                    AllergeneSpeise.Add(mt.id, listAl);
                }
                GewählteSpeise = AlleSpeisen.Where(x => x.id == speiseId).FirstOrDefault();
            }
        }
    }

    public class KategorienFuerModel {
        public Kategorien Kategorie { get; set; }
        public bool SelectedKategorieHasChilds { get; set; }
        public bool SelectedKategorieHasItems { get; set; }
        public int Level { get; set; }
    }
}