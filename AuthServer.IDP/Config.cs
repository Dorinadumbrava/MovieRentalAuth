// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthServer.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
                new ApiScope(
                    "movierentalapi",
                    "Movie Rental API")
           };
        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource(
                    "movierentalapi",
                    "Movie Rental API",
                    new List<string>() { "role" }) //this ensures the role is sent to API
                {
                    Scopes = { "movierentalapi"},
                    ApiSecrets = { new Secret("apisecret".Sha256()) }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                 new Client
                {
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 120,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientName = "Movie Rental", //your app name
                    ClientId = "movierentalclient", //your app clientID
                    AllowedGrantTypes = GrantTypes.Code, //the flow we use
                    RequirePkce = true, //Add PKce protection (doesn't work without)
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string>() //unde userul se va intoarce dupa ce face login pe IDP
                    {
                        "https://localhost:44369/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44369/signout-callback-oidc"
                    },
                    AllowedScopes = //the scopes we have in the identity resources and our client can request
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "movierentalapi"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256()) //the test clientSecret
                    }
                }
            };
    }
}