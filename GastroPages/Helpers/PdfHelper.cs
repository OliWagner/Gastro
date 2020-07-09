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
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Speisen.pdf", FileMode.Create));

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
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Getränke.pdf", FileMode.Create));

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
                            innerTable.AddCell(cellemptysmall);
                            PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                            icell.Border = Rectangle.NO_BORDER;
                            icell.Colspan = 3;
                            icell.HorizontalAlignment = 0;
                            icell.PaddingLeft = 10;
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
                            icell.Colspan = 3;
                            icell.HorizontalAlignment = 0;
                            icell.PaddingLeft = 10;
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
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Allergene.pdf", FileMode.Create));

                document.Open();

                PdfPTable table = new PdfPTable(5);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 20f, 20f, 120f, 290f, 100f };
                table.TotalWidth = 550f;
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

                PdfPCell cellEmptyLinks = new PdfPCell(new Phrase(" ", font10));
                cellEmptyLinks.Border = Rectangle.NO_BORDER;
                //PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Speisen, FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 45)));
                PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Allergene, font45));
                cellHeader.Border = Rectangle.NO_BORDER;
                cellHeader.Colspan = 3;
                cellHeader.HorizontalAlignment = 0;
                table.AddCell(cellEmptyLinks);
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
                    table.AddCell(cellEmptyLinks);
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
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Mittagtisch.pdf", FileMode.Create));

                document.Open();

                PdfPTable table = new PdfPTable(5);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 20f, 210f, 110f, 100f, 110f };
                table.TotalWidth = 550f;
                table.LockedWidth = true;
                table.SetWidths(widths);


                string FONT = HttpContext.Current.Request.PhysicalApplicationPath + "\\fonts\\arial.ttf";
                BaseFont bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font45 = new Font(bf, 45, Font.NORMAL);
                Font font20 = new Font(bf, 20, Font.NORMAL);
                Font font12 = new Font(bf, 12, Font.NORMAL);
                Font font10 = new Font(bf, 10, Font.NORMAL);
                Font font6 = new Font(bf, 6, Font.NORMAL);

                PdfPCell cellEmptyLinks = new PdfPCell(new Phrase(" ", font10));
                cellEmptyLinks.Border = Rectangle.NO_BORDER;
                //PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Speisen, FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 45)));
                PdfPCell cellHeader = new PdfPCell(new Phrase("Aktuelles Angebot", font45));
                cellHeader.Border = Rectangle.NO_BORDER;
                cellHeader.Colspan = 3;
                cellHeader.HorizontalAlignment = 0;
                table.AddCell(cellEmptyLinks);
                table.AddCell(cellHeader);


                Image jpg = Image.GetInstance(HttpContext.Current.Request.PhysicalApplicationPath + "/Images/logo.png");
                jpg.ScaleToFit(90f, 90f);
                jpg.SpacingAfter = 12f;
                jpg.SpacingBefore = 12f;
                PdfPCell cellImage = new PdfPCell(jpg);
                //cellImage.Colspan = 2;
                cellImage.Border = Rectangle.NO_BORDER;
                table.AddCell(cellImage);

                foreach (KategorienFuerModel kat in homeSpeisenModel.AlleKategorien)
                {
                    PdfPTable innerTable = new PdfPTable(4);
                    innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    float[] widthsInner = new float[] { 300, 130f, 20f, 70f };
                    innerTable.TotalWidth = 520f;
                    innerTable.LockedWidth = true;
                    innerTable.SetWidths(widthsInner);


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
                            
                            PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                            icell.Border = Rectangle.NO_BORDER;
                            icell.Colspan = 3;
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
                            innerTable.AddCell(" ");

                            innerTable.AddCell("€");

                            Phrase phrasen = new Phrase(speise.Preis.ToString(), font12);
                            PdfPCell cell2n = new PdfPCell(phrasen);
                            cell2n.Border = Rectangle.NO_BORDER;
                            cell2n.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                            innerTable.AddCell(cell2n);

                            //Item row 2
                            
                            Phrase phrase = new Phrase(speise.Beschreibung, font12);
                            PdfPCell cell2 = new PdfPCell(phrase);
                            cell2.Colspan = 2;
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
                    table.AddCell(cellEmptyLinks);
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

        
        public static void MakePdfPlaner(HttpRequestBase Request)
        {
            string txt = Guid.NewGuid().ToString().Replace("-", "");
            PdfPlanerModel model = MakePdfPlanerData(Request);
            MakePdfPlanerDocument(model);
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

        private static void MakePdfPlanerDocument(PdfPlanerModel model)
        {
            Document document = new Document();

            try
            {
                string txt = Guid.NewGuid().ToString().Replace("-", "");

                PdfWriter writer;
                if (model.DsvgoOk != null)
                {
                    writer = PdfWriter.GetInstance(document, new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Planer_" + txt + ".pdf", FileMode.Create));
                    HttpContext.Current.Session["pdfguid"] = txt;
                }
                else
                {
                    writer = PdfWriter.GetInstance(document, new FileStream(HttpRuntime.AppDomainAppPath + "Content\\Pdfs\\_Planer.pdf", FileMode.Create));
                }



                document.Open();

                PdfPTable table = new PdfPTable(6);
                table.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widths = new float[] { 20f, 30f, 250f, 75f, 60f, 90f };
                table.TotalWidth = 525f;
                table.LockedWidth = true;
                table.SetWidths(widths);

                string FONT = HttpContext.Current.Request.PhysicalApplicationPath + "\\fonts\\arial.ttf";
                BaseFont bf = BaseFont.CreateFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font45 = new Font(bf, 45, Font.NORMAL);
                Font font30 = new Font(bf, 30, Font.BOLD);
                Font font20 = new Font(bf, 20, Font.NORMAL);
                Font font20b = new Font(bf, 20, Font.BOLD);
                Font font10 = new Font(bf, 10, Font.NORMAL);
                Font font15 = new Font(bf, 15, Font.NORMAL);
                Font font15b = new Font(bf, 15, Font.BOLD);
                Font font10b = new Font(bf, 10, Font.BOLD);
                Font font6 = new Font(bf, 6, Font.NORMAL);

                PdfPCell cellEmptyLinks = new PdfPCell(new Phrase(" ", font10));
                cellEmptyLinks.Border = Rectangle.NO_BORDER;
                //HEADER
                table.AddCell(cellEmptyLinks);
                PdfPCell cellHeader = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.PlanderPdfHeader, font45));
                cellHeader.Border = Rectangle.NO_BORDER;
                cellHeader.Colspan = 4;
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

                //Wenn Anfrage, die Daten auf das Pdf drucken --> Innere Tabelle
                if (model.DsvgoOk != null)
                {
                    PdfPTable t = new PdfPTable(4);
                    t.DefaultCell.Border = Rectangle.NO_BORDER;
                    float[] widths_t = new float[] { 90f, 180f, 90f, 165f };
                    t.TotalWidth = 525f;
                    t.LockedWidth = true;
                    t.SetWidths(widths_t);

                    //Zeile 1
                    PdfPCell cellInner1 = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.PlanerPdfName + ":", font10b));
                    cellInner1.Border = Rectangle.NO_BORDER;
                    cellInner1.Colspan = 1;
                    cellInner1.HorizontalAlignment = 0;
                    t.AddCell(cellInner1);

                    PdfPCell cellInner2 = new PdfPCell(new Phrase(model.DsvgoName + " (" + DateTime.Now.ToLocalTime().ToShortDateString() + ", " + DateTime.Now.ToLocalTime().ToShortTimeString() + ")", font10b));
                    cellInner2.Border = Rectangle.NO_BORDER;
                    cellInner2.Colspan = 1;
                    cellInner2.HorizontalAlignment = 0;
                    t.AddCell(cellInner2);

                    PdfPCell cellInner3 = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.PlanerPdfTermin + ":", font10b));
                    cellInner3.Border = Rectangle.NO_BORDER;
                    cellInner3.Colspan = 1;
                    cellInner3.HorizontalAlignment = 0;
                    t.AddCell(cellInner3);

                    PdfPCell cellInner4 = new PdfPCell(new Phrase(model.DsvgoDatum + " (" + model.AnzahlPersonenInsgesamt + " " + ResourcesGastro.Shared.Navi.Personen + ")", font10b));
                    cellInner4.Border = Rectangle.NO_BORDER;
                    cellInner4.Colspan = 1;
                    cellInner4.HorizontalAlignment = 0;
                    t.AddCell(cellInner4);

                    //Zeile 2
                    PdfPCell cellInner5 = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.PlanerPdfTelefon + ":", font10b));
                    cellInner5.Border = Rectangle.NO_BORDER;
                    cellInner5.Colspan = 1;
                    cellInner5.HorizontalAlignment = 0;
                    t.AddCell(cellInner5);

                    PdfPCell cellInner6 = new PdfPCell(new Phrase(model.DsvgoTelefon, font10b));
                    cellInner6.Border = Rectangle.NO_BORDER;
                    cellInner6.Colspan = 1;
                    cellInner6.HorizontalAlignment = 0;
                    t.AddCell(cellInner6);

                    PdfPCell cellInner7 = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Email + ":", font10b));
                    cellInner7.Border = Rectangle.NO_BORDER;
                    cellInner7.Colspan = 1;
                    cellInner7.HorizontalAlignment = 0;
                    t.AddCell(cellInner7);

                    PdfPCell cellInner8 = new PdfPCell(new Phrase(model.DsvgoMail, font10b));
                    cellInner8.Border = Rectangle.NO_BORDER;
                    cellInner8.Colspan = 1;
                    cellInner8.HorizontalAlignment = 0;
                    t.AddCell(cellInner8);

                    PdfPCell cellInner9 = new PdfPCell(new Phrase(" ", font10b));
                    cellInner9.Border = Rectangle.NO_BORDER;
                    cellInner9.Colspan = 4;
                    cellInner9.HorizontalAlignment = 0;
                    t.AddCell(cellInner9);

                    PdfPCell cellInner10 = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.PdfAnsprache, font10b));
                    cellInner10.Border = Rectangle.NO_BORDER;
                    cellInner10.Colspan = 4;
                    cellInner10.HorizontalAlignment = 0;
                    t.AddCell(cellInner10);



                    //Nun neue Tabelle in eine Zelle einfügen
                    PdfPCell ctz = new PdfPCell(t);
                    ctz.Border = Rectangle.NO_BORDER;
                    ctz.Colspan = 5;
                    ctz.HorizontalAlignment = 0;
                    table.AddCell(cellEmptyLinks);
                    table.AddCell(ctz);
                }

                PdfPCell cellEmptySmall = new PdfPCell(new Phrase(" ", font10));
                cellEmptySmall.Border = Rectangle.NO_BORDER;
                cellEmptySmall.Colspan = 6;
                cellEmptySmall.HorizontalAlignment = 0;

                PdfPCell cellEmpty = new PdfPCell(new Phrase(" ", font20));
                cellEmpty.Border = Rectangle.NO_BORDER;
                cellEmpty.Colspan = 6;
                cellEmpty.HorizontalAlignment = 0;
                table.AddCell(cellEmpty);

                PdfPCell cellHeaderSPeisen = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Speisen, font30));
                cellHeaderSPeisen.Border = Rectangle.NO_BORDER;
                cellHeaderSPeisen.Colspan = 5;
                cellHeaderSPeisen.HorizontalAlignment = 0;
                table.AddCell(cellEmptyLinks);
                table.AddCell(cellHeaderSPeisen);
                table.AddCell(cellEmptySmall);

                //Ab hier die Speisen
                int counterSpeisen = 0;
                foreach (var item in model.KategorienSpeisen)
                {
                    if (item.Item1.Equals("0"))
                    {
                        //PdfPCell cell = new PdfPCell(new Phrase(item.Item2, font20b));
                        //cell.Border = Rectangle.NO_BORDER;
                        //cell.Colspan = 4;
                        //cell.HorizontalAlignment = 0;
                        //table.AddCell(cell);
                    }
                    else
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(item.Item2, font20));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.Colspan = 5;
                        cell.HorizontalAlignment = 0;
                        table.AddCell(cellEmptyLinks);
                        table.AddCell(cell);

                        List<Tuple<string, string, string, string>> list = model.ItemsSpeisen.ElementAt(counterSpeisen);
                        foreach (Tuple<string, string, string, string> elem in list)
                        {
                            table.AddCell(cellEmptyLinks);

                            PdfPCell c1 = new PdfPCell(new Phrase(elem.Item1.TrimStart(new char[] { '0' }), font10));
                            c1.Border = Rectangle.NO_BORDER;
                            c1.Colspan = 1;
                            c1.HorizontalAlignment = 0;
                            table.AddCell(c1);

                            PdfPCell c2 = new PdfPCell(new Phrase(elem.Item2, font10b));
                            c2.Border = Rectangle.NO_BORDER;
                            c2.Colspan = 1;
                            c2.HorizontalAlignment = 0;
                            table.AddCell(c2);

                            PdfPCell c3 = new PdfPCell(new Phrase(elem.Item3, font10));
                            c3.Border = Rectangle.NO_BORDER;
                            c3.Colspan = 1;
                            c3.HorizontalAlignment = 2;
                            table.AddCell(c3);

                            PdfPCell c4 = new PdfPCell(new Phrase(elem.Item4, font10));
                            c4.Border = Rectangle.NO_BORDER;
                            c4.Colspan = 1;
                            c4.HorizontalAlignment = 2;
                            table.AddCell(c4);

                            int anz = int.Parse(elem.Item1);
                            decimal preis = decimal.Parse(elem.Item4);
                            PdfPCell c5 = new PdfPCell(new Phrase((anz * preis).ToString(), font10b));
                            c5.Border = Rectangle.NO_BORDER;
                            c5.Colspan = 1;
                            c5.HorizontalAlignment = 2;
                            table.AddCell(c5);
                        }
                        table.AddCell(cellEmptySmall);
                    }
                    counterSpeisen++;
                }

                table.AddCell(cellEmpty);

                PdfPCell cellHeaderGetränke = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Getränke, font30));
                cellHeaderGetränke.Border = Rectangle.NO_BORDER;
                cellHeaderGetränke.Colspan = 5;
                cellHeaderGetränke.HorizontalAlignment = 0;
                table.AddCell(cellEmptyLinks);
                table.AddCell(cellHeaderGetränke);
                table.AddCell(cellEmptySmall);

                //Ab hier die Getränke
                int counterGetränke = 0;
                foreach (var item in model.KategorienGetränke)
                {
                    if (item.Item1.Equals("0"))
                    {
                        //PdfPCell cell = new PdfPCell(new Phrase(item.Item2, font20b));
                        //cell.Border = Rectangle.NO_BORDER;
                        //cell.Colspan = 4;
                        //cell.HorizontalAlignment = 0;
                        //table.AddCell(cell);
                    }
                    else
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(item.Item2, font20));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.Colspan = 5;
                        cell.HorizontalAlignment = 0;
                        table.AddCell(cellEmptyLinks);
                        table.AddCell(cell);

                        List<Tuple<string, string, string, string>> list = model.ItemsGetränke.ElementAt(counterGetränke);
                        foreach (Tuple<string, string, string, string> elem in list)
                        {
                            table.AddCell(cellEmptyLinks);
                            PdfPCell c1 = new PdfPCell(new Phrase(elem.Item1.TrimStart(new char[] { '0' }), font10));
                            c1.Border = Rectangle.NO_BORDER;
                            c1.Colspan = 1;
                            c1.HorizontalAlignment = 0;
                            table.AddCell(c1);

                            PdfPCell c2 = new PdfPCell(new Phrase(elem.Item2, font10b));
                            c2.Border = Rectangle.NO_BORDER;
                            c2.Colspan = 1;
                            c2.HorizontalAlignment = 0;
                            table.AddCell(c2);

                            PdfPCell c3 = new PdfPCell(new Phrase(elem.Item3, font10));
                            c3.Border = Rectangle.NO_BORDER;
                            c3.Colspan = 1;
                            c3.HorizontalAlignment = 2;
                            table.AddCell(c3);

                            PdfPCell c4 = new PdfPCell(new Phrase(elem.Item4, font10));
                            c4.Border = Rectangle.NO_BORDER;
                            c4.Colspan = 1;
                            c4.HorizontalAlignment = 2;
                            table.AddCell(c4);

                            int anz = int.Parse(elem.Item1);
                            decimal preis = decimal.Parse(elem.Item4);
                            PdfPCell c5 = new PdfPCell(new Phrase((anz * preis).ToString(), font10b));
                            c5.Border = Rectangle.NO_BORDER;
                            c5.Colspan = 1;
                            c5.HorizontalAlignment = 2;
                            table.AddCell(c5);
                        }
                        table.AddCell(cellEmptySmall);
                    }
                    counterGetränke++;
                }

                table.AddCell(cellEmpty);

                PdfPCell cellHeaderZusammenfassung = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Zusammenfassung, font30));
                cellHeaderZusammenfassung.Border = Rectangle.NO_BORDER;
                cellHeaderZusammenfassung.Colspan = 5;
                cellHeaderZusammenfassung.HorizontalAlignment = 0;
                table.AddCell(cellEmptyLinks);
                table.AddCell(cellHeaderZusammenfassung);
                table.AddCell(cellEmptySmall);

                //Ab hier die Zusammenfassung
                PdfPTable tz = new PdfPTable(5);
                tz.DefaultCell.Border = Rectangle.NO_BORDER;
                float[] widthstz = new float[] { 100f, 230f, 80f, 20f, 75f };
                tz.TotalWidth = 505f;
                tz.LockedWidth = true;
                tz.SetWidths(widthstz);

                PdfPCell cellEuro = new PdfPCell(new Phrase("€", font15));
                cellEuro.HorizontalAlignment = 2;
                cellEuro.Border = Rectangle.NO_BORDER;

                //Zeile1
                tz.AddCell(" ");
                PdfPCell tzcell1 = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Planersumme + " " + ResourcesGastro.Shared.Navi.Speisen, font15));
                tzcell1.Border = Rectangle.NO_BORDER;
                tzcell1.HorizontalAlignment = 0;
                tz.AddCell(tzcell1);
                tz.AddCell(" ");
                tz.AddCell(cellEuro);
                PdfPCell tzcell2 = new PdfPCell(new Phrase(model.SummarySummeSpeisen, font15));
                tzcell2.Border = Rectangle.NO_BORDER;
                tzcell2.HorizontalAlignment = 2;
                tz.AddCell(tzcell2);


                //Zeile2
                tz.AddCell(" ");
                PdfPCell tzcell3 = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Planersumme + " " + ResourcesGastro.Shared.Navi.Getränke, font15));
                tzcell3.Border = Rectangle.NO_BORDER;
                tzcell3.HorizontalAlignment = 0;
                tz.AddCell(tzcell3);
                tz.AddCell(" ");
                tz.AddCell(cellEuro);
                PdfPCell tzcell4 = new PdfPCell(new Phrase(model.SummarySummeGetränke, font15));
                tzcell4.Border = Rectangle.NO_BORDER;
                tzcell4.HorizontalAlignment = 2;
                tz.AddCell(tzcell4);


                //Zeile2
                tz.AddCell(" ");
                PdfPCell tzcell5 = new PdfPCell(new Phrase(ResourcesGastro.Shared.Navi.Planersumme + " " + ResourcesGastro.Shared.Navi.Insgesamt, font15b));
                tzcell5.Border = Rectangle.NO_BORDER;
                tzcell5.HorizontalAlignment = 0;
                tz.AddCell(tzcell5);
                tz.AddCell(" ");
                tz.AddCell(cellEuro);
                PdfPCell tzcell6 = new PdfPCell(new Phrase(model.SummarySummeGesamt, font15b));
                tzcell6.Border = Rectangle.NO_BORDER;
                tzcell6.HorizontalAlignment = 2;
                tz.AddCell(tzcell6);



                //Tabelle in die andere einfügen
                PdfPCell c = new PdfPCell(tz);
                c.Border = Rectangle.NO_BORDER;
                c.Colspan = 5;
                c.HorizontalAlignment = 0;
                table.AddCell(cellEmptyLinks);
                table.AddCell(c);
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
    }
}
