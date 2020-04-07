using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GastroPages.Models
{
    public class HomeGetränkeModel
    {
        public List<KategorienFuerModel> AlleKategorien { get; set; }
        public Dictionary<int, List<Getränke>> AlleSpeisenZuDenKategorien = new Dictionary<int, List<Getränke>>();
        public Dictionary<int, string> AllergeneZuSpeisen = new Dictionary<int, string>();
        public List<Allergene> AlleAllergene = new HomeAllergeneModel().AlleAllergene;

        public HomeGetränkeModel()
        {
            AlleKategorien = new List<KategorienFuerModel>();
            using (GastroEntities _db = new GastroEntities())
            {
                List<Kategorien> alleKategorien = KategorienHelper.LoadKategorien(2);
                List<Getränke> AlleGetränke = (from Getränke mt in _db.Getränke where mt.IstAktiv orderby mt.Sortierung ascending select mt).ToList();
                foreach (Kategorien kat in alleKategorien)
                {
                    KategorienFuerModel kfm = new KategorienFuerModel();
                    kfm.Kategorie = kat;
                    kfm.SelectedKategorieHasChilds = (from Kategorien k in _db.Kategorien where k.Oberkategorie == kat.id select k).Any();
                    kfm.SelectedKategorieHasItems = (from Getränke mt in _db.Getränke where mt.KategorieId == kat.id select mt).Any();

                    bool checker = true;
                    int levelCounter = 0;
                    int oberkategorie = (int)kat.Oberkategorie;
                    while (checker)
                    {
                        Kategorien _kat = (from Kategorien k in _db.Kategorien where k.id == oberkategorie select k).FirstOrDefault();
                        if (_kat != null)
                        {
                            levelCounter++;
                            kfm.Level = levelCounter;
                            oberkategorie = (int)_kat.Oberkategorie;
                        }
                        else
                        {
                            checker = false;
                        }
                    }
                    AlleKategorien.Add(kfm);
                    //Internationalisieren
                    int sprachId = CultureHelper.GetCurrentCultureId();
                    if (sprachId != 1)
                    {
                        foreach (KategorienFuerModel all in AlleKategorien)
                        {
                            var element = (from I18n i18n in _db.I18n where i18n.Typ == 3 && i18n.AllergenId == all.Kategorie.id && i18n.SprachId == sprachId select i18n).FirstOrDefault();
                            if (element != null)
                            {
                                all.Kategorie.Bezeichnung = element.Bezeichnung;
                                all.Kategorie.Header = element.Header;
                                all.Kategorie.Footer = element.Footer;
                            }
                        }
                    }

                    List<Getränke> TempSpeisen = new List<Getränke>();
                    foreach (Getränke mt in AlleGetränke)
                    {
                        if (mt.KategorieId == kat.id) {
                            //Internationalisieren
                            var element = (from I18n i18n in _db.I18n where i18n.Typ == 2 && i18n.AllergenId == mt.id && i18n.SprachId == sprachId select i18n).FirstOrDefault();
                            if (element != null)
                            {
                                mt.Bezeichnung = element.Bezeichnung;
                                mt.Ergänzung1 = element.Ergänzung1;
                                mt.Ergänzung2 = element.Ergänzung2;
                            }
                            List<string> liste = (from AllergeneGetränkeIdSpeiseId amsid in _db.AllergeneGetränkeIdSpeiseId
                                                  join Allergene al in _db.Allergene on amsid.aid equals al.id
                                                  where amsid.sid == mt.id
                                                  select al.Nummer).ToList();
                            StringBuilder sb = new StringBuilder();
                            string txt = "";
                            if (liste.Count > 0)
                            {
                                sb.Append("(");
                                foreach (string item in liste)
                                {
                                    sb.Append(item + ", ");
                                }
                                sb.Append(")");
                                txt = sb.ToString().Replace(", )", ")");
                                AllergeneZuSpeisen.Add(mt.id, txt);
                            }
                            TempSpeisen.Add(mt);

                        }
                    }
                    AlleSpeisenZuDenKategorien.Add(kat.id, TempSpeisen);
                }
            }
        }
    }
}