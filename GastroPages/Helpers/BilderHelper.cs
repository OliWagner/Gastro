using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Helpers
{
    public static class BilderHelper
    {
        public static void AddBildNews(string url, string text, string newsId)
        {
            using (GastroEntities _db = new GastroEntities()) {
                NewsBilder bild = new NewsBilder();
                bild.BildUrl = url;
                bild.BildText = text;
                bild.NewsId = Int32.Parse(newsId);
                _db.NewsBilder.Add(bild);
                _db.SaveChanges();
            }
                
            
        }
    }
}