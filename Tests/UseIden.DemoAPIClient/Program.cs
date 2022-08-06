using IdentityModel.Client;

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

WriteLine("\n\nPress any key ...");
ReadKey();


