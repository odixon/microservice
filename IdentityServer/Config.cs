using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer
{
    internal class Config
    {
        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId="user_client",
                ClientName="Aiden Wang",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("user_secret".Sha256()) },
                AllowedScopes = {
                    "UserApi"
                },
                Claims =
                {
                    new ClientClaim(IdentityModel.JwtClaimTypes.Email, "hiwangjunhui@hotmail.com"),
                    new ClientClaim(IdentityModel.JwtClaimTypes.PhoneNumber, "18191856144")
                }
            }
        };
        public static IEnumerable<ApiScope> ApiScopes =>new[]
        {
            new ApiScope("UserApi")
        };
        public static IEnumerable<ApiResource> ApiResources => new []
        {
            new ApiResource("UserApi", "User API")
        };
    }
}
