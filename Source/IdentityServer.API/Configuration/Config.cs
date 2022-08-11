using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer.API.Configuration;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(name: "apitestscope", displayName: "Test API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // machine to machine client (from quickstart 1)
            new Client
            {
                ClientId = "api-clientid",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("NoPassword@1".Sha256()) /* Need to get this from Configuration */
                },

                // scopes that client has access to
                AllowedScopes = { "apitestscope" }
            },
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web-clientid",

                ClientSecrets =
                {
                    new Secret("NoPassword@1".Sha256()) /* Need to get this from Configuration */
                },

                AllowedGrantTypes = GrantTypes.Code,
            
                // where to redirect to after login
                RedirectUris = { "https://localhost:7019/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:7019/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };


}