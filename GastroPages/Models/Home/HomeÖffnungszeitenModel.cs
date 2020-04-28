using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class HomeÖffnungszeitenModel
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

        public HomeÖffnungszeitenModel()
        {
            using (GastroEntities _db = new GastroEntities())
            {
                int typId = 8;
                int sprachId = CultureHelper.GetCurrentCultureId();

                List<Öffnungszeiten> liste = _db.Öffnungszeiten.ToList();
                Vorwort = liste.Where(x => x.Wochentag == 8).FirstOrDefault().Ergänzung1;
                Nachwort = liste.Where(x => x.Wochentag == 9).FirstOrDefault().Ergänzung1;
                if (sprachId != 1)
                {
                    foreach (Öffnungszeiten öz in liste)
                    {
                        var element = (from I18n i18n in _db.I18n where i18n.Typ == typId && i18n.AllergenId == öz.Wochentag && i18n.SprachId == sprachId select i18n).FirstOrDefault();
                        if (element != null)
                        {
                            öz.Ergänzung1 = element.Ergänzung1;
                            öz.Ergänzung2 = element.Ergänzung2;
                            if (öz.Wochentag == 1) {
                                Vorwort = element.Header;
                            }
                            if (öz.Wochentag == 7)
                            {
                                Nachwort = element.Header;
                            }
                        }
                    }
                }
                Montag = liste.Where(x => x.Wochentag == 1).FirstOrDefault();
                
                Dienstag = liste.Where(x => x.Wochentag == 2).FirstOrDefault();

                Mittwoch = liste.Where(x => x.Wochentag == 3).FirstOrDefault();

                Donnerstag = liste.Where(x => x.Wochentag == 4).FirstOrDefault();

                Freitag = liste.Where(x => x.Wochentag == 5).FirstOrDefault();

                Samstag = liste.Where(x => x.Wochentag == 6).FirstOrDefault();

                Sonntag = liste.Where(x => x.Wochentag == 7).FirstOrDefault();

                
            }

        }
    }
}