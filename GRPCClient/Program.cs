using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServer;
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

            var greeterInput = new HelloRequest { Name = "World!" };
            var greeterChannel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
            var greeterclient = new Greeter.GreeterClient(greeterChannel);
            var greeterReply = await greeterclient.SayHelloAsync(greeterInput);

            var customerInput = new CustomerLookupModel { UserId = 1 };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var customerClient = new Customer.CustomerClient(channel);
            var customerReply = await customerClient.GetCustomerInfoAsync(customerInput);

            Console.WriteLine(customerReply.FirstName);
            Console.ReadLine();
        }
    }
}
