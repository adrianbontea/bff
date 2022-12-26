using Bff.Core.InputPorts;
using Bff.Core.OutputPorts;
using Bff.Service;
using Grpc.Core;

namespace Bff.Service.Services
{
    public class BffService : Bff.BffBase
    {
        private readonly ILogger<BffService> _logger;
        private readonly IBffService _bffService;

        public BffService(ILogger<BffService> logger, IBffService bffService)
        {
            _logger = logger;
            _bffService = bffService;
        }

        public async override Task<Response> Interact(Request request, ServerCallContext context)
        {
            var output = await _bffService.InteractAsync(new Input(Guid.NewGuid().ToString(), request.Text));
            return new Response { Text = output.Message, ShouldCaptureFaceImage = output.ShouldCaptureFaceImage };
        }
    }
}
