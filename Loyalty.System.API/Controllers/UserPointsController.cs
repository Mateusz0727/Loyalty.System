using Loyalty.System.API.Services.UserPointsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loyalty.System.API.Controllers
{
    [AllowAnonymous]
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
        public async Task<ushort> Index(string id)
        {
            return _userPointsService.getUserPoints(id);
        }
        [HttpPut("id")]
        public async Task addUserPoint(string id)
        {
            _userPointsService.addUserPoint(id);
        }
    }
}
