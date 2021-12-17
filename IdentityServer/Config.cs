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
                    "user"
                },
                Claims =
                {
                    new ClientClaim(IdentityModel.JwtClaimTypes.Email, "hiwangjunhui@hotmail.com"),
                    new ClientClaim(IdentityModel.JwtClaimTypes.PhoneNumber, "18191856144")
                }
            }
        };
        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("user")
        };
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("aiden") //audience,name其实是audience
            {
                Scopes = new[]{ "user"} //如果这里不写scope，生成的access_token中不包含audience
            }
        };
    }
}
