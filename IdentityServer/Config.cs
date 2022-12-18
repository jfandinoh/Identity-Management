using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {

        public static IEnumerable<IdentityResource> IdentityResources =>
        new []
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
        new []
        {
            new ApiScope("api.read"),
            new ApiScope("api.write"),
        };
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
        new ApiResource("api")
        {
            Scopes = new List<string> {"api.read", "api.write"},
            ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
            UserClaims = new List<string> {"role"}
        }
        };

        public static IEnumerable<Client> Clients =>
        new[]
        {
            // Machine to Machine client credentials flow client
            new Client
            {
                ClientId = "client",
                ClientName = "Client Credentials",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("password".Sha256())},

                AllowedScopes = {"api.read", "api.write"}
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = {new Secret("password".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                // Where to redirect to after login
                RedirectUris = {"https://localhost:5444/signin-oidc"},

                // Where to redirect to after logout
                FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},

                AllowOfflineAccess = true,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api.read"
                    },
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            },
        };
    }
}