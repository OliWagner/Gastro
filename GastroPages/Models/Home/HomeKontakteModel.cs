using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class HomeKontakteModel
    {
        public AdminKontakte Kontakt { get; set; }

        public HomeKontakteModel()
        {
            using (GastroEntities db = new GastroEntities())
            {
                Kontakt = (from AdminKontakte a in db.AdminKontakte where a.id == 1 select a).FirstOrDefault();
                int sprachId = CultureHelper.GetCurrentCultureId();

             
                if (sprachId != 1)
                {
                        var element = (from I18n i18n in db.I18n where i18n.Typ == 9 && i18n.AllergenId == 1 && i18n.SprachId == sprachId select i18n).FirstOrDefault();
                    if (element != null)
                    {
                        Kontakt.Einleitungstext = element.Ergänzung1;
                        Kontakt.TextFuerNachricht = element.Ergänzung2;
                    }
                    else {
                        Kontakt.Einleitungstext = "";
                        Kontakt.TextFuerNachricht = "";
                    }
                    
                }
            }
            
        }
    }
}