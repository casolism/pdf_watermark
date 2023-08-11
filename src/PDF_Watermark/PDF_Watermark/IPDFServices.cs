using iTextSharp.text;

namespace PDF_Watermark
{
    public interface IPDFServices
    {
        byte[] MarcaDeAgua(byte[] pdfIn, Font font, string Text, float x, float y, float rotation);
    }
}
