using GastroPages.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Image = iTextSharp.text.Image;

namespace GastroPages.Helpers
{
    public static class PdfHelper
    {
        public static void MakePdfSpeisen(HomeSpeisenModel homeSpeisenModel)
        {
            Document document = new Document();

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:\\copy\\_Speisen.pdf", FileMode.Create));

                document.Open();

                PdfPTable table = new PdfPTable(4);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 10f, 100f, 10f, 20f };
                table.TotalWidth = 580f;
                table.LockedWidth = true;
                table.SetWidths(widths);

               
                string FONT = HttpContext.Current.Request.PhysicalApplicationPath + "\\fonts\\arial.ttf";
                BaseFont bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font45 = new Font(bf, 45, Font.NORMAL);
                Font font20 = new Font(bf, 20, Font.NORMAL);
                Font font12 = new Font(bf, 12, Font.NORMAL);
                Font font10 = new Font(bf, 10, Font.NORMAL);
                Font font6 = new Font(bf, 6, Font.NORMAL);

                //PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Speisen, FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 45)));
                PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Speisen, font45));
                cellHeader.Border = Rectangle.NO_BORDER;
                cellHeader.Colspan = 2;
                cellHeader.HorizontalAlignment = 0;
                table.AddCell(cellHeader);


                Image jpg = Image.GetInstance(HttpContext.Current.Request.PhysicalApplicationPath + "/Images/logo.png");
                jpg.ScaleToFit(90f, 90f);
                jpg.SpacingAfter = 12f;
                jpg.SpacingBefore = 12f;
                PdfPCell cellImage = new PdfPCell(jpg);
                cellImage.Colspan = 2;
                cellImage.Border = Rectangle.NO_BORDER;
                table.AddCell(cellImage);

                foreach (KategorienFuerModel kat in homeSpeisenModel.AlleKategorien)
                {
                    PdfPTable innerTable = new PdfPTable(4);
                    innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    //float[] widths = new float[] { 10f, 100f, 10f, 20f };
                    innerTable.TotalWidth = 580f;
                    innerTable.LockedWidth = true;
                    innerTable.SetWidths(widths);


                    PdfPCell cell = new PdfPCell(new Phrase(kat.Kategorie.Bezeichnung, font20));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 4;
                    cell.HorizontalAlignment = 0;
                    innerTable.AddCell(cell);

                    PdfPCell cellempty = new PdfPCell(new Phrase(" ", FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 15)));
                    cellempty.Border = Rectangle.NO_BORDER;
                    cellempty.Colspan = 4;
                    cellempty.HorizontalAlignment = 0;
                    innerTable.AddCell(cellempty);

                    PdfPCell cellemptysmall = new PdfPCell(new Phrase(" ", FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 5)));
                    cellemptysmall.Border = Rectangle.NO_BORDER;
                    cellemptysmall.Colspan = 4;
                    cellemptysmall.HorizontalAlignment = 0;
                    

                    if (kat.Kategorie.Header != null && !kat.Kategorie.Header.Equals(""))
                    {
                        string[] texte = kat.Kategorie.Header.Split('|');
                        foreach (var item in texte)
                        {
                            innerTable.AddCell(" ");
                            PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                            icell.Border = Rectangle.NO_BORDER;
                            icell.Colspan = 2;
                            icell.HorizontalAlignment = 0;
                            innerTable.AddCell(icell);
                            innerTable.AddCell(" ");
                            innerTable.AddCell(cellemptysmall);
                        }
                    }
           

                        foreach(var item in homeSpeisenModel.AlleSpeisenZuDenKategorien.Where(x => x.Key == kat.Kategorie.id))
                        {
                            foreach (Speisen speise in item.Value)
                            {
                                    //Item row 1
                                    innerTable.AddCell(speise.Kartennummer);

                                    //Allergene beachten!!!
                                    if (homeSpeisenModel.AllergeneZuSpeisen.Where(x => x.Key == speise.id).Any())
                                    {
                                        string val = homeSpeisenModel.AllergeneZuSpeisen.Where(x => x.Key == speise.id).FirstOrDefault().Value;
                                        Paragraph paragraph = new Paragraph();
                                        Phrase pr = new Phrase(speise.Bezeichnung, font12);
                                        paragraph.Add(pr);
                                        Chunk al = new Chunk(val, font6);
                                        al.SetTextRise(7);
                                        paragraph.Add(al);
                                        innerTable.AddCell(paragraph);
                                    }
                                    else {
                                        Phrase p = new Phrase(speise.Bezeichnung, font12);
                                        PdfPCell cellBez = new PdfPCell(p);
                                        cellBez.Border = Rectangle.NO_BORDER;
                                        innerTable.AddCell(cellBez);
                                    }


                                    innerTable.AddCell("€");

                            Phrase phrasen = new Phrase(speise.Preis.ToString(), font12);
                            PdfPCell cell2n = new PdfPCell(phrasen);
                            cell2n.Border = Rectangle.NO_BORDER;
                            cell2n.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                            innerTable.AddCell(cell2n);
                                    
                            //Item row 2
                            innerTable.AddCell("");
                            Phrase phrase = new Phrase(speise.Beschreibung, font12);
                            PdfPCell cell2 = new PdfPCell(phrase);
                            cell2.Border = Rectangle.NO_BORDER;
                            innerTable.AddCell(cell2);
                            //int hoehe = (int)cell2.Height;
                            innerTable.AddCell("");
                            innerTable.AddCell("");

                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                            }
                        }
           
                        if(kat.Kategorie.Footer != null && !kat.Kategorie.Footer.Equals(""))
                        {
                            string[] texte = kat.Kategorie.Footer.Split('|');
                            foreach (var item in texte)
                            {
                                    innerTable.AddCell(" ");
                                PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                                icell.Border = Rectangle.NO_BORDER;
                                icell.Colspan = 2;
                                icell.HorizontalAlignment = 0;
                                    innerTable.AddCell(icell);
                                    innerTable.AddCell(" ");
                                    innerTable.AddCell(cellemptysmall);
                            }
                        }
                    //Die Tabelle der Kategorien noch in die Gesamttabelle packen
                    PdfPCell tabcell = new PdfPCell(innerTable);
                    tabcell.Border = Rectangle.NO_BORDER;
                    tabcell.Colspan = 4;
                    tabcell.HorizontalAlignment = 0;
                    table.AddCell(tabcell);
                }
    
    document.Add(table);

}
catch (DocumentException de)
{
    var test = 0;
}
catch (IOException ioe)
{
    var test = 0;
}


    // step 5: we close the document
    document.Close();
    }

        public static void MakePdfGetränke(HomeGetränkeModel homeSpeisenModel)
        {
            Document document = new Document();

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:\\copy\\_Getränke.pdf", FileMode.Create));

                document.Open();

                PdfPTable table = new PdfPTable(4);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 10f, 100f, 10f, 20f };
                table.TotalWidth = 580f;
                table.LockedWidth = true;
                table.SetWidths(widths);


                string FONT = HttpContext.Current.Request.PhysicalApplicationPath + "\\fonts\\arial.ttf";
                BaseFont bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font45 = new Font(bf, 45, Font.NORMAL);
                Font font20 = new Font(bf, 20, Font.NORMAL);
                Font font12 = new Font(bf, 12, Font.NORMAL);
                Font font10 = new Font(bf, 10, Font.NORMAL);
                Font font6 = new Font(bf, 6, Font.NORMAL);

                //PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Speisen, FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 45)));
                PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Getränke, font45));
                cellHeader.Border = Rectangle.NO_BORDER;
                cellHeader.Colspan = 2;
                cellHeader.HorizontalAlignment = 0;
                table.AddCell(cellHeader);


                Image jpg = Image.GetInstance(HttpContext.Current.Request.PhysicalApplicationPath + "/Images/logo.png");
                jpg.ScaleToFit(90f, 90f);
                jpg.SpacingAfter = 12f;
                jpg.SpacingBefore = 12f;
                PdfPCell cellImage = new PdfPCell(jpg);
                cellImage.Colspan = 2;
                cellImage.Border = Rectangle.NO_BORDER;
                table.AddCell(cellImage);

                foreach (KategorienFuerModel kat in homeSpeisenModel.AlleKategorien)
                {
                    PdfPTable innerTable = new PdfPTable(5);
                    innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    float[] innerwidths = new float[] { 100f, 15f, 15f, 5f, 10f };
                    innerTable.TotalWidth = 580f;
                    innerTable.LockedWidth = true;
                    innerTable.SetWidths(innerwidths);


                    PdfPCell cell = new PdfPCell(new Phrase(kat.Kategorie.Bezeichnung, font20));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 5;
                    cell.HorizontalAlignment = 0;
                    innerTable.AddCell(cell);

                    PdfPCell cellempty = new PdfPCell(new Phrase(" ", FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 15)));
                    cellempty.Border = Rectangle.NO_BORDER;
                    cellempty.Colspan = 5;
                    cellempty.HorizontalAlignment = 0;
                    innerTable.AddCell(cellempty);

                    PdfPCell cellemptysmall = new PdfPCell(new Phrase(" ", FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 5)));
                    cellemptysmall.Border = Rectangle.NO_BORDER;
                    cellemptysmall.Colspan = 5;
                    cellemptysmall.HorizontalAlignment = 0;


                    if (kat.Kategorie.Header != null && !kat.Kategorie.Header.Equals(""))
                    {
                        string[] texte = kat.Kategorie.Header.Split('|');
                        foreach (var item in texte)
                        {
                            innerTable.AddCell(" ");
                            PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                            icell.Border = Rectangle.NO_BORDER;
                            icell.Colspan = 1;
                            icell.HorizontalAlignment = 0;
                            innerTable.AddCell(icell);
                            innerTable.AddCell(" ");
                            innerTable.AddCell(cellemptysmall);
                        }
                    }


                    foreach (var item in homeSpeisenModel.AlleSpeisenZuDenKategorien.Where(x => x.Key == kat.Kategorie.id))
                    {
                        foreach (Getränke speise in item.Value)
                        {

                            //Allergene beachten!!!
                            if (homeSpeisenModel.AllergeneZuSpeisen.Where(x => x.Key == speise.id).Any())
                            {
                                string val = homeSpeisenModel.AllergeneZuSpeisen.Where(x => x.Key == speise.id).FirstOrDefault().Value;
                                Paragraph paragraph = new Paragraph();
                                Phrase pr = new Phrase(speise.Bezeichnung, font12);
                                paragraph.Add(pr);
                                Chunk al = new Chunk(val, font6);
                                al.SetTextRise(7);
                                paragraph.Add(al);
                                innerTable.AddCell(paragraph);
                            }
                            else
                            {
                                Phrase p = new Phrase(speise.Bezeichnung, font12);
                                PdfPCell cellBez = new PdfPCell(p);
                                cellBez.Border = Rectangle.NO_BORDER;
                                innerTable.AddCell(cellBez);
                            }
                            innerTable.AddCell(speise.Ergänzung1);
                            innerTable.AddCell(speise.Ergänzung2);

                            innerTable.AddCell("€");

                            Phrase phrasen = new Phrase(speise.Preis.ToString(), font12);
                            PdfPCell cell2n = new PdfPCell(phrasen);
                            cell2n.Border = Rectangle.NO_BORDER;
                            cell2n.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                            innerTable.AddCell(cell2n);

                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                        }
                    }

                    if (kat.Kategorie.Footer != null && !kat.Kategorie.Footer.Equals(""))
                    {
                        string[] texte = kat.Kategorie.Footer.Split('|');
                        foreach (var item in texte)
                        {

                            PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                            icell.Border = Rectangle.NO_BORDER;
                            icell.Colspan = 1;
                            icell.HorizontalAlignment = 0;
                            innerTable.AddCell(icell);
                            innerTable.AddCell(" ");
                            innerTable.AddCell(cellemptysmall);
                        }
                    }
                    //Die Tabelle der Kategorien noch in die Gesamttabelle packen
                    PdfPCell tabcell = new PdfPCell(innerTable);
                    tabcell.Border = Rectangle.NO_BORDER;
                    tabcell.Colspan = 5;
                    tabcell.HorizontalAlignment = 0;
                    table.AddCell(tabcell);
                }

                document.Add(table);

            }
            catch (DocumentException de)
            {
                var test = 0;
            }
            catch (IOException ioe)
            {
                var test = 0;
            }


            // step 5: we close the document
            document.Close();
        }

        public static void MakePdfAllergene(HomeAllergeneModel allergenModel)
        {
            Document document = new Document();

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:\\copy\\_Allergene.pdf", FileMode.Create));

                document.Open();

                PdfPTable table = new PdfPTable(4);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 20f, 120f, 340f, 100f };
                table.TotalWidth = 580f;
                table.LockedWidth = true;
                table.SetWidths(widths);

                string FONT = HttpContext.Current.Request.PhysicalApplicationPath + "\\fonts\\arial.ttf";
                BaseFont bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font45 = new Font(bf, 45, Font.NORMAL);
                Font font20 = new Font(bf, 20, Font.NORMAL);
                Font font12 = new Font(bf, 12, Font.NORMAL);
                Font font10 = new Font(bf, 10, Font.NORMAL);
                Font font10b = new Font(bf, 10, Font.BOLD);
                Font font6 = new Font(bf, 6, Font.NORMAL);

                //PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Speisen, FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 45)));
                PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Allergene, font45));
                cellHeader.Border = Rectangle.NO_BORDER;
                cellHeader.Colspan = 3;
                cellHeader.HorizontalAlignment = 0;
                table.AddCell(cellHeader);


                Image jpg = Image.GetInstance(HttpContext.Current.Request.PhysicalApplicationPath + "/Images/logo.png");
                jpg.ScaleToFit(90f, 90f);
                jpg.SpacingAfter = 12f;
                jpg.SpacingBefore = 12f;
                PdfPCell cellImage = new PdfPCell(jpg);
                cellImage.HorizontalAlignment = 1;
                cellImage.Border = Rectangle.NO_BORDER;
                table.AddCell(cellImage);

                foreach (Allergene allergen in allergenModel.AlleAllergene)
                {

                    PdfPCell cell0 = new PdfPCell(new Phrase(allergen.Nummer, font10));
                    cell0.Border = Rectangle.NO_BORDER;
                    cell0.HorizontalAlignment = 0;
                    table.AddCell(cell0);

                    PdfPCell cell = new PdfPCell(new Phrase(allergen.Bezeichnung, font10b));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = 0;
                    table.AddCell(cell);

                    PdfPCell cell2 = new PdfPCell(new Phrase(allergen.Beschreibung, font10));
                    cell2.Border = Rectangle.NO_BORDER;
                    cell2.HorizontalAlignment = 0;
                    cell2.Colspan = 2;
                    table.AddCell(cell2);
                }

                document.Add(table);

            }
            catch (DocumentException de)
            {
                var test = 0;
            }
            catch (IOException ioe)
            {
                var test = 0;
            }


            // step 5: we close the document
            document.Close();
        }

        public static void MakePdfMittagstisch(HomeMittagstischModel homeSpeisenModel)
        {
            Document document = new Document();

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:\\copy\\_Mittagtisch.pdf", FileMode.Create));

                document.Open();

                PdfPTable table = new PdfPTable(4);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 10f, 100f, 10f, 20f };
                table.TotalWidth = 580f;
                table.LockedWidth = true;
                table.SetWidths(widths);


                string FONT = HttpContext.Current.Request.PhysicalApplicationPath + "\\fonts\\arial.ttf";
                BaseFont bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font45 = new Font(bf, 45, Font.NORMAL);
                Font font20 = new Font(bf, 20, Font.NORMAL);
                Font font12 = new Font(bf, 12, Font.NORMAL);
                Font font10 = new Font(bf, 10, Font.NORMAL);
                Font font6 = new Font(bf, 6, Font.NORMAL);

                //PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Speisen, FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 45)));
                PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Mittagstisch, font45));
                cellHeader.Border = Rectangle.NO_BORDER;
                cellHeader.Colspan = 2;
                cellHeader.HorizontalAlignment = 0;
                table.AddCell(cellHeader);


                Image jpg = Image.GetInstance(HttpContext.Current.Request.PhysicalApplicationPath + "/Images/logo.png");
                jpg.ScaleToFit(90f, 90f);
                jpg.SpacingAfter = 12f;
                jpg.SpacingBefore = 12f;
                PdfPCell cellImage = new PdfPCell(jpg);
                cellImage.Colspan = 2;
                cellImage.Border = Rectangle.NO_BORDER;
                table.AddCell(cellImage);

                foreach (KategorienFuerModel kat in homeSpeisenModel.AlleKategorien)
                {
                    PdfPTable innerTable = new PdfPTable(4);
                    innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    innerTable.TotalWidth = 580f;
                    innerTable.LockedWidth = true;
                    innerTable.SetWidths(widths);


                    PdfPCell cell = new PdfPCell(new Phrase(kat.Kategorie.Bezeichnung, font20));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 4;
                    cell.HorizontalAlignment = 0;
                    innerTable.AddCell(cell);

                    PdfPCell cellempty = new PdfPCell(new Phrase(" ", FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 15)));
                    cellempty.Border = Rectangle.NO_BORDER;
                    cellempty.Colspan = 4;
                    cellempty.HorizontalAlignment = 0;
                    innerTable.AddCell(cellempty);

                    PdfPCell cellemptysmall = new PdfPCell(new Phrase(" ", FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 5)));
                    cellemptysmall.Border = Rectangle.NO_BORDER;
                    cellemptysmall.Colspan = 4;
                    cellemptysmall.HorizontalAlignment = 0;


                    if (kat.Kategorie.Header != null && !kat.Kategorie.Header.Equals(""))
                    {
                        string[] texte = kat.Kategorie.Header.Split('|');
                        foreach (var item in texte)
                        {
                            innerTable.AddCell(" ");
                            PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                            icell.Border = Rectangle.NO_BORDER;
                            icell.Colspan = 2;
                            icell.HorizontalAlignment = 0;
                            innerTable.AddCell(icell);
                            innerTable.AddCell(" ");
                            innerTable.AddCell(cellemptysmall);
                        }
                    }


                    foreach (var item in homeSpeisenModel.AlleSpeisenZuDenKategorien.Where(x => x.Key == kat.Kategorie.id))
                    {
                        foreach (Mittagstisch speise in item.Value)
                        {
                            //Item row 1
                            innerTable.AddCell(" ");

                            //Allergene beachten!!!
                            if (homeSpeisenModel.AllergeneZuSpeisen.Where(x => x.Key == speise.id).Any())
                            {
                                string val = homeSpeisenModel.AllergeneZuSpeisen.Where(x => x.Key == speise.id).FirstOrDefault().Value;
                                Paragraph paragraph = new Paragraph();
                                Phrase pr = new Phrase(speise.Bezeichnung, font12);
                                paragraph.Add(pr);
                                Chunk al = new Chunk(val, font6);
                                al.SetTextRise(7);
                                paragraph.Add(al);
                                innerTable.AddCell(paragraph);
                            }
                            else
                            {
                                Phrase p = new Phrase(speise.Bezeichnung, font12);
                                PdfPCell cellBez = new PdfPCell(p);
                                cellBez.Border = Rectangle.NO_BORDER;
                                innerTable.AddCell(cellBez);
                            }


                            innerTable.AddCell("€");

                            Phrase phrasen = new Phrase(speise.Preis.ToString(), font12);
                            PdfPCell cell2n = new PdfPCell(phrasen);
                            cell2n.Border = Rectangle.NO_BORDER;
                            cell2n.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                            innerTable.AddCell(cell2n);

                            //Item row 2
                            innerTable.AddCell("");
                            Phrase phrase = new Phrase(speise.Beschreibung, font12);
                            PdfPCell cell2 = new PdfPCell(phrase);
                            cell2.Border = Rectangle.NO_BORDER;
                            innerTable.AddCell(cell2);
                            //int hoehe = (int)cell2.Height;
                            innerTable.AddCell("");
                            innerTable.AddCell("");

                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                            innerTable.AddCell(" ");
                        }
                    }

                    if (kat.Kategorie.Footer != null && !kat.Kategorie.Footer.Equals(""))
                    {
                        string[] texte = kat.Kategorie.Footer.Split('|');
                        foreach (var item in texte)
                        {
                            innerTable.AddCell(" ");
                            PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                            icell.Border = Rectangle.NO_BORDER;
                            icell.Colspan = 2;
                            icell.HorizontalAlignment = 0;
                            innerTable.AddCell(icell);
                            innerTable.AddCell(" ");
                            innerTable.AddCell(cellemptysmall);
                        }
                    }
                    //Die Tabelle der Kategorien noch in die Gesamttabelle packen
                    PdfPCell tabcell = new PdfPCell(innerTable);
                    tabcell.Border = Rectangle.NO_BORDER;
                    tabcell.Colspan = 4;
                    tabcell.HorizontalAlignment = 0;
                    table.AddCell(tabcell);
                }

                document.Add(table);

            }
            catch (DocumentException de)
            {
                var test = 0;
            }
            catch (IOException ioe)
            {
                var test = 0;
            }


            // step 5: we close the document
            document.Close();
        }

        private static List<Tuple<string, string, string, string>> GetItemsPerKategorieSpeisen(HttpRequestBase Request, int kategorie) {
            List<Tuple<string, string, string, string>> liste = new List<Tuple<string, string, string, string>>();
            bool checker = true;
            int counterItem = 0;
            while (checker)
            {
                var txt = "itemsSpeisen_" + kategorie + "_" + counterItem;
                var anzahl = Request[txt];

                if (anzahl != null)
                {
                    if (!anzahl.Equals("0") && !anzahl.Equals(""))
                    {
                        //Kategorie zur Liste hinzufügen
                        liste.Add(new Tuple<string,string, string, string>(anzahl, Request["itemsBezeichnung_" + kategorie + "_" + counterItem], Request["itemsEinheit_" + kategorie + "_" + counterItem], Request["itemsPreis_" + kategorie + "_" + counterItem]));
                        //Die Liste der Items zu der Kategorie schreiben
                    }
                }
                else
                {
                    checker = false;
                }
                counterItem++;
            }


            return liste;
        }

        private static List<Tuple<string, string, string, string>> GetItemsPerKategorieGetränke(HttpRequestBase Request, int kategorie)
        {
            List<Tuple<string, string, string, string>> liste = new List<Tuple<string, string, string, string>>();
            bool checker = true;
            int counterItem = 0;
            while (checker)
            {
                var txt = "itemsGetränke_" + kategorie + "_" + counterItem;
                var anzahl = Request[txt];

                if (anzahl != null)
                {
                    if (!anzahl.Equals("0") && !anzahl.Equals(""))
                    {
                        //Kategorie zur Liste hinzufügen
                        liste.Add(new Tuple<string, string, string, string>(anzahl, Request["itemsGetränkeBezeichnung_" + kategorie + "_" + counterItem], Request["itemsGetränkeEinheit" + kategorie + "_" + counterItem], Request["itemsPreisG_" + kategorie + "_" + counterItem]));
                        //Die Liste der Items zu der Kategorie schreiben
                    }
                }
                else
                {
                    checker = false;
                }
                counterItem++;
            }


            return liste;
        }

        public static void MakePdfPlaner(HttpRequestBase Request)
        {
            PdfPlanerModel model = MakePdfPlanerData(Request);
        }



        private static PdfPlanerModel MakePdfPlanerData(HttpRequestBase Request) {
            var dsvgoOk = Request["dsgvoOk"];
            var dsvgoName = Request["dsgvoName"];
            var dsvgoDatum = Request["dsgvoDatum"];
            var dsvgoMail = Request["dsgvoMail"];
            var dsvgoTelefon = Request["dsgvoTelefon"];

            var anzahlPersonenInsgesamt = Request["anzahlPersonenInsgesamt"];

            var SummarySummeSpeisen = Request["summarySummeSpeisenValue"];
            var SummarySummeGetränke = Request["summarySummeGetränkeValue"];
            var SummarySummeGesamt = Request["summarySummeGesamtValue"];

            List<Tuple<string, string, string>> KategorienSpeisen = new List<Tuple<string, string, string>>();
            List<List<Tuple<string, string, string, string>>> ItemsSpeisen = new List<List<Tuple<string, string, string, string>>>();
            List<Tuple<string, string, string>> KategorienGetränke = new List<Tuple<string, string, string>>();
            List<List<Tuple<string, string, string, string>>> ItemsGetränke = new List<List<Tuple<string, string, string, string>>>();

            //SPEISEN
            bool checker = true;
            int counterKategorie = 0;
            while (checker) {
                var txt = "_anzahlKategorie" + counterKategorie;
                var text = Request[txt];

                if (text != null)
                {
                    if (!text.Equals("0"))
                    {
                        //Kategorie zur Liste hinzufügen
                        KategorienSpeisen.Add(new Tuple<string, string, string>(text, Request["_bezeichnungKategorie" + counterKategorie], Request["_summeKategorie" + counterKategorie]));
                        //Die Liste der Items zu der Kategorie schreiben
                        ItemsSpeisen.Add(GetItemsPerKategorieSpeisen(Request, counterKategorie));
                    }
                }
                else {
                    checker = false;
                }
                counterKategorie++;
            }

            //GETRÄNKE
            checker = true;
            counterKategorie = 0;
            while (checker)
            {
                var txt2 = "_anzahlKategorieGetränk" + counterKategorie;
                var text2 = Request[txt2];

                if (text2 != null)
                {
                    //if (!text2.Equals("0"))
                    //{
                        //Kategorie zur Liste hinzufügen
                        KategorienGetränke.Add(new Tuple<string, string, string>(text2, Request["_bezeichnungKategorieGetränk" + counterKategorie], Request["_summeKategorieGetränk" + counterKategorie]));
                        //Die Liste der Items zu der Kategorie schreiben
                        ItemsGetränke.Add(GetItemsPerKategorieGetränke(Request, counterKategorie));
                    //}
                }
                else
                {
                    checker = false;
                }
                counterKategorie++;
            }
            PdfPlanerModel model = new PdfPlanerModel();
            model.DsvgoOk = dsvgoOk;
            model.DsvgoName = dsvgoName;
            model.DsvgoDatum = dsvgoDatum;
            model.DsvgoMail = dsvgoMail;
            model.DsvgoTelefon = dsvgoTelefon;

            model.AnzahlPersonenInsgesamt = anzahlPersonenInsgesamt;

            model.SummarySummeSpeisen = SummarySummeSpeisen;
            model.SummarySummeGetränke = SummarySummeGetränke;
            model.SummarySummeGesamt = SummarySummeGesamt;

            model.KategorienSpeisen = KategorienSpeisen;
            model.ItemsSpeisen = ItemsSpeisen;
            model.KategorienGetränke = KategorienGetränke;
            model.ItemsGetränke = ItemsGetränke;
            return model;
        }
    }
}