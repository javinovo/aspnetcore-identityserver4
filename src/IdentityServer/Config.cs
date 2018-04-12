using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
            {
                new ApiResource("api1", "My API", new [] { JwtClaimTypes.Name, JwtClaimTypes.FamilyName, JwtClaimTypes.Email, "domain" }),
                new ApiResource("api2", "My other API")
                {
                    
                },
                new ApiResource("ERP", "ERP")
                {
                    Scopes =
                    {
                        new Scope("dept1", "Department 1"),
                        new Scope("dept2", "Department 2"),

                    }
                }
            };

        public static IEnumerable<Client> GetClients() => new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    // scopes that client has access to
                    // Scopes define the resources in your system that you want to protect, e.g. APIs.
                    AllowedScopes = { "api1" }
                },
                
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "api1" }
                },
                
                new Client
                {
                    ClientId ="erp.client",
                    //AllowedGrantTypes = GrantTypes.
                    AllowedScopes = { "ERP "},
                }
            };

        public static IEnumerable<TestUser> GetUsers() => new List<TestUser>
        {
                //new TestUser
                //{
                //    SubjectId = "1",
                //    Username = "alice",
                //    Password = "password"
                //},
                //new TestUser
                //{
                //    SubjectId = "2",
                //    Username = "bob",
                //    Password = "password"
                //}
            new TestUser{SubjectId = "818727", Username = "alice", Password = "password",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim("domain", @"DOMAIN1\alice"),
                    new Claim("domain", @"DOMAIN2\alice"),
                }
            },
            new TestUser{SubjectId = "88421113", Username = "bob", Password = "password",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim("location", "somewhere"),
                }
            },
        };
    }
}