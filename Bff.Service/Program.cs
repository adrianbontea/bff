using System.Net.Http.Headers;
using Azure;
using Azure.AI.TextAnalytics;
using Bff.Core.InputPorts;
using Bff.Core.OutputPorts;
using Bff.Service.Adapters;
using Bff.Service.Services;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.Configure<OpenAiOptions>(
    builder.Configuration.GetSection(OpenAiOptions.OpenAi));

builder.Services.Configure<AzureFaceOptions>(
    builder.Configuration.GetSection(AzureFaceOptions.AzureFace));

builder.Services.Configure<AzureLanguageOptions>(
    builder.Configuration.GetSection(AzureLanguageOptions.AzureLanguage));

builder.Services.AddHttpClient<IConversationService, OpenAiConversationService>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<OpenAiOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.ApiKey);
}).AddPolicyHandler(GetRetryPolicy());

builder.Services.AddTransient<IEmotionService, AzureEmotionService>();
builder.Services.AddTransient<IBffService, Bff.Core.BffService>();
builder.Services.AddSingleton<IFaceClient, FaceClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<AzureFaceOptions>>().Value;
    return new FaceClient(new ApiKeyServiceClientCredentials(options.Key)) { Endpoint = options.Endpoint };
});
builder.Services.AddSingleton<TextAnalyticsClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<AzureLanguageOptions>>().Value;
    return new TextAnalyticsClient(new Uri(options.Endpoint), new AzureKeyCredential(options.Key));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BffService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}
