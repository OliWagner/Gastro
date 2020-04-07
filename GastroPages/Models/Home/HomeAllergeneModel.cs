using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class HomeAllergeneModel
    {
        public List<Allergene> AlleAllergene { get; set; }

        public HomeAllergeneModel()
        {
            AlleAllergene = new List<Allergene>();
            using (GastroEntities _db = new GastroEntities())
            {
                int typId = 1;
                int sprachId = CultureHelper.GetCurrentCultureId();

                AlleAllergene = (from Allergene al in _db.Allergene select al).ToList();
                if (sprachId != 1) { 
                    foreach (Allergene all in AlleAllergene)
                    {
                        var element = (from I18n i18n in _db.I18n where i18n.Typ == typId && i18n.AllergenId == all.id && i18n.SprachId == sprachId select i18n).FirstOrDefault();
                        if (element != null)
                        {
                            all.Bezeichnung = element.Bezeichnung;
                            all.Beschreibung = element.Beschreibung;
                        }
                    }
                }
            }
        }
    }
}