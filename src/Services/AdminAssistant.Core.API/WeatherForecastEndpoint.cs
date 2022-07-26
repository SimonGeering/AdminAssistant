namespace AdminAssistant.Core.API
{
    public static class WeatherForecastEndpoint
    {
        public static void MapWeatherForecastEndpoint(this IEndpointRouteBuilder app) =>
            app.MapGet("/weatherforecast", () =>
            {
                var Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                 .ToArray();
            })
            .WithName("GetWeatherForecast");
    }
}
