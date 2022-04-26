// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog")
            {
                Scopes = { "catalog_fullpermission" }
            },
            new ApiResource("resource_photostock")
            {
                Scopes = { "photostock_fullpermission" }
            },
            new ApiResource("resource_basket")
            {
                Scopes = { "basket_fullpermission" }
            },
            new ApiResource("resource_discount")
            {
                Scopes = {
                    "discount_fullpermission",
                    //"discount_read","discount_write"
                }
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    Description = "User Roles",
                    UserClaims = new[] { ClaimTypes.Role }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission", "Full Permission for Catalog API"),
                new ApiScope("photostock_fullpermission", "Full Permission for PhotoStock API"),
                new ApiScope("basket_fullpermission", "Full Permission for Basket API"),
                new ApiScope("discount_fullpermission", "Full Permission for Discount API"),
                //new ApiScope("discount_read", "Read Permission for Discount API"),
                //new ApiScope("discount_write", "Write Permission for Discount API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "catalog_fullpermission",
                        "photostock_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                },
                new Client()
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "roles",
                        IdentityServerConstants.LocalApi.ScopeName,
                        "catalog_fullpermission",
                        "photostock_fullpermission",
                        "basket_fullpermission",
                        "discount_fullpermission"
                    },
                    AccessTokenLifetime = 1 * 60 * 60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
    }
}