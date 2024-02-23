using Loyalty.System.API.Context;
using Loyalty.System.API.Models;
using Loyalty.System.API.Services.QrCodeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Loyalty.System.API.Controllers.QrCodes
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : Controller
    {
        private readonly QrCodeService _qrCodeService;
        public QrCodeController(QrCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            QrCode qrCode = new()
            {
                ExpiredTime = DateTime.Now.AddHours(1),
                Id = User.Id(),
            };
            return Ok(JsonConvert.SerializeObject(qrCode));


        }
    }
}
