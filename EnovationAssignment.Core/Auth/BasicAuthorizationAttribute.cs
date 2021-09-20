using Microsoft.AspNetCore.Authorization;

namespace EnovationAssignment.Auth
{
	public class BasicAuthorizationAttribute : AuthorizeAttribute
  {
    public BasicAuthorizationAttribute()
    {
      Policy = "BasicAuthentication";
    }
  }
}
