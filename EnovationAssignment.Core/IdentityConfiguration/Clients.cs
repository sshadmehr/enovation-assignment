using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnovationAssignment.IdentityConfiguration
{
	public class Clients
	{
		public static IEnumerable<Client> Get()
		{
			return new List<Client>
				{
						new Client
						{
								ClientId = "enovation-client-id",
								ClientName = "Enovation Client",
								AllowedGrantTypes = GrantTypes.ClientCredentials,
								ClientSecrets = new List<Secret> {new Secret("MySecret".Sha256())}, // TODO: read from configuration file
								AllowedScopes = new List<string> {"api.read", "api.write"}
						}
				};
		}
	}
}
