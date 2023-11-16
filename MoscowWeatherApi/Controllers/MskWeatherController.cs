using Microsoft.AspNetCore.Mvc;
using MoscowWeatherApi.Entities;
using MoscowWeatherApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoscowWeatherApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MskWeatherController : ControllerBase
    {
        private readonly ILogger<MskWeatherController> _logger;
        private readonly IMoscowWeatherSrv _mskWeather;

        public MskWeatherController(IMoscowWeatherSrv mskWeather, ILogger<MskWeatherController> logger)
        {
            _mskWeather = mskWeather;
            _logger = logger;
        }

        // GET <MskWeatherController>
        [HttpGet("{date}")]
        public IActionResult GetWeather(DateTime date)
        {
            MoscowWeather weather = _mskWeather.GetWeather(date);
            return Ok(weather);
        }

        // POST <MskWeatherController>
        [HttpPost("{date}")]
        public IActionResult CreateWeather(DateTime date)
        {
            MoscowWeather weather = _mskWeather.CreateWeather(date);
            return Ok(weather);
        }
    }
}
