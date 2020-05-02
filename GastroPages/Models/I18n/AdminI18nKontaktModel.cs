using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GastroPages.Models
{
    public class AdminI18nKontaktModel
    {
        public string Englisch_Einleitung { get; set; }
        public string Spanisch_Einleitung { get; set; }
        public string Russisch_Einleitung { get; set; }
        public string Italienisch_Einleitung { get; set; }

        public string Englisch_Nachrichtentext { get; set; }
        public string Italienisch_Nachrichtentext { get; set; }
        public string Spanisch_Nachrichtentext { get; set; }
        public string Russisch_Nachrichtentext { get; set; }

        public List<I18n> liste { get; set; }

        public AdminI18nKontaktModel() { 
            using (GastroEntities _db = new GastroEntities())
            {
                // 1 - Deutsch
                // 2 - Italienisch
                // 3 - Spanisch
                // 4 - Russisch
                // 5 - Englisch
                
                liste = (from I18n i18n in _db.I18n where i18n.Typ == 9 && i18n.AllergenId == 1 select i18n).ToList();
                Englisch_Einleitung = (from I18n x in liste where x.SprachId == 5 select x.Ergänzung1).FirstOrDefault();
                Italienisch_Einleitung = (from I18n x in liste where x.SprachId == 2 select x.Ergänzung1).FirstOrDefault();
                Spanisch_Einleitung = (from I18n x in liste where x.SprachId == 3 select x.Ergänzung1).FirstOrDefault();
                Russisch_Einleitung = (from I18n x in liste where x.SprachId == 4 select x.Ergänzung1).FirstOrDefault();


                Englisch_Nachrichtentext = (from I18n x in liste where x.SprachId == 5 select x.Ergänzung2).FirstOrDefault();
                Italienisch_Nachrichtentext = (from I18n x in liste where x.SprachId == 2 select x.Ergänzung2).FirstOrDefault();
                Spanisch_Nachrichtentext = (from I18n x in liste where x.SprachId == 3 select x.Ergänzung2).FirstOrDefault();
                Russisch_Nachrichtentext = (from I18n x in liste where x.SprachId == 4 select x.Ergänzung2).FirstOrDefault();
            }
        }  
    }
}