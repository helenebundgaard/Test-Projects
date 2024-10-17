using Xunit;
using Moq;
using server;

namespace server_tests
{
    public class WeatherRequestHandlerTests
    {
        [Fact]
        public void TestHandleWeatherRequest_InvalidRequest()
        {
            var mockWeatherService = new Mock<WeatherService>();
            var handler = new WeatherRequestHandler(mockWeatherService.Object);

            string response = handler.HandleWeatherRequest("Invalid request");

            Assert.Equal("Invalid weather request. Use format: Weather <location>", response);
        }

        [Fact]
        public void TestHandleWeatherRequest_ValidRequest()
        {
            // Mocking the WeatherService
            var mockWeatherService = new Mock<WeatherService>();
            string expectedWeather = "The weather in Seattle is sunny with a temperature of 25°C.";
            mockWeatherService.Setup(service => service.GetWeather("Seattle")).Returns(expectedWeather);

            var handler = new WeatherRequestHandler(mockWeatherService.Object);

            string response = handler.HandleWeatherRequest("weather Seattle");

            Assert.Equal(expectedWeather, response);
        }


        [Fact]
        public void TestHandleWeatherRequest_EmptyRequest()
        {
            var mockWeatherService = new Mock<WeatherService>();
            var handler = new WeatherRequestHandler(mockWeatherService.Object);

            string response = handler.HandleWeatherRequest("");

            Assert.Equal("Invalid weather request. Use format: Weather <location>", response);
        }
    }
}
