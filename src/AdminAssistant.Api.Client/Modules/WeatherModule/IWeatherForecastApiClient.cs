using AdminAssistant.Api;

namespace AdminAssistant.WebAPIClient.v1.WeatherModule;

public interface IWeatherForecastApiClient
{
    [Get("/api/v1/weather-module/WeatherForecast")]
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync(CancellationToken cancellationToken = default);
}
