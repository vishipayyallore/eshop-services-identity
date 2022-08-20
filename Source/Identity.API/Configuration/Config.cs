using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Identity.API.Configuration;

public static class Config
{

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),

            new IdentityResource("color", new [] { "favorite_color" }),

            new IdentityResource()
            {
                Name = "verification",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(name: "eshopapiscope", displayName: "eShop API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // machine to machine client (from quickstart 1). Client Credentials Flow client
            new Client
            {
                ClientId = "api-clientid",
                ClientName = "Client Credentials Client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("NoPassword@1".Sha256()) /* Need to get this from Configuration */
                },

                // scopes that client has access to
                AllowedScopes = { "eshopapiscope" }
            },
            // interactive ASP.NET Core Web App. interactive client using Code Flow + PKCE
            new Client
            {
                ClientId = "web-clientid",
                ClientName = "eShop Server Side Web Application",

                ClientSecrets =
                {
                    new Secret("NoPassword@1".Sha256()) /* Need to get this from Configuration */
                },

                AllowedGrantTypes = GrantTypes.Code,
            
                // where to redirect to after login
                RedirectUris = { "https://localhost:7019/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:7019/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "verification",
                    "eshopapiscope",
                    "color"
                }
            },
            new Client
            {
                ClientName = "eShop Web Application",
                ClientId = "eShop-webclientId",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = new List<string>{ "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                RequirePkce = true,
                AllowAccessTokensViaBrowser = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "eshopapiscope",
                },
                AllowedCorsOrigins = { "http://localhost:4200" },
                RequireClientSecret = false,
                PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-callback" },
                RequireConsent = false,
                AccessTokenLifetime = 600
            }
        };


}