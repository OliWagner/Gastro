using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminGetränkeModel
    {
        public AdminKategorienModel AKModel { get; set; }

        public List<Getränke> AlleGetränke { get; set; }
        public Getränke GewähltesGetränk { get; set; }

        public Dictionary<int, List<Allergene>> AllergeneSpeise = new Dictionary<int, List<Allergene>>();
        public List<Allergene> AlleAllergene { get; set; }

        public AdminGetränkeModel()
        {
                AKModel = new AdminKategorienModel("Getränke", "GetränkeKategorieEintragen", 2);
        }

        public AdminGetränkeModel(int id, int level)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                AlleGetränke = (from Getränke mt in _db.Getränke where mt.KategorieId == id select mt).ToList();
                GewähltesGetränk = new Getränke();
                GewähltesGetränk.id = 0;
                AKModel = new AdminKategorienModel("Getränke", "GetränkeKategorieEintragen", 2, id, level);
            }
        }

        public AdminGetränkeModel(int id, int level, int speiseId)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
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
                AKModel = new AdminKategorienModel("Getränke", "GetränkeKategorieEintragen", 2, id, level);
            }
        }
    }
}