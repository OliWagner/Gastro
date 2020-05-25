using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class UmfrageModelHelper {
        public List<UmfrageAntworten> Antworten { get; set; }
        public List<UmfrageBilder> Bilder { get; set; }
        public int UmfrageTeilnahmen { get; set; }
        public Umfragen Umfrage { get; set; }
    }

    public class HomeUmfrageModel
    {
        
        public List<UmfrageModelHelper> AlleDaten = new List<UmfrageModelHelper>();


        public HomeUmfrageModel()
        {
            using (GastroEntities _db = new GastroEntities())
            {
                List<Umfragen> AlleUmfragen = (from Umfragen u in _db.Umfragen where u.DatumStart < DateTime.Now && u.DatumEnde > DateTime.Now orderby u.id descending select u).ToList();
                foreach (Umfragen umfrage in AlleUmfragen)
                {
                    UmfrageModelHelper umh = new UmfrageModelHelper();
                    umh.Umfrage = umfrage;
                    umh.Antworten = (from UmfrageAntworten ua in _db.UmfrageAntworten where ua.UmfrageId == umfrage.id select ua).ToList();
                    umh.Bilder = (from UmfrageBilder ub in _db.UmfrageBilder where ub.UmfrageId == umfrage.id select ub).ToList();
                    umh.UmfrageTeilnahmen = (from UmfrageErgebnisse ub in _db.UmfrageErgebnisse where ub.UmfrageId == umfrage.id select ub).Distinct().ToList().Count();
                    AlleDaten.Add(umh);
                }
            }
        }
    }
}