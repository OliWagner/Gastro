using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminVeranstaltungsGetränkeModel
    {
        //public List<KategorienFuerModel> AlleKategorien { get; set; }
        //public KategorienFuerModel SelectedKategorie { get; set; }
        public AdminKategorienModel AKModel { get; set; }


        public List<VeranstaltungsGetränke> AlleGetränke { get; set; }
        public VeranstaltungsGetränke GewähltesGetränk { get; set; }

        public Dictionary<int, List<Allergene>> AllergeneSpeise = new Dictionary<int, List<Allergene>>();
        public List<Allergene> AlleAllergene { get; set; }

        public AdminVeranstaltungsGetränkeModel()
        {
            AKModel = new AdminKategorienModel("VeranstaltungsGetränke", "VeranstaltungsGetränkeKategorieEintragen", 3);
        }

        public AdminVeranstaltungsGetränkeModel(int id, int level)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                AlleGetränke = (from VeranstaltungsGetränke mt in _db.VeranstaltungsGetränke where mt.KategorieGetränke == id select mt).ToList();
                GewähltesGetränk = new VeranstaltungsGetränke();
                GewähltesGetränk.id = 0;
                AKModel = new AdminKategorienModel("VeranstaltungsGetränke", "VeranstaltungsGetränkeKategorieEintragen", 3, id, level);
            }
        }

        public AdminVeranstaltungsGetränkeModel(int id, int level, int speiseId)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                AlleGetränke = (from VeranstaltungsGetränke mt in _db.VeranstaltungsGetränke where mt.KategorieGetränke == id select mt).ToList();
                foreach (VeranstaltungsGetränke mt in AlleGetränke)
                {
                    List<int> list = (from AllergeneVeranstaltungsGetränkeIdSpeiseId amsid in _db.AllergeneVeranstaltungsGetränkeIdSpeiseId where amsid.sid == mt.id select amsid.aid).ToList();

                    var listAl = (from amsid in _db.AllergeneVeranstaltungsGetränkeIdSpeiseId
                                  join al in _db.Allergene on amsid.aid equals al.id
                                  where amsid.sid == mt.id
                                  select al).ToList();
                    AllergeneSpeise.Add(mt.id, listAl);
                }
                GewähltesGetränk = AlleGetränke.Where(x => x.id == speiseId).FirstOrDefault();
                AKModel = new AdminKategorienModel("VeranstaltungsGetränke", "VeranstaltungsGetränkeKategorieEintragen", 3, id, level);
            }
        }
    }
}