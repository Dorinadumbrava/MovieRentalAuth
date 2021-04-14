// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser
             {
                 SubjectId = "d860efca-22d9-47fd-8249-791ba61b07c7",
                 Username = "Anthony",
                 Password = "password",

                 Claims = new List<Claim>
                 {
                     new Claim("given_name", "Anthony"),
                     new Claim("family_name", "Lockwood"),
                     new Claim("address", "alockwood@test.com")
                 }
             },
             new TestUser
             {
                 SubjectId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                 Username = "Lucy",
                 Password = "password",

                 Claims = new List<Claim>
                 {
                     new Claim("given_name", "Lucy"),
                     new Claim("family_name", "Carlyle"),
                     new Claim("address", "lCarlyle@test.com")
                 }
             }
         };
    }
}