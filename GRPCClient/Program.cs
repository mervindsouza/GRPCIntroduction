using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GRPCServer;

namespace GRPCClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // ignore invalid development certificate for linux distros
            // use dotnet dev-certs https --trust on windows or mac os

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            var input = new HelloRequest { Name = "World!" };
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(input);

            Console.WriteLine(reply.Message);
            Console.ReadLine();
        }
    }
}
