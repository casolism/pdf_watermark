using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.util;

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
                        ColumnText.ShowTextAligned(stamper.GetOverContent(i), Element.ALIGN_RIGHT, new Phrase(Text, font), x, y, rotation);
                }
                return stream.ToArray();
            }
        }

        public byte[] SecurePDF(byte[] pdfIn,string Password)
        {
            MemoryStream securedPdfStream = new MemoryStream();
            PdfReader pdfReader = new PdfReader(pdfIn);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, securedPdfStream);
            UnicodeEncoding encoding = new UnicodeEncoding();
            string ownerPassword = Password;
            int permissions = PdfWriter.ENCRYPTION_AES_128;
            pdfStamper.SetEncryption(
                null,                // User password
                ownerPassword.GetIsoBytes(),               // Owner password
                permissions,                 // Permissions
                PdfWriter.DO_NOT_ENCRYPT_METADATA  // Disable metadata encryption
            );
            pdfStamper.Close();
            return securedPdfStream.ToArray();
        }
    }
}
