namespace Bff.Service.Adapters
{
    public class OpenAiOptions
    {
        public const string OpenAi = "OpenAi";

        public string BaseUrl { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;
    }
}
