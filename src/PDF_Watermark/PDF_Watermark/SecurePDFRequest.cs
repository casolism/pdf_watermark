using System.Drawing;

namespace PDF_Watermark
{
    public class SecurePDFRequest
    {
        public string InputPDFBase64 { get; set; }
        public string Password { get; set; }
        public bool ServersidePassword { get; set; }
    }

}