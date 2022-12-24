using Bff.Core.InputPorts;
using Bff.Core.OutputPorts;

namespace Bff.Core
{
    public class BffService : IBffService
    {
        private readonly IEmotionService _emotionsService;
        private readonly IConversationService _conversationService;

        public BffService(IEmotionService emotionsService, IConversationService conversationService)
        {
            _emotionsService = emotionsService;
            _conversationService = conversationService;
        }

        public async Task<Output> InteractAsync(Input input)
        {
            throw new NotImplementedException();
        }
    }
}
