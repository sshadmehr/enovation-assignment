namespace EnovationAssignment.Exceptions
{
	public class PersonNotFoundException: NotFoundException
	{
		public PersonNotFoundException(int id): base("Person", id)
		{

		}
	}
}
