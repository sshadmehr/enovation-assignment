using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnovationAssignment.IdentityConfiguration
{
  public class Scopes
  {
    public static IEnumerable<ApiScope> GetApiScopes()
    {
      return new[]
      {
            new ApiScope("api.read", "Read Access to the API"),
            new ApiScope("api.write", "Write Access to the API"),
        };
    }
  }
}
