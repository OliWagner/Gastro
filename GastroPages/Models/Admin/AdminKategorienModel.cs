using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminKategorienModel
    {
        public List<KategorienFuerModel> AlleKategorien { get; set; }
        public KategorienFuerModel SelectedKategorie { get; set; }

        public string ActionLink { get; set; }
        public string ControllerAction { get; set; }


        public AdminKategorienModel() { }
    

        public AdminKategorienModel(string aLink, string cAction, int kategorieArt)
        {
            ActionLink = aLink;
            ControllerAction = cAction;
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(kategorieArt);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    switch (kategorieArt)
                    {
                        //Speisen
                        case 1:
                            kfm.SelectedKategorieHasItems = (from Speisen mt in _db.Speisen where mt.KategorieId == kat.id select mt).Any();
                            break;
                        //Getränke
                        case 2:
                            kfm.SelectedKategorieHasItems = (from Getränke mt in _db.Getränke where mt.KategorieId == kat.id select mt).Any();
                            break;
                        //Veranst-Getr.
                        case 3:
                            kfm.SelectedKategorieHasItems = (from VeranstaltungsGetränke mt in _db.VeranstaltungsGetränke where mt.KategorieGetränke == kat.id select mt).Any();
                            break;
                        //Veranst.-Speisen
                        case 4:
                            kfm.SelectedKategorieHasItems = (from VeranstaltungsSpeisen mt in _db.VeranstaltungsSpeisen where mt.KategorieSpeise == kat.id select mt).Any();
                            break;
                        //Mittagstisch
                        case 5:
                            kfm.SelectedKategorieHasItems = (from Mittagstisch mt in _db.Mittagstisch where mt.KategorieId == kat.id select mt).Any();
                            break;

                    }
                    AlleKategorien.Add(kfm);
                }
            }
        }

        public AdminKategorienModel(string aLink, string cAction, int kategorieArt, int id, int level)
        {
            ActionLink = aLink;
            ControllerAction = cAction;
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(kategorieArt);
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    switch (kategorieArt)
                    {
                        //Speisen
                        case 1:
                            kfm.SelectedKategorieHasItems = (from Speisen mt in _db.Speisen where mt.KategorieId == kat.id select mt).Any();
                            break;
                        //Getränke
                        case 2:
                            kfm.SelectedKategorieHasItems = (from Getränke mt in _db.Getränke where mt.KategorieId == kat.id select mt).Any();
                            break;
                        //Veranst-Getr.
                        case 3:
                            kfm.SelectedKategorieHasItems = (from VeranstaltungsGetränke mt in _db.VeranstaltungsGetränke where mt.KategorieGetränke == kat.id select mt).Any();
                            break;
                        //Veranst.-Speisen
                        case 4:
                            kfm.SelectedKategorieHasItems = (from VeranstaltungsSpeisen mt in _db.VeranstaltungsSpeisen where mt.KategorieSpeise == kat.id select mt).Any();
                            break;
                        //Mittagstisch
                        case 5:
                            kfm.SelectedKategorieHasItems = (from Mittagstisch mt in _db.Mittagstisch where mt.KategorieId == kat.id select mt).Any();
                            break;

                    }
                    if (kfm.Kategorie.id == id){
                        SelectedKategorie = kfm;
                        SelectedKategorie.Level = level;
                    }
                    AlleKategorien.Add(kfm);
                }
            }
        }
    }
}