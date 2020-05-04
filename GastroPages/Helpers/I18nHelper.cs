using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Helpers
{
    public static class I18nHelper
    {
        public static I18n CreateInstance(int typ, int sprachId, int allergenId, string bezeichnung, string beschreibung, string ergänzung1, string ergänzung2, string header, string footer, string Einheit) {
            I18n returner = new I18n();
            returner.Typ = typ;
            returner.SprachId = sprachId;
            returner.AllergenId = allergenId;
            returner.Bezeichnung = bezeichnung != null && !bezeichnung.Equals("") ? bezeichnung : " ";
            returner.Beschreibung = beschreibung;
            returner.Ergänzung1 = ergänzung1;
            returner.Ergänzung2 = ergänzung2;
            returner.Header = header;
            returner.Footer = footer;
            returner.Einheit = Einheit;
            return returner;
        }
    }
}