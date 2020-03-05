using BlazorEndToEndClientSide.EndToEnd.Data.EndTEnd;
using BlazorEndToEndClientSide.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEndToEndClientSide.Server.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //THis holds data context object to comunicate with the database:
        private readonly EndToEndContext _context;

        //Inject the EndToEndContext using dependency injection
        public WeatherForecastController(EndToEndContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/WeatherForecast/GetAsync")]
        public async Task<List<WeatherForecastDTO>> GetAsync()
        {
            List<WeatherForecast> result = await _context.WeatherForecast.ToListAsync();

            return result.Select(x => new WeatherForecastDTO
            {
                Id = x.Id,
                Date = x.Date,
                Summary = x.Summary,
                TemperatureC = x.TemperatureC,
                TemperatureF = x.TemperatureF,
                UserName = x.UserName,
            }).ToList();

            //var colWeatherForecast = new List<WeatherForecastDTO>();
            //foreach (var forecast in result)
            //{
            //    var weatherForecastDTO = new WeatherForecastDTO
            //    {
            //        Id = forecast.Id,
            //        Date = forecast.Date,
            //        Summary = forecast.Summary,
            //        TemperatureC = forecast.TemperatureC,
            //        TemperatureF = forecast.TemperatureF,
            //        UserName = forecast.UserName,
            //    };



            //    colWeatherForecast.Add(weatherForecastDTO);

            //}

            //return colWeatherForecast;

        }

        [HttpPost]
        [Route("/api/WatherForecast/Post")]
        public void Post([FromBody] WeatherForecastDTO model)
        {
            WeatherForecast weatheForecast = new WeatherForecast
            {
                Id = model.Id,
                UserName = model.UserName,
                Date = model.Date,
                Summary = model.Summary,
                TemperatureC = model.TemperatureC,
                TemperatureF = model.TemperatureF,
            };

            _context.Add(weatheForecast);
            _context.SaveChanges();
        }

        [HttpPut]
        [Route("api/WatherForecast/Put")]
        public void Put([FromBody] WeatherForecastDTO model)
        {
            WeatherForecast existingForecast = _context.WeatherForecast.FirstOrDefault(w => w.Id == model.Id);

            if (existingForecast != null)
            {
                existingForecast.Date = model.Date;
                existingForecast.Summary = model.Summary;
                existingForecast.TemperatureC = model.TemperatureC;
                existingForecast.TemperatureF = model.TemperatureF;
                existingForecast.UserName = model.UserName;
                _context.SaveChanges();
            }
        }
    }
}
