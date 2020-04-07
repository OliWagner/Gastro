using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GastroPages.Models
{
    public class AdminI18nKategorienModel
    {
        public string Englisch_Bezeichnung { get; set; }
        public string Italienisch_Bezeichnung { get; set; }
        public string Spanisch_Bezeichnung { get; set; }
        public string Russisch_Bezeichnung { get; set; }

        public string Englisch_Header { get; set; }
        public string Italienisch_Header { get; set; }
        public string Spanisch_Header { get; set; }
        public string Russisch_Header { get; set; }

        public string Englisch_Footer { get; set; }
        public string Italienisch_Footer { get; set; }
        public string Spanisch_Footer { get; set; }
        public string Russisch_Footer { get; set; }

        public string Deutsch_Bezeichnung { get; set; }
        public int KategorieId { get; set; }
        public int KategorieArt { get; set; }

        public List<I18n> liste { get; set; }

        public AdminI18nKategorienModel() { }


        public AdminI18nKategorienModel(int typId, int kategorieId, int kategorieArt) { 
            using (GastroEntities _db = new GastroEntities())
            {
                // 1 - Deutsch
                // 2 - Italienisch
                // 3 - Spanisch
                // 4 - Russisch
                // 5 - Englisch
                Kategorien all = (from Kategorien al in _db.Kategorien where al.id == kategorieId && al.Kategorieart == kategorieArt select al).FirstOrDefault();
                Deutsch_Bezeichnung = all.Bezeichnung;
                liste = (from I18n i18n in _db.I18n where i18n.Typ == typId && i18n.AllergenId == kategorieId select i18n).ToList();
                Englisch_Bezeichnung = (from I18n x in liste where x.SprachId == 5 select x.Bezeichnung).FirstOrDefault();
                Englisch_Header = (from I18n x in liste where x.SprachId == 5 select x.Header).FirstOrDefault();
                Englisch_Footer = (from I18n x in liste where x.SprachId == 5 select x.Footer).FirstOrDefault();

                Italienisch_Bezeichnung = (from I18n x in liste where x.SprachId == 2 select x.Bezeichnung).FirstOrDefault();
                Italienisch_Header = (from I18n x in liste where x.SprachId == 2 select x.Header).FirstOrDefault();
                Italienisch_Footer = (from I18n x in liste where x.SprachId == 2 select x.Footer).FirstOrDefault();

                Spanisch_Bezeichnung = (from I18n x in liste where x.SprachId == 3 select x.Bezeichnung).FirstOrDefault();
                Spanisch_Header = (from I18n x in liste where x.SprachId == 3 select x.Header).FirstOrDefault();
                Spanisch_Footer = (from I18n x in liste where x.SprachId == 3 select x.Footer).FirstOrDefault();

                Russisch_Bezeichnung = (from I18n x in liste where x.SprachId == 4 select x.Bezeichnung).FirstOrDefault();
                Russisch_Header = (from I18n x in liste where x.SprachId == 4 select x.Header).FirstOrDefault();
                Russisch_Footer = (from I18n x in liste where x.SprachId == 4 select x.Footer).FirstOrDefault();

                KategorieId = kategorieId;
                KategorieArt = kategorieArt;
            }
        }  
    }
}