using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace IdentityServer
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                street_address = "1 Pilley",
                locality = "Victoria",
                postal_code = 3001,
                country = "Australia"
                };

                return new List<TestUser>
                {

                    new TestUser
                    {
                        SubjectId = "1",
                        Username = "Jaime",
                        Password = "Fandino",
                        Claims =
                        {
                        new Claim(JwtClaimTypes.Name, "Jaime Fandino"),
                        new Claim(JwtClaimTypes.GivenName, "Jaime"),
                        new Claim(JwtClaimTypes.FamilyName, "Fandino"),
                        new Claim(JwtClaimTypes.Email, "jfandinoh@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.WebSite, "http://jafh.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                            IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    },

                    new TestUser
                    {
                        SubjectId = "2",
                        Username = "Diana",
                        Password = "Casas",
                        Claims =
                        {
                        new Claim(JwtClaimTypes.Name, "Diana Casas"),
                        new Claim(JwtClaimTypes.GivenName, "Diana"),
                        new Claim(JwtClaimTypes.FamilyName, "Casas"),
                        new Claim(JwtClaimTypes.Email, "Diana@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "user"),
                        new Claim(JwtClaimTypes.WebSite, "http://diana.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                            IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    }
                };
            }
        }
    }
}