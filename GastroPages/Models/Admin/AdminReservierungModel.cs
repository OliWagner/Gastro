using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminReservierungModel
    {
        public string Ansprache { get; set; }
        public string WichtigerHinweis { get; set; }

        public AdminReservierungModel() {
            using (GastroEntities _db = new GastroEntities()) {
                Ansprache = (from Öffnungszeiten oez in _db.Öffnungszeiten where oez.Wochentag == 10 select oez.Ergänzung1).FirstOrDefault();
                WichtigerHinweis = (from Öffnungszeiten oez in _db.Öffnungszeiten where oez.Wochentag == 11 select oez.Ergänzung1).FirstOrDefault();
            }
        }

    }
}