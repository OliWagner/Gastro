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
            // step 1: creation of a document-object
            Document document = new Document();


            try
            {


                // step 2:
                // we create a writer that listens to the document
                // and directs a PDF-stream to a file
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:\\copy\\Chap1002.pdf", FileMode.Create));
                // step 3: we open the document
                document.Open();

                // step 4: we grab the ContentByte and do some stuff with it
                //PdfContentByte cb = writer.DirectContent;
                //// we tell the ContentByte we're ready to draw text
                //cb.BeginText();


                //BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\Verdana.TTF", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                //cb.SetFontAndSize(f_cn, 12);

                //int y = 760;
                //for (int i = 0; i < 10; i++)
                //{
                //    //ELEMENTE SPEISE
                //    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "12", 50, y, 0);
                //    cb.SetLineWidth(0.5f);
                //    cb.SetRGBColorFill(0, 0, 255);
                //    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                //    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Schweinefilet Beuf Irschendjet", 70, y, 0);
                //    cb.SetLineWidth(0);
                //    cb.SetTextRenderingMode(PdfContentByte.ALIGN_LEFT);
                //    string txt = "Beschreibung zu Schweinefilet Beuf Irschendjet, hämmern wir mal noch blödsinnigen Text dran, dann wird das Feld auch schön lang";
                //    Phrase phrase = new Phrase(txt, FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 12));
                //    cb.SetRGBColorFill(0, 0, 0);
                //    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "€", 465, y, 0);
                //    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "22,50", 520, y, 0);
                //    ColumnText ct = new ColumnText(cb);
                //    ct.SetSimpleColumn(phrase, 70, y - 5, 500, 50, 13, Element.ALIGN_LEFT);
                //    ct.Go();
                //    y = y - 70;
                //}
                //cb.EndText();

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
                    PdfPCell cell = new PdfPCell(new Phrase(kat.Kategorie.Bezeichnung, font20));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 4;
                    cell.HorizontalAlignment = 0;
                    table.AddCell(cell);

                    PdfPCell cellempty = new PdfPCell(new Phrase(" ", FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 15)));
                    cellempty.Border = Rectangle.NO_BORDER;
                    cellempty.Colspan = 4;
                    cellempty.HorizontalAlignment = 0;
                    table.AddCell(cellempty);

                    PdfPCell cellemptysmall = new PdfPCell(new Phrase(" ", FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 5)));
                    cellemptysmall.Border = Rectangle.NO_BORDER;
                    cellemptysmall.Colspan = 4;
                    cellemptysmall.HorizontalAlignment = 0;
                    

                    if (kat.Kategorie.Header != null && !kat.Kategorie.Header.Equals(""))
                    {
                        string[] texte = kat.Kategorie.Header.Split('|');
                        foreach (var item in texte)
                        {
                            table.AddCell(" ");
                            PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                            icell.Border = Rectangle.NO_BORDER;
                            icell.Colspan = 2;
                            icell.HorizontalAlignment = 0;
                            table.AddCell(icell);
                            table.AddCell(" ");
                            table.AddCell(cellemptysmall);
                        }
                    }
           

                foreach(var item in homeSpeisenModel.AlleSpeisenZuDenKategorien.Where(x => x.Key == kat.Kategorie.id))
                {
                    foreach (Speisen speise in item.Value)
                    {
                        //Item row 1
                        table.AddCell(speise.Kartennummer);

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
                                table.AddCell(paragraph);
                            }
                            else {
                                Phrase p = new Phrase(speise.Bezeichnung, font12);
                                PdfPCell cellBez = new PdfPCell(p);
                                cellBez.Border = Rectangle.NO_BORDER;
                                table.AddCell(cellBez);
                            }
                        

                        table.AddCell("€");
                        table.AddCell(speise.Preis.ToString());
                        //Item row 2
                        table.AddCell("");
                        Phrase phrase = new Phrase(speise.Beschreibung, font12);
                        PdfPCell cell2 = new PdfPCell(phrase);
                        cell2.Border = Rectangle.NO_BORDER;
                        table.AddCell(cell2);
                        //int hoehe = (int)cell2.Height;
                        table.AddCell("");
                        table.AddCell("");

                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                    }
                }
           
                if(kat.Kategorie.Footer != null && !kat.Kategorie.Footer.Equals(""))
                {
                    string[] texte = kat.Kategorie.Footer.Split('|');
                    foreach (var item in texte)
                    {
                        table.AddCell(" ");
                        PdfPCell icell = new PdfPCell(new Phrase(item, font10));
                        icell.Border = Rectangle.NO_BORDER;
                        icell.Colspan = 2;
                        icell.HorizontalAlignment = 0;   
                        table.AddCell(icell);
                        table.AddCell(" ");
                        table.AddCell(cellemptysmall);
                    }
                }

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
}
}