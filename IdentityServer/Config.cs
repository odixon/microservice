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
                ClientId="weather_client",
                ClientName="Aiden Wang",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("weather_client".Sha256()) },
                AllowedScopes = {
                    //IdentityModel.OidcConstants.StandardScopes.OpenId,
                    //IdentityModel.OidcConstants.StandardScopes.Profile,
                    "WeatherScope"
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
        //    new ApiScope(IdentityModel.OidcConstants.StandardScopes.OpenId),
        //    new ApiScope(IdentityModel.OidcConstants.StandardScopes.Profile),
            new ApiScope("WeatherScope")
        };
        public static IEnumerable<ApiResource> ApiResources => new []
        {
            new ApiResource("WeatherApi", "天气情况 API")
            {
                Scopes = { "WeatherScope" }
            }
        };
    }
}