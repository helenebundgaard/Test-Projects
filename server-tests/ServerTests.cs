using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace server_tests
{
    public class ServerTests
    {
        private readonly Calculator _calculator;
        private readonly Die _die;
        private readonly IClientHandler _clientHandler;
        private readonly IServer _server;
        private readonly WeatherService _weatherService;
        private readonly WeatherRequestHandler _weatherRequestHandler;
        private readonly ColorTheory _colorTheory;

        public ServerTests()
        {
            _calculator = new Calculator();
            _die = new Die();
            _weatherService = new WeatherService();
            _colorTheory = new ColorTheory();
            _weatherRequestHandler = new WeatherRequestHandler(_weatherService);
            _clientHandler = new ClientHandler(_calculator, _die, _weatherRequestHandler, _colorTheory);
            _server = new Server(_clientHandler);
        }

        [Fact]
        public async Task Server_ShouldAcceptClientConnection()
        {
            // Arrange
            var serverStarted = new TaskCompletionSource<bool>();
            _server.ServerStarted += () => serverStarted.SetResult(true);
            Task serverTask = Task.Run(() => _server.Start());

            // Wait for the server to start
            await serverStarted.Task;

            // Act
            TcpClient client = new TcpClient();
            await client.ConnectAsync(IPAddress.Loopback, 12000);

            // Assert
            Assert.True(client.Connected);

            // Cleanup
            _server.Stop();
            await serverTask;
            client.Close();
        }

    }
}
