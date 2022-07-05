using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Parmezzan.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> { 
                new ApiScope("parmezzan", "Parmezzan Server"),
                new ApiScope(name: "read", displayName: "Read"),
                new ApiScope(name: "write", displayName: "Write"),
                new ApiScope(name: "delete", displayName : "Delete")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret ("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile"}
                },
                new Client
                {
                    ClientId = "parmezzan",
                    ClientSecrets = { new Secret ("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7062/signin-oidc" },//14533
                    PostLogoutRedirectUris = { "https://localhost:7062/signout-callback-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "parmezzan"
                    }
                }
            };

    }
}
