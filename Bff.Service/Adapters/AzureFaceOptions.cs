namespace Bff.Service.Adapters
{
    public class AzureFaceOptions
    {
        public const string AzureFace = "Azure-Face";

        public string Endpoint { get; set; } = string.Empty;

        public string Key { get; set; } = string.Empty;
    }
}
