using Bff.Core.OutputPorts;
using System.Net.Http.Formatting;

namespace Bff.Service.Adapters
{
    public class OpenAiConversationService : IConversationService
    {
        private readonly HttpClient _httpClient;
        private const string Model = "text-davinci-003";
        private const int MaxTokens = 360;
        private const decimal Temperature = 0.9m;

        public OpenAiConversationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ChatAsync(string message)
        {
            var response = await _httpClient.PostAsJsonAsync<PromptRequest>("completions", new PromptRequest { MaxTokens = MaxTokens, Temperature = Temperature, Model = Model, Prompt = message });
            var completionResponse = await response.Content.ReadAsAsync<CompletionResponse>();

            return completionResponse?.Choices.FirstOrDefault()?.Text;
        }
    }
}
