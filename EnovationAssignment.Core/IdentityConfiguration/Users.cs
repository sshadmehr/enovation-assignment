using IdentityModel;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnovationAssignment.IdentityConfiguration
{
  public class Users
  {
    public static List<TestUser> Get()
    {
      return new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "12345678",
                Username = "app1",
                Password = "12345",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Email, "mail@bemail.com"),
                    new Claim(JwtClaimTypes.Role, "admin")
                }
            }
        };
    }
  }
}
