using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminBenutzerModel
    {
        public Benutzer Benutzer { get; set; }
        public List<Benutzer> AlleBenutzer { get; set; }

        public AdminBenutzerModel() {
            Benutzer = new Benutzer();
            using (GastroEntities _db = new GastroEntities()) {
                AlleBenutzer = (from Benutzer b in _db.Benutzer select b).ToList();
            }
        }

        public AdminBenutzerModel(Benutzer benutzer)
        {
            Benutzer = benutzer;
            using (GastroEntities _db = new GastroEntities())
            {
                AlleBenutzer = (from Benutzer b in _db.Benutzer select b).ToList();
            }
        }

        public AdminBenutzerModel(int id)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                Benutzer = (from Benutzer b in _db.Benutzer where b.id == id select b).FirstOrDefault();
                AlleBenutzer = (from Benutzer b in _db.Benutzer select b).ToList();
            }
        }
    }
}