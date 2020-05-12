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
        public string Pinterest { get; set; }
        public string Instagramm { get; set; }
        public string Snapchat { get; set; }
        public string Facebook { get; set; }
        public string GooglePlus { get; set; }
        public string Youtube { get; set; }
        public List<News> News { get; set; }

        public MainLayoutViewModel() { 
            using(GastroEntities db = new GastroEntities()){
                AdminKontakte k = (from AdminKontakte ak in db.AdminKontakte where ak.id == 1 select ak).FirstOrDefault();
                Pinterest = k.Pinterest;
                Instagramm = k.Instagramm;
                Youtube = k.Youtube;
                Snapchat = k.Snapchat;
                Facebook = k.Facebook;
                GooglePlus = k.GooglePlus;

                News = (from News news in db.News orderby news.Datum descending select news).ToList();
            }
        }
    }
}