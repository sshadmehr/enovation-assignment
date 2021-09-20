using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnovationAssignment.Services
{
	public interface IPersonService
	{
		Task<Models.Person> Add(Models.Person person);
		Task<bool> Delete(int id);
		Task<Models.Person> Get(int id);
		Task<bool> Update(Models.Person person);
		Task<IEnumerable<Models.Person>> Search(string? name, Models.Gender? gender);
	}
}
