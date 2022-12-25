using System.Threading.Tasks;
using Bff.Console.Client;
using Grpc.Net.Client;
using static Bff.Console.Client.Bff;

using var channel = GrpcChannel.ForAddress("https://localhost:7134");
var bffClient = new BffClient(channel);

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Type messages below to interact with BFF! Type 'QUIT' to exit.");

Console.ForegroundColor = ConsoleColor.White;
var input = Console.ReadLine();

while (input != "QUIT")
{
    var response = await bffClient.InteractAsync(new Request { Text = input });
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(response.Text);

    Console.ForegroundColor = ConsoleColor.White;
    input = Console.ReadLine();
}
