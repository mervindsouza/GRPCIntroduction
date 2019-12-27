using Grpc.Core;
using GrpcServer;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GRPCServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.FirstName = "User 1";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "User 2";
            }
            else
            {
                output.FirstName = "All Other Users";
            }

            return Task.FromResult(output);
        }
    }
}
