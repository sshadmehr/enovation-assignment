using EnovationAssignment.Exceptions;
using EnovationAssignment.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnovationAssignment.Services
{
	public class PersonService : IPersonService
	{
		private readonly AppDbContext _appDbContext;
		public PersonService(AppDbContext appDbContext)
		{
			this._appDbContext = appDbContext;

		}

		public async Task<Models.Person> Add(Models.Person person)
		{
			if (string.IsNullOrEmpty(person.Name))
			{
				throw new InvalidInputException("name", person.Name);
			}

			_appDbContext.People.Add(person);
			await _appDbContext.SaveChangesAsync();
			return person;
		}

		public async Task<bool> Delete(int id)
		{
			var person = await _appDbContext.People.FindAsync(id);
			if (person != null)
			{
				_appDbContext.People.Remove(person);
				return (await _appDbContext.SaveChangesAsync()) > 0;
			}
			throw new PersonNotFoundException(id);
		}

		public async Task<Models.Person> Get(int id)
		{
			var person = await _appDbContext.People.FindAsync(id);

			if (person != null)
			{
				return person;
			}
			throw new PersonNotFoundException(id);
		}

		public async Task<IEnumerable<Models.Person>> Search(string? name, Models.Gender? gender)
		{
			var query = _appDbContext.People.Where(x => true);
			if (!string.IsNullOrEmpty(name))
			{
				query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
			}

			if (gender.HasValue)
			{
				query = query.Where(x => x.Gender == gender);
			}

			return await query.ToListAsync();

		}

		public async Task<bool> Update(Models.Person person)
		{
			if (string.IsNullOrEmpty(person.Name))
			{
				throw new InvalidInputException("name", person.Name);
			}

			var persistedPerson = await _appDbContext.People.FindAsync(person.Id);
			if (persistedPerson != null)
			{
				persistedPerson.Name = person.Name;
				persistedPerson.Gender = person.Gender;
				return (await _appDbContext.SaveChangesAsync() > 0);
			}
			throw new PersonNotFoundException(person.Id);
		}

	}
}
