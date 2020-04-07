using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminVeranstaltungsSpeisenModel
    {
        public AdminKategorienModel AKModel { get; set; }

        public List<VeranstaltungsSpeisen> AlleGetränke { get; set; }
        public VeranstaltungsSpeisen GewähltesGetränk { get; set; }

        public Dictionary<int, List<Allergene>> AllergeneSpeise = new Dictionary<int, List<Allergene>>();
        public List<Allergene> AlleAllergene { get; set; }

        public AdminVeranstaltungsSpeisenModel()
        {
            AKModel = new AdminKategorienModel("VeranstaltungsSpeisen", "VeranstaltungsSpeisenKategorieEintragen", 4);
        }

        public AdminVeranstaltungsSpeisenModel(int id, int level)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
                AlleGetränke = (from VeranstaltungsSpeisen mt in _db.VeranstaltungsSpeisen where mt.KategorieSpeise == id select mt).ToList();
                GewähltesGetränk = new VeranstaltungsSpeisen();
                GewähltesGetränk.id = 0;
                AKModel = new AdminKategorienModel("VeranstaltungsSpeisen", "VeranstaltungsSpeisenKategorieEintragen", 4, id, level);
            }
        }

        public AdminVeranstaltungsSpeisenModel(int id, int level, int speiseId)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                AlleAllergene = _db.Allergene.OrderBy(x => x.Nummer).ToList();
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
                AKModel = new AdminKategorienModel("VeranstaltungsSpeisen", "VeranstaltungsSpeisenKategorieEintragen", 4, id , level);
            }
        }
    }
}