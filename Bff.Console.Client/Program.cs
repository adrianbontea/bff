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

    if(response.ShouldCaptureFaceImage == true)
    {
        var faceImage = await CaptureFaceImageAsync();
        //TO DO: Adjust gRPC Request model in proto, server and client-side implementation to be able to send the image content.
    }

    Console.ForegroundColor = ConsoleColor.White;
    input = Console.ReadLine();
}

// Capture face image POC

async Task<Stream> CaptureFaceImageAsync()
{
    var result = new MemoryStream();
    var devices = new CaptureDevices();

    var descriptor = devices.EnumerateDescriptors().FirstOrDefault();

    if (descriptor != null)
    {
        using var device = await descriptor.OpenAsync(
            descriptor.Characteristics.First(),
             bufferScope =>
            {
                byte[] image = bufferScope.Buffer.ExtractImage();
                result = new MemoryStream(image);
            });


        await device.StartAsync();
        await Task.Delay(1000);
        await device.StopAsync();
    }

    return result;
}
