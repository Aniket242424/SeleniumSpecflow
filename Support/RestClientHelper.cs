using RestSharp;
using System.Threading.Tasks;
using ReqnrollProject1.Support;
using System;

public class RestClientHelper
{
    private readonly RestClient _client;

    public RestClientHelper()
    {
        var options = new RestClientOptions("https://api.dev.au.membr.com")
        {
            ThrowOnAnyError = false
        };

        _client = new RestClient(options);
    }

    public async Task<RestResponse> CreatePackageAsync(string jsonBody)
    {
        var token = ConfigReader.GetSection("API", "BearerToken");

        if (string.IsNullOrWhiteSpace(token) || token.Contains("http"))
            throw new InvalidOperationException("Bearer token is invalid. Check appsettings.json");

        var request = new RestRequest("api/gym/55/package?gym=55&locale=en_AU&timezoneOffset=-330", Method.Post);

        request.AddHeader("accept", "application/json, text/plain, */*");
        request.AddHeader("authorization", $"Bearer {token}");
        request.AddHeader("content-type", "application/json;charset=UTF-8");
        request.AddHeader("origin", "https://admin.dev.au.membr.com");
        request.AddHeader("referer", "https://admin.dev.au.membr.com/");

        request.AddStringBody(jsonBody, DataFormat.Json);

        var fullUrl = _client.BuildUri(request);
        Console.WriteLine($"🔵 Sending {request.Method} to {fullUrl}");
        Console.WriteLine("🔑 Token prefix: " + token.Substring(0, 15) + "...");

        var response = await _client.ExecuteAsync(request);

        Console.WriteLine("=== API Response ===");
        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine(response.Content);

        return response;
    }
}
