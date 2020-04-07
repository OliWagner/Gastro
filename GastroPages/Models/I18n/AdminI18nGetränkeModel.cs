using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GastroPages.Models
{
    public class AdminI18nGetränkeModel
    {
        public string Englisch_Bezeichnung { get; set; }
        public string Italienisch_Bezeichnung { get; set; }
        public string Spanisch_Bezeichnung { get; set; }
        public string Russisch_Bezeichnung { get; set; }

        public string Englisch_Ergänzung1 { get; set; }
        public string Italienisch_Ergänzung1 { get; set; }
        public string Spanisch_Ergänzung1 { get; set; }
        public string Russisch_Ergänzung1 { get; set; }

        public string Englisch_Ergänzung2 { get; set; }
        public string Italienisch_Ergänzung2 { get; set; }
        public string Spanisch_Ergänzung2 { get; set; }
        public string Russisch_Ergänzung2 { get; set; }

        public string Deutsch_Bezeichnung { get; set; }
        public int GetränkId { get; set; }

        public List<I18n> liste { get; set; }

        public AdminI18nGetränkeModel() { }


        public AdminI18nGetränkeModel(int typId, int getränkId) { 
            using (GastroEntities _db = new GastroEntities())
            {
                // 1 - Deutsch
                // 2 - Italienisch
                // 3 - Spanisch
                // 4 - Russisch
                // 5 - Englisch
                Getränke all = (from Getränke al in _db.Getränke where al.id == getränkId select al).FirstOrDefault();
                Deutsch_Bezeichnung = all.Bezeichnung;
                liste = (from I18n i18n in _db.I18n where i18n.Typ == typId && i18n.AllergenId == getränkId select i18n).ToList();
                Englisch_Bezeichnung = (from I18n x in liste where x.SprachId == 5 select x.Bezeichnung).FirstOrDefault();
                Englisch_Ergänzung1 = (from I18n x in liste where x.SprachId == 5 select x.Ergänzung1).FirstOrDefault();
                Englisch_Ergänzung2 = (from I18n x in liste where x.SprachId == 5 select x.Ergänzung2).FirstOrDefault();

                Italienisch_Bezeichnung = (from I18n x in liste where x.SprachId == 2 select x.Bezeichnung).FirstOrDefault();
                Italienisch_Ergänzung1 = (from I18n x in liste where x.SprachId == 2 select x.Ergänzung1).FirstOrDefault();
                Italienisch_Ergänzung2 = (from I18n x in liste where x.SprachId == 2 select x.Ergänzung2).FirstOrDefault();

                Spanisch_Bezeichnung = (from I18n x in liste where x.SprachId == 3 select x.Bezeichnung).FirstOrDefault();
                Spanisch_Ergänzung1 = (from I18n x in liste where x.SprachId == 3 select x.Ergänzung1).FirstOrDefault();
                Spanisch_Ergänzung2 = (from I18n x in liste where x.SprachId == 3 select x.Ergänzung2).FirstOrDefault();

                Russisch_Bezeichnung = (from I18n x in liste where x.SprachId == 4 select x.Bezeichnung).FirstOrDefault();
                Russisch_Ergänzung1 = (from I18n x in liste where x.SprachId == 4 select x.Ergänzung1).FirstOrDefault();
                Russisch_Ergänzung2 = (from I18n x in liste where x.SprachId == 4 select x.Ergänzung2).FirstOrDefault();

                GetränkId = getränkId;
            }
        }  
    }
}