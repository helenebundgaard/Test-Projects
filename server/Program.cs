using System.Net.Sockets;
using System.Net;
using server;


namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            Die die = new Die();
            WeatherService weatherService = new WeatherService();
            WeatherRequestHandler weatherRequestHandler = new WeatherRequestHandler(weatherService);
            ColorTheory colorTheory = new ColorTheory();
            IClientHandler clientHandler = new ClientHandler(calc, die, weatherRequestHandler, colorTheory);
            IServer server = new Server(clientHandler);

            server.Start();

            Console.WriteLine("Press enter to stop the server...");
            Console.ReadLine(); // This line blocks the main thread until the user presses enter

            server.Stop();
        }
    }
}

