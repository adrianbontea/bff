using System.Threading.Tasks;
using Bff.Console.Client;
using FlashCap;
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

// Capture face image POC

var devices = new CaptureDevices();

var descriptor = devices.EnumerateDescriptors().First();

using var device = await descriptor.OpenAsync(
    descriptor.Characteristics[0],
    async bufferScope =>
    {
        byte[] image = bufferScope.Buffer.ExtractImage();

        var ms = new MemoryStream(image);
        using var fs = new FileStream("D:\\test.jpg", FileMode.Create);
        ms.WriteTo(fs);
        await fs.FlushAsync();
    });


await device.StartAsync();
await Task.Delay(1000);
await device.StopAsync();
