using BrunoCampiol.Domain.Core.Interfaces;
using BrunoCampiol.Domain.Core.Notifications;
using BrunoCampiol.Services.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrunoCampiol.Services.Api.Controllers
{
    [Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(IHandler<DomainNotification> notifications)
            : base(notifications)
        {
            
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<WeatherForecast> Get()
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ResponseApi();
            }

            return ResponseApi(() =>
            {
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            });
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}