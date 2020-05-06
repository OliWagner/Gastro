using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace GastroPages.Models
{
    public class AdminIndexModel
    {
        public List<Kontakte> Kontakte { get; set; }
        public List<Reservierungen> Reservierungen { get; set; }
        public List<Veranstaltungen> Veranstaltungsanfragen { get; set; }

        public AdminIndexModel()
        {
            using (GastroEntities db = new GastroEntities())
            {
                Kontakte = (from Kontakte a in db.Kontakte orderby a.id select a).ToList();
                Reservierungen = (from Reservierungen a in db.Reservierungen orderby a.id select a).ToList();
                Veranstaltungsanfragen = (from Veranstaltungen a in db.Veranstaltungen orderby a.id select a).ToList();
                
            }

        }
    }
    public class VeranstaltungsAnfragen {
        public DateTime Datum { get; set; }
        public string Dateipfad { get; set; }
    }
}