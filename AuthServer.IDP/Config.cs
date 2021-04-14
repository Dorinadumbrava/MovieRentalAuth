﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
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
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> Apis =>
           new ApiResource[]
           { };

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
                    AllowedScopes = //the scopes we have in the identity resources
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256()) //the test clientSecret
                    }
                }
            };
    }
}