using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class ClientHandler : IClientHandler
    {
        private readonly Calculator _calc;
        private readonly Die _die;
        private readonly WeatherRequestHandler _weatherRequestHandler;
        private readonly ColorTheory _colorTheory;


        public ClientHandler(Calculator calc, Die die, WeatherRequestHandler weatherRequestHandler, ColorTheory colorTheory)
        {
            _calc = calc;
            _die = die;
            _weatherRequestHandler = weatherRequestHandler;
            _colorTheory = colorTheory;
        }

        public void HandleClient(TcpClient client)
        {
            Guid clientId = Guid.NewGuid();
            Console.WriteLine($"Client {clientId} connected.");

            using (var stream = client.GetStream())
            using (var writer = new StreamWriter(stream) { AutoFlush = true })
            using (var reader = new StreamReader(stream))
            {
                try
                {
                    while (client.Connected)
                    {
                        string request = reader.ReadLine();

                        if (request != null)
                        {
                            Console.WriteLine($"Received message from {clientId}: {request}");

                            string response = HandleRequest(request);

                            // Echoing message
                            writer.WriteLine("Response: " + response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred for client {clientId}: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"Client disconnected: {clientId}");
                    client.Close();
                }
            }
        }

        public virtual string HandleRequest(string request)
        {
            string[] parts = request.Split(' ');

            if (string.IsNullOrEmpty(request))
            {
                return "Invalid request";
            }

            if (parts.Length == 0)
            {
                return "Invalid request";
            }

            try
            {
                switch (parts[0])
                {
                    case "dice":
                        return _die.Roll().ToString();
                    case "add":
                        if (parts.Length != 3) return "Invalid parameters";
                        return _calc.Add(float.Parse(parts[1]), float.Parse(parts[2])).ToString();
                    case "subtract":
                        if (parts.Length != 3) return "Invalid parameters";
                        return _calc.Subtract(float.Parse(parts[1]), float.Parse(parts[2])).ToString();
                    case "multiply":
                        if (parts.Length != 3) return "Invalid parameters";
                        return _calc.Multiply(float.Parse(parts[1]), float.Parse(parts[2])).ToString();
                    case "divide":
                        if (parts.Length != 3) return "Invalid parameters";
                        return _calc.Divide(float.Parse(parts[1]), float.Parse(parts[2])).ToString();
                    case "weather":
                        return _weatherRequestHandler.HandleWeatherRequest(request);
                    case "analogous":
                        if (parts.Length != 2) return "Invalid parameters";
                        return string.Join(", ", ColorTheory.GetAnalogousColors(parts[1]));
                    case "complementary":
                        if (parts.Length != 2) return "Invalid parameters";
                        return ColorTheory.GetComplementaryColor(parts[1]);
                    case "addcolors":
                        if (parts.Length < 3) return "Invalid parameters";
                        return ColorTheory.AddColors(parts.Skip(1).ToArray());
                    default:
                        return "Unknown request";
                }
            }
            catch (DivideByZeroException ex)
            {
                return ex.Message;
            }
        }

    }
}




