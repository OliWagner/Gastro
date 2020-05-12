using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class AdminNewsModel
    {
        public News News { get; set; }
        public List<News> AlleNews { get; set; }
        public List<NewsBilder> Bilder { get; set; }
        public int SelectedId { get; set; }

        public AdminNewsModel() {
            SelectedId = 0;
            News = new News();
            News.id = 0;
            News.Datum = DateTime.Now;
            Bilder = new List<NewsBilder>();
            using (GastroEntities _db = new GastroEntities()) {
                AlleNews = (from News b in _db.News select b).ToList();
            }
        }

        public AdminNewsModel(int id)
        {
            SelectedId = id;
            Bilder = new List<NewsBilder>();
            using (GastroEntities _db = new GastroEntities())
            {
                News = (from News b in _db.News where b.id == id select b).FirstOrDefault();
                AlleNews = (from News b in _db.News select b).ToList();
                foreach (News n in AlleNews)
                {
                    Bilder = (from NewsBilder nb in _db.NewsBilder where nb.NewsId == n.id select nb).ToList();
                }
                
            }
        }
    }
}