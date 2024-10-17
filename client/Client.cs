using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class Client : IClient
    {
        private TcpClient _client;
        private Thread _receiveThread;
        private StreamWriter _writer;

        public Client(string host = "localhost", int port = 12000)
        {
            Console.Title = "Client";

            _client = new TcpClient();
            _client.Connect(host, port);
            Console.WriteLine("Connected to server...");
            Console.WriteLine("It is possible to use Dice method to chose a random number.");
            Console.WriteLine("Example: Dice");
            Console.WriteLine("When you input 'Weather' and some place you will get weather in that place.");
            Console.WriteLine("Example: Weather London");
            Console.WriteLine("You can use Calculator functions 'Add' 'Subtract' 'Multiply' 'Divide'");
            Console.WriteLine("Example: Add 5 5");
            Console.WriteLine("You can use ColorTheory functions 'analogous' 'complementary' 'addcolors'");
            Console.WriteLine("Example: analogous red");
            Console.WriteLine("Example: complementary blue");
            Console.WriteLine("Example: addcolors red yellow");

            // Start a thread to receive messages
            _receiveThread = new Thread(() => ReceiveMessages());
            _receiveThread.Start();

            // Initialize StreamWriter for sending messages
            _writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };
        }

        public void SendMessage(string message)
        {
            _writer.WriteLine(message.ToLower());
        }

        public void ReceiveMessages()
        {
            using (StreamReader reader = new StreamReader(_client.GetStream()))
            {
                while (_client.Connected)
                {
                    string message = reader.ReadLine().ToLower();
                    if (message != null)
                    {
                        Console.WriteLine(message);
                    }
                    
                }
            }
        }

    }
}
