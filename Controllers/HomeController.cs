using Microsoft.AspNetCore.Mvc;
using QRCodeGeneratorHelper.Helper.QRCodeGeneratorHelper;
using QRCodeGeneratorHelper.Models;
using QRCodeGeneratorHelper.ViewModels;
using System.Diagnostics;

namespace QRCodeGeneratorHelper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQRCodeGeneratorHelper qRCodeGeneratorHelper;
        private readonly QRGenerateLibrary.QRCode Qrcode;

        public HomeController(ILogger<HomeController> logger, IQRCodeGeneratorHelper qRCodeGeneratorHelper)
        {
            _logger = logger;
            this.qRCodeGeneratorHelper = qRCodeGeneratorHelper;
            this.Qrcode = new QRGenerateLibrary.QRCode();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string text)
        {

            //if (!long.TryParse(text, out long number))
            //    return BadRequest("Invalid input. Please enter a valid number.");
            string baseUrl = "https://dev-eis-portal.mra.mw/api/api/v1/ReorderNotification/Validation";


            //long QRCodeAsBytes = QRGenerateLibrary.QRCode(number);

            //string QRCodeAsImageBase64 = $"data:image/png;base64,{Convert.ToBase64String(QRCodeAsBytes)}";
            DateTime transactionDate = new DateTime(2025, 1, 5);
            string filePath = "C:\\FileShare_Dil";
            string newInvoice = Qrcode.GenerateInvoiceNumber(10,1, transactionDate, 1);
            string QRcodeResult = Qrcode.GenerateQRCodeWithHmac(baseUrl, "K-B-JYwp-B", 2, 19600.00, 0, "JYwp", "90aa239950b887149d068ea32841c997d53d099", filePath);
            Console.WriteLine(newInvoice);
            //string QRCodeAsImageBase64 = QRGenerateLibrary.QRCode.Base10ToBase64(QRcodeResult);
            //string QRCodeAsImageBase64Image = $"data:image/png;base64,{QRcodeResult}";
            Console.WriteLine(QRcodeResult);    

            GenerateQRCodeViewModel model = new GenerateQRCodeViewModel
            {
                QRCodeImageUrl = QRcodeResult
            };

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
