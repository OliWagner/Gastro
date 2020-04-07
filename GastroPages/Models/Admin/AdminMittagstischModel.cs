using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminMittagstischModel
    {
        public AdminKategorienModel AKModel { get; set; }

        public List<Mittagstisch> AlleSpeisen { get; set; }
        public Mittagstisch GewählteSpeise { get; set; }
        public Dictionary<int, List<Allergene>> AllergeneSpeise = new Dictionary<int, List<Allergene>>();
        public List<Allergene> AlleAllergene { get; set; }

        public AdminMittagstischModel()
        {
            AKModel = new AdminKategorienModel("Mittagstisch", "MittagstischKategorieEintragen", 5);
        }

        public AdminMittagstischModel(int id, int level)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
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
                AKModel = new AdminKategorienModel("Mittagstisch", "MittagstischKategorieEintragen", 5, id, level);
            }
        }

        public AdminMittagstischModel(int id, int level, int speiseId)
        {
            
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
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
                AKModel = new AdminKategorienModel("Mittagstisch", "MittagstischKategorieEintragen", 5, id, level);
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