using com.btc.process.manager.System.Abstract;
using com.btc.process.security.middleware;
using com.btc.process.type.Dto.System;
using com.btc.process.utility.elasticsearch.Concrete;
using com.btc.process.utility.redis.Abstract;
using com.btc.process.utility.redis.Concrete;
using com.btc.type.Entity.System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace com.btc.app.system.Controllers
{
    [ApiController]
    [EnableCors("default")]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class SystemController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SystemController> _logger;
        private readonly IUserManager _userManager;
        private readonly IRedisCacheService _redisManager;
        public ElasticSearch _elasticSearch = new ElasticSearch();
        private JwtConfigure jwtMiddleware = new JwtConfigure();
        public SystemController(ILogger<SystemController> logger, IUserManager userManager, IRedisCacheService redisManager)
        {
            _logger = logger;
            _userManager = userManager;
            _redisManager = redisManager;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> GetWeatherForecast()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet(Name = "GetCurrentUser")]
        public async Task<User> GetCurrentUser()
        {
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var userId = await jwtMiddleware.parseJwtToken(authHeader);
            var user = await _userManager.GetById(Int32.Parse(userId.ToString()));
            return user;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
          
            return Ok(_userManager.GetAll());
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
            return Ok(await _userManager.Create(user));
        }

        [HttpGet(Name = "GetUserNameById")]
        public IActionResult GetUserNameById(int id)
        {
            return Ok(_userManager.GetName(id));
        }

        [HttpGet(Name ="GetRedisCache")]
        public async Task<IActionResult> GetRedisCache(string key)
        {
            return Ok(await _redisManager.GetValueAsync(key));
        }

        [HttpPost(Name = "CreateRedisValue")]
        public async Task<IActionResult> CreateRedisValue(string key,string value)
        {
            return Ok(await _redisManager.SetValueAsync(key,value));
        }

        [HttpGet(Name = "CreateElasticSearchIndex")]
        public IActionResult CreateElasticSearchIndex(string key)
        {
            _elasticSearch.IndexItems("esearchitems", _userManager.GetAll().ToList());
            return Ok();
        }

        [HttpGet(Name = "GetUserListElasticSearch")]
        public async Task<IActionResult> GetUserListElasticSearch()
        {
            return Ok(await _elasticSearch.Get());
        }

        [HttpGet(Name = "GetNameElasticSearch")]
        public async Task<IActionResult> GetNameElasticSearch(string name)
        {
            return Ok(await _elasticSearch.GetName(name));
        }

        [HttpGet(Name = "GetRedisCacheObject")]
        public async Task<IActionResult> GetRedisCacheObject(string key)
        {
            return Ok(await _redisManager.GetValueAsync(key));
        }

        [HttpPost(Name = "CreateRedisValueObject")]
        public async Task<IActionResult> CreateRedisValueObject(string key, UserDto value)
        {
            return Ok(await _redisManager.SetJsonValueAsync(key, value));
        }

        [HttpGet(Name = "GetRedisCurrentUser")]
        public async Task<IActionResult> GetRedisCurrentUser()
        {
            var user = await GetCurrentUser();
            return Ok(await _redisManager.GetValueAsync(user.UserCode));
        }

        [HttpGet(Name = "GetElasticSearchCurrentUser")]
        public async Task<IActionResult> GetElasticSearchCurrentUser()
        {
            var user = await GetCurrentUser();
            return Ok(await _elasticSearch.GetById(user.Id));
        }


        [HttpPost(Name = "Logout")]
        public async Task<IActionResult> Logout()
        {
            var user = await GetCurrentUser();
            _redisManager.Clear(user.UserCode);
            return Ok("");
        }
    }
}
