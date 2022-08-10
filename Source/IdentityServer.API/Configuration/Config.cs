using Duende.IdentityServer.Models;

namespace IdentityServer.API.Configuration;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(name: "apitestscope", displayName: "Test API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "apitest-clientid",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("NoPassword@1".Sha256()) /* Need to get this from Configuration */
                },

                // scopes that client has access to
                AllowedScopes = { "apitestscope" }
            }
        };


}