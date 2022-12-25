using Newtonsoft.Json;

namespace Bff.Service.Adapters
{
    public class Completion
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
    }
}
