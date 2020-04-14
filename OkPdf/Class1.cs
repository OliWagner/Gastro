
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;


namespace OkPdf
{
    public class PdfHelloWorld
    {
        public PdfHelloWorld(HomeSpeisenModel homeSpeisenModel) {
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
                
                PdfPCell cell = new PdfPCell(new Phrase("Speisen" , FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 20)));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                cell.HorizontalAlignment = 0;
                table.AddCell(cell);
                

                //Item row 1
                table.AddCell("12");
                table.AddCell("Schweinefilet Beuf");
                table.AddCell("€");
                table.AddCell("23,80");
                //Item row 2
                table.AddCell("");
                string txt = "Beschreibung zu Schweinefilet Beuf Irschendjet, hämmern wir mal noch blödsinnigen Text dran, dann wird das Feld auch schön lang";
                Phrase phrase = new Phrase(txt, FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 12));
                PdfPCell cell2 = new PdfPCell(phrase);
                table.AddCell(cell2);
                //int hoehe = (int)cell2.Height;
                table.AddCell("");
                table.AddCell("");
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
