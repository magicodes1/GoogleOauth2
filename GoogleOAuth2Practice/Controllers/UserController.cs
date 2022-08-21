using GoogleOAuth2Practice.Models;
using GoogleOAuth2Practice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoogleOAuth2Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IOAuth2Service oAuth2Service;

        public UserController(IOAuth2Service oAuth2Service)
        {
            this.oAuth2Service = oAuth2Service;
        }

        [HttpGet("login")]
        public IActionResult Login() => Ok(oAuth2Service.GetOAuth2Url());

        [HttpGet("token")]
        public async Task<IActionResult> GetAccessToken([FromQuery] string code)
        {
            GoogleToken token = await oAuth2Service.GetAccessTokenAsync(code);

            if(token == null)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(GetUserInfo),new { accessToken = token.access_token });
        }

        [HttpGet("userInfo")]
        public async Task<IActionResult> GetUserInfo([FromQuery] string accessToken) => 
                                                                Ok(await oAuth2Service.GetUserInfo(accessToken));
    }
}
