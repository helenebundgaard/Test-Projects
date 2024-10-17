using Xunit;
using server;

namespace server_tests
{
    public class WeatherServiceTests
    {
        [Fact]
        public void GetWeather_ShouldReturnWeatherInfo()
        {
            // Arrange
            var weatherService = new WeatherService();

            // Act
            var result = weatherService.GetWeather("New York");

            // Assert
            Assert.Contains("The weather in New York is sunny with a temperature of 25°C.", result);
        }
    }
}
