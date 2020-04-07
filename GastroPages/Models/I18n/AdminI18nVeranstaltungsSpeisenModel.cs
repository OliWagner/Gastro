using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GastroPages.Models
{
    public class AdminI18nVeranstaltungsSpeisenModel
    {
        public string Englisch_Bezeichnung { get; set; }
        public string Italienisch_Bezeichnung { get; set; }
        public string Spanisch_Bezeichnung { get; set; }
        public string Russisch_Bezeichnung { get; set; }

        public string Englisch_Beschreibung { get; set; }
        public string Italienisch_Beschreibung { get; set; }
        public string Spanisch_Beschreibung { get; set; }
        public string Russisch_Beschreibung { get; set; }

        public string Englisch_Einheit { get; set; }
        public string Italienisch_Einheit { get; set; }
        public string Spanisch_Einheit { get; set; }
        public string Russisch_Einheit { get; set; }

        public string Deutsch_Bezeichnung { get; set; }
        public int GetränkId { get; set; }

        public List<I18n> liste { get; set; }

        public AdminI18nVeranstaltungsSpeisenModel() { }


        public AdminI18nVeranstaltungsSpeisenModel(int typId, int getränkId) { 
            using (GastroEntities _db = new GastroEntities())
            {
                // 1 - Deutsch
                // 2 - Italienisch
                // 3 - Spanisch
                // 4 - Russisch
                // 5 - Englisch
                VeranstaltungsSpeisen all = (from VeranstaltungsSpeisen al in _db.VeranstaltungsSpeisen where al.id == getränkId select al).FirstOrDefault();
                Deutsch_Bezeichnung = all.Bezeichnung;
                liste = (from I18n i18n in _db.I18n where i18n.Typ == typId && i18n.AllergenId == getränkId select i18n).ToList();
                Englisch_Bezeichnung = (from I18n x in liste where x.SprachId == 5 select x.Bezeichnung).FirstOrDefault();
                Englisch_Beschreibung = (from I18n x in liste where x.SprachId == 5 select x.Beschreibung).FirstOrDefault();
                Englisch_Einheit = (from I18n x in liste where x.SprachId == 5 select x.Einheit).FirstOrDefault();

                Italienisch_Bezeichnung = (from I18n x in liste where x.SprachId == 2 select x.Bezeichnung).FirstOrDefault();
                Italienisch_Beschreibung = (from I18n x in liste where x.SprachId == 2 select x.Beschreibung).FirstOrDefault();
                Italienisch_Einheit = (from I18n x in liste where x.SprachId == 2 select x.Einheit).FirstOrDefault();

                Spanisch_Bezeichnung = (from I18n x in liste where x.SprachId == 3 select x.Bezeichnung).FirstOrDefault();
                Spanisch_Beschreibung = (from I18n x in liste where x.SprachId == 3 select x.Beschreibung).FirstOrDefault();
                Spanisch_Einheit = (from I18n x in liste where x.SprachId == 3 select x.Einheit).FirstOrDefault();

                Russisch_Bezeichnung = (from I18n x in liste where x.SprachId == 4 select x.Bezeichnung).FirstOrDefault();
                Russisch_Beschreibung = (from I18n x in liste where x.SprachId == 4 select x.Beschreibung).FirstOrDefault();
                Russisch_Einheit = (from I18n x in liste where x.SprachId == 4 select x.Einheit).FirstOrDefault();

                GetränkId = getränkId;
            }
        }  
    }
}