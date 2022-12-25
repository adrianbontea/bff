using Newtonsoft.Json;

namespace Bff.Service.Adapters
{
    public class CompletionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("choices")]
        public IList<Completion> Choices { get; set; }

        [JsonProperty("usage")]
        public CompletionUsage Usage { get; set; }
    }
}
