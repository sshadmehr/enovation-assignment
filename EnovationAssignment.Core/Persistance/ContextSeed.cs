using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnovationAssignment.Persistance
{
	public class ContextSeed
	{
		private readonly AppDbContext _appDbContext;
		public ContextSeed(AppDbContext appDbContext)
		{
			this._appDbContext = appDbContext;
		}
		public void Seed()
		{
			_appDbContext.People.Add(
				new Models.Person { Gender = Models.Gender.Male, Name = "Joe" }
			);

			_appDbContext.People.Add(
				new Models.Person { Gender = Models.Gender.Male, Name = "Jack" }
			);

			_appDbContext.People.Add(
				new Models.Person { Gender = Models.Gender.Female, Name = "Lili" }
			);

			_appDbContext.SaveChanges();
		}
	}
}
