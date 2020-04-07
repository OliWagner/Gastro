using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GastroPages.Helpers
{
    public static class KategorienHelper
    {
        static List<Kategorien> liste;

        public static List<Kategorien> LoadKategorien(int Kategorieart)
        {
            liste = new List<Kategorien>();
            using (GastroEntities _db = new GastroEntities()) {
                List<Kategorien> alleKategorien = (from Kategorien k in _db.Kategorien where k.Kategorieart == Kategorieart orderby k.Sortierung ascending select k).ToList();

                foreach (Kategorien kat in alleKategorien)
                {
                    if (kat.Oberkategorie == 0)
                    {
                        int value = kat.id;
                        liste.Add(kat);
                        FillChild(value);
                    }
                }
            }
            //return TreeView1;
            return liste;
        }

        private static int FillChild(int IID)
        {
            using (GastroEntities _db = new GastroEntities())
            {
                List<Kategorien> alleKats = (from Kategorien k in _db.Kategorien where k.Oberkategorie == IID orderby k.Sortierung ascending select k).ToList();

                if (alleKats.Count > 0)
                {
                    foreach (Kategorien kat in alleKats)
                    {
                        int temp = kat.id;
                        liste.Add(kat);
                        FillChild(temp);
                    }
                    return 0;
                }
                else
                {
                    return 0;
                }
            }  
        }
    }
}