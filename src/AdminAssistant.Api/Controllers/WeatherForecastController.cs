using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Api.Controllers;

[ApiController]
[Route("api/v1/weather-module/[controller]")]
[ApiExplorerSettings(GroupName = "Weather Module")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    [EndpointDescription("Retrieve a list of weather forecast information")]
    [EndpointSummary("Get Weather")]
    [Tags("Weather")]
    [HttpGet(Name = "GetWeatherForecast")]
    [ProducesResponseType<IEnumerable<WeatherForecast>>(StatusCodes.Status200OK, "application/json")]
    public IEnumerable<WeatherForecast> Get()
    {
        logger.LogInformation("Retrieving weather forecast data");

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}
