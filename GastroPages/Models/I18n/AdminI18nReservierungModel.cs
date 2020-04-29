using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GastroPages.Models
{
    public class AdminI18nReservierungModel
    {
        public string Englisch_Ansprache { get; set; }
        public string Italienisch_Ansprache { get; set; }
        public string Spanisch_Ansprache { get; set; }
        public string Russisch_Ansprache { get; set; }
        

        public string Englisch_WichtigerHinweis { get; set; }
        public string Italienisch_WichtigerHinweis { get; set; }
        public string Spanisch_WichtigerHinweis { get; set; }
        public string Russisch_WichtigerHinweis { get; set; }

        public List<I18n> liste { get; set; }

        public AdminI18nReservierungModel() { 
            using (GastroEntities _db = new GastroEntities())
            {
                // 2 - Italienisch
                // 3 - Spanisch
                // 4 - Russisch
                // 5 - Englisch
                //Ansprache
                Öffnungszeiten öz = (from Öffnungszeiten ö in _db.Öffnungszeiten where ö.id == 10 select ö).FirstOrDefault();
                liste = (from I18n i18n in _db.I18n where i18n.Typ == 9 && i18n.AllergenId == 10 select i18n).ToList();

                Englisch_Ansprache = (from I18n x in liste where x.SprachId == 5 select x.Ergänzung1).FirstOrDefault();
                Italienisch_Ansprache = (from I18n x in liste where x.SprachId == 2 select x.Ergänzung1).FirstOrDefault();
                Spanisch_Ansprache = (from I18n x in liste where x.SprachId == 3 select x.Ergänzung1).FirstOrDefault();
                Russisch_Ansprache = (from I18n x in liste where x.SprachId == 4 select x.Ergänzung1).FirstOrDefault();

                //Wichtiger Hinweis
                öz = (from Öffnungszeiten ö in _db.Öffnungszeiten where ö.id == 11 select ö).FirstOrDefault();
                liste = (from I18n i18n in _db.I18n where i18n.Typ == 9 && i18n.AllergenId == 11 select i18n).ToList();

                Englisch_WichtigerHinweis = (from I18n x in liste where x.SprachId == 5 select x.Ergänzung1).FirstOrDefault();
                Italienisch_WichtigerHinweis = (from I18n x in liste where x.SprachId == 2 select x.Ergänzung1).FirstOrDefault();
                Spanisch_WichtigerHinweis = (from I18n x in liste where x.SprachId == 3 select x.Ergänzung1).FirstOrDefault();
                Russisch_WichtigerHinweis = (from I18n x in liste where x.SprachId == 4 select x.Ergänzung1).FirstOrDefault();

            }
        }  
    }
}