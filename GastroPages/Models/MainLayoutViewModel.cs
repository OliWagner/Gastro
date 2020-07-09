using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GastroPages.Models
{
    public class MainLayoutViewModel
    {
        public string TelefonCall { get; set; }
        public string Telefon { get; set; }
        public string Pinterest { get; set; }
        public string Instagramm { get; set; }
        public string Snapchat { get; set; }
        public string Facebook { get; set; }
        public string GooglePlus { get; set; }
        public string Youtube { get; set; }
        public List<News> News { get; set; }
        public List<Umfragen> Umfragen { get; set; }

        public string AdresseName { get; set; }
        public string AdresseStrasse { get; set; }
        public string AdresseZusatz { get; set; }
        public string AdressePlz { get; set; }
        public string AdresseOrt { get; set; }
        public string AdresseEmail { get; set; }
        public string AdresseInhaber { get; set; }

        public MainLayoutViewModel() { 
            using(GastroEntities db = new GastroEntities()){
                AdminKontakte k = (from AdminKontakte ak in db.AdminKontakte where ak.id == 1 select ak).FirstOrDefault();

                AdresseName = k.AdresseName;
                AdresseStrasse = k.AdresseStrasse;
                AdresseZusatz = k.AdresseZusatz;
                AdressePlz = k.AdressePlz;
                AdresseOrt = k.AdresseOrt;
                AdresseEmail = k.Email;
                AdresseInhaber = k.AdresseInhaber;

                Telefon = k.Telefon;
                TelefonCall = "tel:" + k.Telefon.Replace("-", "").Replace("/", "").Replace(" ", "");
                Pinterest = k.Pinterest;
                Instagramm = k.Instagramm;
                Youtube = k.Youtube;
                Snapchat = k.Snapchat;
                Facebook = k.Facebook;
                GooglePlus = k.GooglePlus;

                News = (from News news in db.News orderby news.Datum descending select news).ToList();
                Umfragen = (from Umfragen u in db.Umfragen where u.DatumStart < DateTime.Now && u.DatumEnde > DateTime.Now select u).ToList();
            }
        }
    }
}