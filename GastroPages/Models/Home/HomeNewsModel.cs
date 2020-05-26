using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class HomeNewsModel
    {
        public News News { get; set; }
        public List<News> AlleNews { get; set; }
        public List<NewsBilder> Bilder { get; set; }
        

        public HomeNewsModel()
        {
            Bilder = new List<NewsBilder>();
            using (GastroEntities _db = new GastroEntities())
            {
                News = (from News news in _db.News orderby news.id descending select news).FirstOrDefault();
                AlleNews = (from News b in _db.News select b).ToList();
                if (News != null)
                {
                    AlleNews.Remove(News);
                    Bilder = (from NewsBilder nb in _db.NewsBilder where nb.NewsId == News.id select nb).ToList();
                }
                else
                {
                    Bilder = new List<NewsBilder>();
                    News = new News();
                }
            }
        }

        public HomeNewsModel(int id)
        {
            Bilder = new List<NewsBilder>();
            using (GastroEntities _db = new GastroEntities())
            {
                News = (from News news in _db.News where news.id == id select news).FirstOrDefault();
                AlleNews = (from News b in _db.News select b).ToList();
                AlleNews.Remove(News);
                Bilder = (from NewsBilder nb in _db.NewsBilder where nb.NewsId == News.id select nb).ToList();
            }
        }
    }
}