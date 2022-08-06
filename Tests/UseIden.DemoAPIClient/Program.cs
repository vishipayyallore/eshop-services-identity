using IdentityModel.Client;
using System.Text.Json;
using static System.Console;

// discover endpoints from metadata
var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
if (disco.IsError)
{
    WriteLine(disco.Error);
    return;
}

// request token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "client-apitest",
    ClientSecret = "NoPassword@1",
    Scope = "apitest"
});

if (tokenResponse.IsError)
{
    WriteLine(tokenResponse.Error);
    return;
}

WriteLine(tokenResponse.AccessToken);

// call api
var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:7062/api/Identity");
if (!response.IsSuccessStatusCode)
{
    WriteLine(response.StatusCode);
}
else
{
    var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
    WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
}

WriteLine("\n\nPress any key ...");
ReadKey();


