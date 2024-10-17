using client;

Client client = new Client();

while (true)
{
    string message = Console.ReadLine();

    // Send the message to the server
    client.SendMessage(message);
}



