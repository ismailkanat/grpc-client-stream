using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task<ServiceResult> GetClientStreamMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
        {
            await Task.Run(async () =>
            {
                while (await requestStream.MoveNext())
                {
                    Console.WriteLine($"Incoming message : {requestStream.Current.Message}");
                }
            });

            return new ServiceResult { Message = $"Transfer completed" };
        }
    }
}
