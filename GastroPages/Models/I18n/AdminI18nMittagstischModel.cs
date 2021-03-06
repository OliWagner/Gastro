﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GastroPages.Models
{
    public class AdminI18nMittagstischModel
    {
        public string Englisch_Bezeichnung { get; set; }
        public string Italienisch_Bezeichnung { get; set; }
        public string Spanisch_Bezeichnung { get; set; }
        public string Russisch_Bezeichnung { get; set; }
        public string Englisch_Beschreibung { get; set; }
        public string Italienisch_Beschreibung { get; set; }
        public string Spanisch_Beschreibung { get; set; }
        public string Russisch_Beschreibung { get; set; }
        public string Deutsch_Bezeichnung { get; set; }
        public string Deutsch_Beschreibung { get; set; }
        public int MittagstischId { get; set; }

        public List<I18n> liste { get; set; }

        public AdminI18nMittagstischModel()
        {
            using (GastroEntities _db = new GastroEntities())
            {
                liste = (from I18n i18n in _db.I18n select i18n).ToList();
                MittagstischId = 0;
            }
        }

        public AdminI18nMittagstischModel(int typId, int mittagstischId) { 
            using (GastroEntities _db = new GastroEntities())
            {
                // 1 - Deutsch
                // 2 - Italienisch
                // 3 - Spanisch
                // 4 - Russisch
                // 5 - Englisch
                Mittagstisch all = (from Mittagstisch al in _db.Mittagstisch where al.id == mittagstischId select al).FirstOrDefault();
                Deutsch_Beschreibung = all.Beschreibung;
                Deutsch_Bezeichnung = all.Bezeichnung;
                liste = (from I18n i18n in _db.I18n where i18n.Typ == typId && i18n.AllergenId == mittagstischId select i18n).ToList();
                Englisch_Bezeichnung = (from I18n x in liste where x.SprachId == 5 select x.Bezeichnung).FirstOrDefault();
                Englisch_Beschreibung = (from I18n x in liste where x.SprachId == 5 select x.Beschreibung).FirstOrDefault();
                Italienisch_Bezeichnung = (from I18n x in liste where x.SprachId == 2 select x.Bezeichnung).FirstOrDefault();
                Italienisch_Beschreibung = (from I18n x in liste where x.SprachId == 2 select x.Beschreibung).FirstOrDefault();
                Spanisch_Bezeichnung = (from I18n x in liste where x.SprachId == 3 select x.Bezeichnung).FirstOrDefault();
                Spanisch_Beschreibung = (from I18n x in liste where x.SprachId == 3 select x.Beschreibung).FirstOrDefault();
                Russisch_Bezeichnung = (from I18n x in liste where x.SprachId == 4 select x.Bezeichnung).FirstOrDefault();
                Russisch_Beschreibung = (from I18n x in liste where x.SprachId == 4 select x.Beschreibung).FirstOrDefault();
                MittagstischId = mittagstischId;
            }
        }  
    }
}