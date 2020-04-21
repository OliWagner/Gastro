using GastroPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GastroPages.Models
{
    public class PdfPlanerModel
    {
        public string DsvgoOk { get; set; }
        public string DsvgoName { get; set; }
        public string DsvgoDatum { get; set; }
        public string DsvgoMail { get; set; }
        public string DsvgoTelefon { get; set; }

        public string AnzahlPersonenInsgesamt { get; set; }

        public string SummarySummeSpeisen { get; set; }
        public string SummarySummeGetränke { get; set; }
        public string SummarySummeGesamt { get; set; }

    public List<Tuple<string, string, string>> KategorienSpeisen { get; set; }
    public List<List<Tuple<string, string, string, string>>> ItemsSpeisen { get; set; }
    public List<Tuple<string, string, string>> KategorienGetränke { get; set; }
    public List<List<Tuple<string, string, string, string>>> ItemsGetränke { get; set; }

    public PdfPlanerModel()
        {
        }
    }
}