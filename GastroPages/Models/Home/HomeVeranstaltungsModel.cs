using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GastroPages.Models
{
    public class HomeVeranstaltungsModel
    {
        public HomeVeranstaltungsGetränkeModel Getränke;
        public HomeVeranstaltungsSpeisenModel Speisen;


        public HomeVeranstaltungsModel()
        {
            Getränke = new HomeVeranstaltungsGetränkeModel();
            Speisen = new HomeVeranstaltungsSpeisenModel();
        }
    }
}