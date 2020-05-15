using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminUmfrageModel
    {
        public Umfragen Umfrage { get; set; }
        public List<Umfragen> AlleUmfragen { get; set; }
        public List<UmfrageAntworten> Antworten { get; set; }
        public List<UmfrageBilder> Bilder { get; set; }
        public List<string> DropdownWerte = new List<string> { "Freie Antwort kurz", "Freie Antwort lang", "Einfachauswahl", "Mehrfachauswahl" };
        public string Umfragetyp = "";

        public AdminUmfrageModel() {
            
                
                Antworten = new List<UmfrageAntworten>();
                Bilder = new List<UmfrageBilder>();
                Umfrage = new Umfragen();
                Umfrage.Typ = "";
                Umfrage.id = 0;
                Umfrage.DatumStart = DateTime.Today.AddDays(1);
                Umfrage.DatumEnde = DateTime.Today.AddDays(10);
                using (GastroEntities _db = new GastroEntities())
                {
                    AlleUmfragen = (from Umfragen b in _db.Umfragen orderby b.id descending select b).ToList();
                Bilder = (from UmfrageBilder b in _db.UmfrageBilder where (b.UmfrageId == 1000000000) select b).ToList();
            }
        }

        public AdminUmfrageModel(int id)
        {
                using (GastroEntities _db = new GastroEntities())
                {
                    AlleUmfragen = (from Umfragen b in _db.Umfragen orderby b.id descending select b).ToList();
                    Umfrage = (from Umfragen b in _db.Umfragen where b.id == id select b).FirstOrDefault();
                    AlleUmfragen.Remove(Umfrage);
                    Antworten = (from UmfrageAntworten b in _db.UmfrageAntworten where b.UmfrageId == Umfrage.id select b).ToList();
                    Bilder = (from UmfrageBilder b in _db.UmfrageBilder where (b.UmfrageId == Umfrage.id || b.UmfrageId == 1000000000) select b).ToList();
                    Umfragetyp = Umfrage.Typ;
                }
        }
    }
}