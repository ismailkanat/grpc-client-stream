using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var greeterClient = new GrpcServer.Greeter.GreeterClient(channel);
            Console.WriteLine("Please press enter key to start.");
            Console.ReadLine();
            var request = greeterClient.GetClientStreamMessage();
            var paragraph = "Lorem ipsum dolor sit amet consectetur adipiscing elit Nulla a consequat dolor vel ultrices leo Donec at felis faucibus venenatis neque id viverra dolor Nam vestibulum nibh sit amet ante gravida eget luctus elit ornare Ut et est euismod iaculis ligula quis consequat neque Donec dui libero lobortis eget tortor vel pharetra porttitor dui Nulla facilisi Praesent vehicula libero a justo ultrices tristique";
            var arr = paragraph.Split(' ');

            await Task.Run(async () =>
            {
                foreach (var item in arr)
                {
                    await request.RequestStream.WriteAsync(new GrpcServer.MessageRequest { Message = item });
                    await Task.Delay(100);
                }
            });

            await request.RequestStream.CompleteAsync();

            var response = await request;
            Console.WriteLine($"Service Result : {response.Message}");
        }
    }
}
