using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class HomeReservierungModel
    {
        public string Ansprache { get; set; }
        public string WichtigerHinweis { get; set; }
        public string Name { get; set; }
        public string Telefon { get; set; }
        public string Datum { get; set; }
        public string Uhrzeit { get; set; }
        public int Personenzahl { get; set; }
        public string Mitteilung { get; set; }

        public HomeReservierungModel() {
            using (GastroEntities _db = new GastroEntities())
            {
                int typId = 9;
                int sprachId = CultureHelper.GetCurrentCultureId();
                
                List<Öffnungszeiten> liste = _db.Öffnungszeiten.ToList();
                Ansprache = liste.Where(x => x.Wochentag == 10).FirstOrDefault().Ergänzung1;
                WichtigerHinweis = liste.Where(x => x.Wochentag == 11).FirstOrDefault().Ergänzung1;
                if (sprachId != 1)
                {
                    foreach (Öffnungszeiten öz in liste)
                    {
                        I18n element = (from I18n i18n in _db.I18n where i18n.Typ == typId && i18n.AllergenId == öz.Wochentag && i18n.SprachId == sprachId select i18n).FirstOrDefault();
                        if (element != null)
                        {
                            if (öz.Wochentag == 10)
                            {
                                Ansprache = element.Ergänzung1;
                            }
                            if (öz.Wochentag == 11)
                            {
                                WichtigerHinweis = element.Ergänzung1;
                            }
                        }
                    }
                }
            }
        }

    }
}