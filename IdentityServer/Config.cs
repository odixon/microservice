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
                AccessTokenLifetime = 20,
                ClientId="weather_client",
                ClientName="Aiden Wang",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("123456".Sha256()) },
                AllowedScopes = {
                    IdentityModel.OidcConstants.StandardScopes.OpenId,
                    IdentityModel.OidcConstants.StandardScopes.Profile,
                    "WeatherApi"
                },
                Claims =
                {
                    new ClientClaim(IdentityModel.JwtClaimTypes.Email, "aiden.wang1@cn.ey.com"),
                    new ClientClaim(IdentityModel.JwtClaimTypes.PhoneNumber, "18191856144")
                }
            }
        };
        public static IEnumerable<ApiScope> ApiScopes =>new[]
        {
            new ApiScope(IdentityModel.OidcConstants.StandardScopes.OpenId),
            new ApiScope(IdentityModel.OidcConstants.StandardScopes.Profile),
            new ApiScope("WeatherApi")
        };
        public static IEnumerable<ApiResource> ApiResources => new []
        {
            new ApiResource("WeatherApi", "天气情况 API")
            {
                Scopes = { "WeatherApi" }
            }
        };
    }
}