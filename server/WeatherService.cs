using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace server
{
    public class WeatherService
    {
       
        public virtual string GetWeather(string location)
        {
            // Simulate fetching weather data for the locations.
            // In a real-world scenario, you might call an external weather API here
            return $"The weather in {location} is sunny with a temperature of 25°C.";
        }
    }
}
