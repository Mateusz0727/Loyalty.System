using Loyalty.System.API.Context;
using Loyalty.System.API.Models.Qrcode;
using Loyalty.System.API.Services.QrCodeService;
using Loyalty.System.API.Services.UserPointsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Loyalty.System.API.Controllers.QrCodes
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : Controller
    {
        private readonly QrCodeService _qrCodeService;
        private readonly UserPointsService _userPointsService;

        public QrCodeController(QrCodeService qrCodeService,UserPointsService userPointsService)
        {
            _qrCodeService = qrCodeService;
            _userPointsService = userPointsService;
        }
        [HttpGet]
        public IActionResult GenerateQrCode()
        {
            QrCode qrCode = new()
            {
                ExpiredTime = DateTime.Now.AddMinutes(5),
                Id = User.Id(),
                Token=Guid.NewGuid().ToString(),
            };
            _userPointsService.updateQrCodeToken(qrCode);
            return Ok(JsonConvert.SerializeObject(qrCode));
        }
        [HttpGet("generateQrCodeForPrize")]
        public IActionResult generateQrCodeForPrize()
        {
            QrCode qrCode = new()
            {
                ExpiredTime = DateTime.Now.AddMinutes(5),
                Id = User.Id(),
                Token = Guid.NewGuid().ToString(),
                isPrize= true,
            };
            _userPointsService.updateQrCodeToken(qrCode);
            return Ok(JsonConvert.SerializeObject(qrCode));
        }
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> ReadQrCode([FromBody] QrCode qrCode)
        {
            
            if(!qrCode.isPrize)
                _userPointsService.addUserPoint(qrCode);
            else
            _userPointsService.getPrize(qrCode);

            return Ok("Points added successfully.");
        }
    }
}
