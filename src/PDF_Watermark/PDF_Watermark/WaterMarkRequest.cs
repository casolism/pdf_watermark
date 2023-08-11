using System.Drawing;

namespace PDF_Watermark
{
    public class WaterMarkRequest
    {
        public string TextoMarcaAgua { get; set; }
        public Fuente Font { get; set; }
        public string InputPDFBase64 { get; set; }
        public Point Posicion { get; set; }
        public float Rotation { get; set; }
    }
    public class Fuente
    {
        public string NombreFuente { get; set; }
        public float Size { get; set; }
        /// <summary>
        /// BOLD = 1;
        /// BOLDITALIC = BOLD | ITALIC;
        /// int ITALIC = 2;
        /// int NORMAL = 0;
        /// </summary>
        public int Style { get; set; }
        public string Color { get; set; }
    }
}