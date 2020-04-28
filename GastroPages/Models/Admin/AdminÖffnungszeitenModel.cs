using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminÖffnungszeitenModel
    {
        public Öffnungszeiten Montag { get; set; }
        public Öffnungszeiten Dienstag { get; set; }
        public Öffnungszeiten Mittwoch { get; set; }
        public Öffnungszeiten Donnerstag { get; set; }
        public Öffnungszeiten Freitag { get; set; }
        public Öffnungszeiten Samstag { get; set; }
        public Öffnungszeiten Sonntag { get; set; }
        public string Vorwort { get; set; }
        public string Nachwort { get; set; }

        public AdminÖffnungszeitenModel() {
            using (GastroEntities _db = new GastroEntities()) {
                List<Öffnungszeiten> liste = _db.Öffnungszeiten.ToList();
                Montag = liste.Where(x => x.Wochentag == 1).FirstOrDefault();
                Dienstag = liste.Where(x => x.Wochentag == 2).FirstOrDefault();
                Mittwoch = liste.Where(x => x.Wochentag == 3).FirstOrDefault();
                Donnerstag = liste.Where(x => x.Wochentag == 4).FirstOrDefault();
                Freitag = liste.Where(x => x.Wochentag == 5).FirstOrDefault();
                Samstag = liste.Where(x => x.Wochentag == 6).FirstOrDefault();
                Sonntag = liste.Where(x => x.Wochentag == 7).FirstOrDefault();

                Vorwort = liste.Where(x => x.Wochentag == 8).FirstOrDefault().Ergänzung1;
                Nachwort = liste.Where(x => x.Wochentag == 9).FirstOrDefault().Ergänzung1;
            }

        }
    }
}