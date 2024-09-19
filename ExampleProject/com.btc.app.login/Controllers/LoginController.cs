using com.btc.process.manager.System.Abstract;
using com.btc.process.security.middleware;
using com.btc.process.type.Dto.System;
using com.btc.process.utility.redis.Abstract;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace com.btc.app.login.Controllers
{
    [ApiController]
    [EnableCors("default")]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<LoginController> _logger;
        private readonly IUserManager _userManager;
        private readonly IRedisCacheService _redisManager;
        private JwtConfigure jwtMiddleware = new JwtConfigure();
        public LoginController(ILogger<LoginController> logger, IUserManager userManager, IRedisCacheService redisManager)
        {
            _logger = logger;
            _userManager = userManager;
            _redisManager = redisManager;
        }

        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var response = _userManager.GetUserByName(userLoginDto.UserName);
         
            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var token = await jwtMiddleware.generateJwtToken(response);
            _redisManager.SetJsonValueAsync(userLoginDto.UserName, response);
            return Ok(token);
        }
    }
}
