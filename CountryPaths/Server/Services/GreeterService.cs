using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryPaths.Server
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var c = Shared.Countries.GetCountries().Where(c => c.ID == 100).FirstOrDefault();
            var cdata = c.Name ?? "no data";

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name + " from country " + cdata
            });
        }
    }
}
