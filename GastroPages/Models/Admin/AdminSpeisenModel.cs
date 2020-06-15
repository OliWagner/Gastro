using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminSpeisenModel
    {
        public AdminKategorienModel AKModel { get; set; }

        public List<Speisen> AlleSpeisen { get; set; }
        public Speisen GewählteSpeise { get; set; }

        public Dictionary<int, List<Allergene>> AllergeneSpeise = new Dictionary<int, List<Allergene>>();
        public List<Allergene> AlleAllergene { get; set; }

        public AdminSpeisenModel()
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AKModel = new AdminKategorienModel("Speisen", "SpeisenKategorieEintragen", 1);
            }
        }

        public AdminSpeisenModel(int id, int level)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                AlleSpeisen = (from Speisen mt in _db.Speisen where mt.KategorieId == id orderby mt.Sortierung select mt).ToList();

                GewählteSpeise = new Speisen();
                GewählteSpeise.id = 0;
                AKModel = new AdminKategorienModel("Speisen", "SpeisenKategorieEintragen", 1, id, level);
            }
        }

        public AdminSpeisenModel(int id, int level, int speiseId)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                AlleSpeisen = (from Speisen mt in _db.Speisen where mt.KategorieId == id orderby mt.Sortierung select mt).ToList();
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
                AKModel = new AdminKategorienModel("Speisen", "SpeisenKategorieEintragen", 1, id, level);
            }
        }
    }
}