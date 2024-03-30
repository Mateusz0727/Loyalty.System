using Loyalty.System.API.Models.Qrcode;
using Loyalty.System.API.Models.User;
using Loyalty.System.API.Services.UserPointsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.System.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserPointsController : Controller
    {
        private readonly UserPointsService _userPointsService;

        public UserPointsController(UserPointsService userPointsService)
        {
            _userPointsService = userPointsService;
        }

        [HttpGet("id")]
        public async Task<UserPointsModel> Index(string id)
        {
            return _userPointsService.getUserPoints(id);
        }

       
       /* [HttpPost]


        public async Task<IActionResult> AddUserPoint([FromBody] QrCode qrCode)
        {


            _userPointsService.addUserPoint(qrCode);



            return Ok("Points added successfully.");
        }*/
    }
}

