using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PDF_Watermark.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        IConfiguration configuration;
        public PDFController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // POST <PDFController>
        [HttpPost]
        [Route("Watermark")]
            public IActionResult Post([FromBody] WaterMarkRequest request) {
                IPDFServices PDF_Service = new PDFServices();
                BaseColor color;
                switch (request.Font.Color) {
                    case "Black": color = BaseColor.Black; break;
                    case "Blue": color = BaseColor.Blue; break;
                    case "DarkGray": color = BaseColor.DarkGray; break;
                    case "Gray": color = BaseColor.Gray; break;
                    case "Green": color = BaseColor.Green; break;
                    case "LightGray": color = BaseColor.LightGray; break;
                    case "Magenta": color = BaseColor.Magenta; break;
                    case "Orange": color = BaseColor.Orange; break;
                    case "Pink": color = BaseColor.Pink; break;
                    case "Red": color = BaseColor.Red; break;
                    case "White": color = BaseColor.White; break;
                    case "Yellow": color = BaseColor.Yellow; break;
                    default: color = BaseColor.Gray; break;
                }
                var responseFile = PDF_Service.MarcaDeAgua(Convert.FromBase64String(request.InputPDFBase64), FontFactory.GetFont(request.Font.NombreFuente, request.Font.Size,
                    request.Font.Style, color),request.TextoMarcaAgua,request.Posicion.X, request.Posicion.Y,request.Rotation);
                return Ok(Convert.ToBase64String(responseFile));
            }

        [HttpPost]
        [Route("WatermarkAndSecure")]
        public IActionResult AndSecure([FromBody] WaterMarkAndSecureRequest request)
        {
            string Password = request.ServersidePassword ? this.configuration["PasswordForPDF"] : request.Password;
            IActionResult watermark = Post(request);
            if (watermark is OkObjectResult okResult && okResult.Value is string base64File)
            {
                SecurePDFRequest secRequest = new SecurePDFRequest() { InputPDFBase64 = base64File, Password = Password };
                return SecurePDF(secRequest);
            }
            else
                return BadRequest();
        }

        // POST <PDFController>
        [HttpPost]
        [Route("SecurePDF")]
        public IActionResult SecurePDF([FromBody] SecurePDFRequest request) {
            string Password = request.ServersidePassword?this.configuration["PasswordForPDF"]:request.Password;
            IPDFServices PDF_Service = new PDFServices();
            var responseFile = PDF_Service.SecurePDF(Convert.FromBase64String(request.InputPDFBase64),Password);
            return Ok(Convert.ToBase64String(responseFile));
        }
    }
}
