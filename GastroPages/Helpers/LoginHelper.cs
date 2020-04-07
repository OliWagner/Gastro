using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Helpers
{
    public static class LoginHelper
    {

        public static void SetSessions(GastroEntities _db, string mailadress)
        {
            Benutzer User = (from Benutzer b in _db.Benutzer where b.Email.Equals(mailadress) && b.IstAktiv select b).FirstOrDefault();
            if (User != null) {
                HttpContext.Current.Session["UserId"] = User.id;
                HttpContext.Current.Session["Nachname"] = User.Nachname;
                HttpContext.Current.Session["Vorname"] = User.Vorname;
                if (User.IstAdmin)
                {
                    HttpContext.Current.Session["Rolle"] = "Admin";
                }
                else
                {
                    HttpContext.Current.Session["Rolle"] = "User";
                }
            }
        }

        public static void KillSessions()
        {
                HttpContext.Current.Session["UserId"] = null;
                HttpContext.Current.Session["Nachname"] = null;
                HttpContext.Current.Session["Vorname"] = null;
                HttpContext.Current.Session["Rolle"] = null;
        }
    }
}