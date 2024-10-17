using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class WeatherRequestHandler
    {
        private readonly WeatherService _weatherService;

        public WeatherRequestHandler()
        {
            _weatherService = new WeatherService();
        }

        public WeatherRequestHandler(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public virtual string HandleWeatherRequest(string request)
        {
            string[] parts = request.Split(' ');

            if (parts.Length != 2 || parts[0].ToLower() != "weather")
            {
                return "Invalid weather request. Use format: Weather <location>";
            }

            string location = parts[1];
            return _weatherService.GetWeather(location);
        }
    }
}
