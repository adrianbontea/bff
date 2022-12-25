using Newtonsoft.Json;

namespace Bff.Service.Adapters
{
    public class PromptRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; }

        [JsonProperty("temperature")]
        public decimal Temperature { get; set; }
    }
}
