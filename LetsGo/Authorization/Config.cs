using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using System.Security.Claims;

namespace WebApplication4
{
    public static class Config
    {
        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
                //// client credentials flow client
                //new Client
                //{
                //    ClientId = "client",
                //    ClientName = "Client Credentials Client",

                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                //    AllowedScopes = { "api1" }
                //},

                new Client
                {
                    ClientId = "LetsGo.MVC",
                    ClientName = "Let's GO",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {
                        "http://localhost:5001/signin-oidc",
                        "http://localhost:54476/signin-oidc"
                    },
                    AllowedScopes = { "openid", "profile", "email"},

                }
            };

        public static IEnumerable<IdentityResource> IdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> ApiResources() =>
         new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
            };
    }
}
