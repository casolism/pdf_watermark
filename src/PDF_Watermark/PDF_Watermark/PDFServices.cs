using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDF_Watermark
{
    public class PDFServices : IPDFServices
    {
        public byte[] MarcaDeAgua(byte[] pdfIn, Font font, string Text, float x, float y,float rotation)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(pdfIn);
                using (PdfStamper stamper = new(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(Text, font), x, y, rotation);
                }
                return stream.ToArray();
            }
        }
    }
}
