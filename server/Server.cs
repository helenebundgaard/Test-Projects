using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    public class Server : IServer
    {
        private readonly TcpListener _listener;
        private readonly IClientHandler _clientHandler;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _acceptClientsTask;

        public event Action ServerStarted;

        public Server(IClientHandler clientHandler, string ipAddress = "0.0.0.0", int port = 12000)
        {
            Console.Title = "Server";
            _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
            _clientHandler = clientHandler;
        }

        public void Start()
        {
            // Register a handler for SIGTERM
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Stop();

            _listener.Start();
            Console.WriteLine($"Server started... listening on port {((IPEndPoint)_listener.LocalEndpoint).Port}");
            ServerStarted?.Invoke();

            _cancellationTokenSource = new CancellationTokenSource();
            _acceptClientsTask = AcceptClientsAsync(_cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _listener.Stop();
            _acceptClientsTask.Wait();
        }

        private async Task AcceptClientsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => _clientHandler.HandleClient(client), cancellationToken);
                }
                catch (ObjectDisposedException) when (cancellationToken.IsCancellationRequested)
                {
                    // Listener was stopped
                    break;
                }
                catch (OperationCanceledException)
                {
                    // Task was canceled
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in AcceptClientsAsync: {ex.Message}");
                }
            }
        }
    }
}
